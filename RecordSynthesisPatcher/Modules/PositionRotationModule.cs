using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using RecordSynthesisPatcher.Core;

namespace RecordSynthesisPatcher.Modules;

// Placement forwarding has four independent decision units. X and Y are one
// atomic pair for Position and Rotation; each Z value resolves independently.
// A unit is recoverable only while the winning value still equals the record
// origin. The highest-priority non-origin leaf then supplies the whole unit.
public sealed class PositionRotationModule : PatcherModule,
    IRecordModule<IPlacedObject, IPlacedObjectGetter>,
    IRecordModule<IPlacedNpc, IPlacedNpcGetter>
{
    private const float SafeDisableZ = -30_000f;

    private int _refrUpdated;
    private int _achrUpdated;
    private int _positionComponents;
    private int _rotationComponents;
    private int _safeDisabledSkipped;

    public override string Name =>
        "Forward lost REFR / ACHR position and rotation edits";

    public override int Order => 300;

    public void Process(RecordWorkItem<IPlacedObject, IPlacedObjectGetter> item)
    {
        var settings = item.Services.Settings.REFR;
        if (!settings.Position && !settings.Rotation)
            return;

        ProcessPlaced(
            item,
            isActor: false,
            forwardPosition: settings.Position,
            forwardRotation: settings.Rotation);
    }

    public void Process(RecordWorkItem<IPlacedNpc, IPlacedNpcGetter> item)
    {
        var settings = item.Services.Settings.ACHR;
        if (!settings.Position && !settings.Rotation)
            return;

        ProcessPlaced(
            item,
            isActor: true,
            forwardPosition: settings.Position,
            forwardRotation: settings.Rotation);
    }

    public override void Complete(PatcherServices services)
    {
        Console.WriteLine(
            $"Placement: {_refrUpdated:N0} REFR and {_achrUpdated:N0} ACHR " +
            "records updated.");
        Console.WriteLine(
            $"  Position components forwarded: {_positionComponents:N0}; " +
            $"rotation components forwarded: {_rotationComponents:N0}.");
        Console.WriteLine(
            $"  Safe-disabled (-30000 Z) position fields skipped: " +
            $"{_safeDisabledSkipped:N0}.");
    }

    private void ProcessPlaced<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        bool isActor,
        bool forwardPosition,
        bool forwardRotation)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        if (item.Contexts.Count < 3 ||
            item.Winner.Placement is null ||
            item.Original.Placement is null)
        {
            return;
        }

        PluginOverrideGraph graph = item.GetGraph();
        IReadOnlyDictionary<ModKey, int> contextIndex =
            BuildContextIndex(item);

        bool skipPosition = forwardPosition && HasSafeDisableZ(item);
        if (skipPosition)
            _safeDisabledSkipped++;

        PairResolution positionPair = forwardPosition && !skipPosition
            ? ResolvePair(item, graph, contextIndex, PlacementPair.Position)
            : default;
        AxisResolution positionZ = forwardPosition && !skipPosition
            ? ResolveAxis(item, graph, contextIndex, PlacementAxis.PositionZ)
            : default;
        PairResolution rotationPair = forwardRotation
            ? ResolvePair(item, graph, contextIndex, PlacementPair.Rotation)
            : default;
        AxisResolution rotationZ = forwardRotation
            ? ResolveAxis(item, graph, contextIndex, PlacementAxis.RotationZ)
            : default;

        bool hasUpdate =
            positionPair.Status == LeafValueResolutionStatus.Selected ||
            positionZ.Status == LeafValueResolutionStatus.Selected ||
            rotationPair.Status == LeafValueResolutionStatus.Selected ||
            rotationZ.Status == LeafValueResolutionStatus.Selected;

        if (item.Services.VerboseRecordLogging &&
            (hasUpdate || skipPosition ||
             positionPair.Status == LeafValueResolutionStatus.WinnerIsNonDefault ||
             positionZ.Status == LeafValueResolutionStatus.WinnerIsNonDefault ||
             rotationPair.Status == LeafValueResolutionStatus.WinnerIsNonDefault ||
             rotationZ.Status == LeafValueResolutionStatus.WinnerIsNonDefault))
        {
            LogDecision(
                item,
                isActor,
                skipPosition,
                positionPair,
                positionZ,
                rotationPair,
                rotationZ);
        }

        if (!hasUpdate)
            return;

        IPlacement? patchPlacement = item.GetOrAddOverride().Placement;
        if (patchPlacement is null)
            return;

        if (positionPair.Status == LeafValueResolutionStatus.Selected ||
            positionZ.Status == LeafValueResolutionStatus.Selected)
        {
            P3Float existing = patchPlacement.Position;
            patchPlacement.Position = new P3Float(
                positionPair.Status == LeafValueResolutionStatus.Selected
                    ? positionPair.Value.X
                    : existing.X,
                positionPair.Status == LeafValueResolutionStatus.Selected
                    ? positionPair.Value.Y
                    : existing.Y,
                positionZ.Status == LeafValueResolutionStatus.Selected
                    ? positionZ.Value
                    : existing.Z);

            _positionComponents +=
                (positionPair.Status == LeafValueResolutionStatus.Selected ? 2 : 0) +
                (positionZ.Status == LeafValueResolutionStatus.Selected ? 1 : 0);
        }

        if (rotationPair.Status == LeafValueResolutionStatus.Selected ||
            rotationZ.Status == LeafValueResolutionStatus.Selected)
        {
            P3Float existing = patchPlacement.Rotation;
            patchPlacement.Rotation = new P3Float(
                rotationPair.Status == LeafValueResolutionStatus.Selected
                    ? rotationPair.Value.X
                    : existing.X,
                rotationPair.Status == LeafValueResolutionStatus.Selected
                    ? rotationPair.Value.Y
                    : existing.Y,
                rotationZ.Status == LeafValueResolutionStatus.Selected
                    ? rotationZ.Value
                    : existing.Z);

            _rotationComponents +=
                (rotationPair.Status == LeafValueResolutionStatus.Selected ? 2 : 0) +
                (rotationZ.Status == LeafValueResolutionStatus.Selected ? 1 : 0);
        }

        if (isActor)
            _achrUpdated++;
        else
            _refrUpdated++;
    }

    private static IReadOnlyDictionary<ModKey, int> BuildContextIndex<
        TRecord, TGetter>(RecordWorkItem<TRecord, TGetter> item)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        var result = new Dictionary<ModKey, int>(item.Contexts.Count);
        for (int index = 0; index < item.Contexts.Count; index++)
            result[item.Contexts[index].ModKey] = index;

        return result;
    }

    private static bool HasSafeDisableZ<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        foreach (var context in item.Contexts)
        {
            if (context.Record.Placement is { } placement &&
                placement.Position.Z.Equals(SafeDisableZ))
            {
                return true;
            }
        }

        return false;
    }

    private static PairResolution ResolvePair<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        PluginOverrideGraph graph,
        IReadOnlyDictionary<ModKey, int> contextIndex,
        PlacementPair pair)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        LeafValueResolution<AxisPair?> resolution = ResolveLeafValue(
            item,
            graph,
            contextIndex,
            record => record.Placement is { } placement
                ? GetPair(placement, pair)
                : null,
            EqualityComparer<AxisPair?>.Default);

        return resolution.Status == LeafValueResolutionStatus.Selected &&
            resolution.Value.HasValue
                ? new PairResolution(
                    resolution.Status,
                    resolution.Value.Value,
                    resolution.LeafIndex)
                : new PairResolution(
                    resolution.Status,
                    default,
                    resolution.LeafIndex);
    }

    private static AxisResolution ResolveAxis<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        PluginOverrideGraph graph,
        IReadOnlyDictionary<ModKey, int> contextIndex,
        PlacementAxis axis)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        LeafValueResolution<float?> resolution = ResolveLeafValue(
            item,
            graph,
            contextIndex,
            record => record.Placement is { } placement
                ? GetAxis(placement, axis)
                : null,
            EqualityComparer<float?>.Default);

        return resolution.Status == LeafValueResolutionStatus.Selected &&
            resolution.Value.HasValue
                ? new AxisResolution(
                    resolution.Status,
                    resolution.Value.Value,
                    resolution.LeafIndex)
                : new AxisResolution(
                    resolution.Status,
                    default,
                    resolution.LeafIndex);
    }

    // Every graph node owns an override of this record, so a leaf's value is
    // the final state of that entire dependency branch. Looking at leaf state
    // directly makes descendant reversions unambiguous and avoids carrying a
    // stale ancestor decision beyond the descendant that superseded it.
    private static LeafValueResolution<TValue> ResolveLeafValue<
        TRecord, TGetter, TValue>(
        RecordWorkItem<TRecord, TGetter> item,
        PluginOverrideGraph graph,
        IReadOnlyDictionary<ModKey, int> contextIndex,
        Func<TGetter, TValue> read,
        IEqualityComparer<TValue> comparer)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        TValue rootValue = read(item.GetRecord(graph.Root.ModKey));
        TValue winnerValue = read(item.Winner);
        var leaves = new List<LeafValueCandidate<TValue>>();

        foreach (PluginOverrideNode leaf in graph.Nodes.Values)
        {
            if (leaf.Children.Count != 0)
                continue;

            leaves.Add(new LeafValueCandidate<TValue>(
                contextIndex[leaf.ModKey],
                read(item.GetRecord(leaf.ModKey))));
        }

        return BranchLeafValueResolver.Resolve(
            winnerValue,
            rootValue,
            leaves,
            comparer);
    }

    private static AxisPair GetPair(
        IPlacementGetter placement,
        PlacementPair pair)
    {
        P3Float value = pair == PlacementPair.Position
            ? placement.Position
            : placement.Rotation;

        return new AxisPair(value.X, value.Y);
    }

    private static float GetAxis(
        IPlacementGetter placement,
        PlacementAxis axis) => axis switch
        {
            PlacementAxis.PositionZ => placement.Position.Z,
            PlacementAxis.RotationZ => placement.Rotation.Z,
            _ => throw new ArgumentOutOfRangeException(nameof(axis)),
        };

    private static void LogDecision<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        bool isActor,
        bool skipPosition,
        PairResolution positionPair,
        AxisResolution positionZ,
        PairResolution rotationPair,
        AxisResolution rotationZ)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        string signature = isActor ? "ACHR" : "REFR";
        Console.WriteLine(
            $"{signature} placement decision: {item.Winner.FormKey} " +
            $"winner={item.WinningPlugin}");

        if (skipPosition)
        {
            Console.WriteLine(
                "  Position: skipped because an override has Z=-30000.");
        }
        else
        {
            LogUnit(item, "Position X/Y", positionPair.Status,
                positionPair.LeafIndex);
            LogUnit(item, "Position Z", positionZ.Status,
                positionZ.LeafIndex);
        }

        LogUnit(item, "Rotation X/Y", rotationPair.Status,
            rotationPair.LeafIndex);
        LogUnit(item, "Rotation Z", rotationZ.Status,
            rotationZ.LeafIndex);
    }

    private static void LogUnit<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        string name,
        LeafValueResolutionStatus status,
        int leafIndex)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        string source = leafIndex >= 0
            ? item.Contexts[leafIndex].ModKey.ToString()
            : "none";
        Console.WriteLine($"  {name}: {status}; leaf={source}.");
    }

    private enum PlacementAxis
    {
        PositionZ,
        RotationZ,
    }

    private enum PlacementPair
    {
        Position,
        Rotation,
    }

    private readonly record struct AxisPair(float X, float Y);

    private readonly record struct PairResolution(
        LeafValueResolutionStatus Status,
        AxisPair Value,
        int LeafIndex);

    private readonly record struct AxisResolution(
        LeafValueResolutionStatus Status,
        float Value,
        int LeafIndex);
}
