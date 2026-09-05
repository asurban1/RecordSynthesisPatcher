using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using RecordSynthesisPatcher.Core;

namespace RecordSynthesisPatcher.Modules;

// Placement forwarding has four independent decision units. X and Y are one
// atomic pair for Position and Rotation; each Z value resolves independently.
// A unit is recoverable only while the winning value still equals the record
// origin. The highest-priority surviving branch decision supplies the unit.
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

        bool skipPosition = forwardPosition && HasSafeDisableZ(item);
        if (skipPosition)
            _safeDisabledSkipped++;

        PairResolution positionPair = forwardPosition && !skipPosition
            ? ResolvePair(item, PlacementPair.Position)
            : default;
        AxisResolution positionZ = forwardPosition && !skipPosition
            ? ResolveAxis(item, PlacementAxis.PositionZ)
            : default;
        PairResolution rotationPair = forwardRotation
            ? ResolvePair(item, PlacementPair.Rotation)
            : default;
        AxisResolution rotationZ = forwardRotation
            ? ResolveAxis(item, PlacementAxis.RotationZ)
            : default;

        bool hasUpdate =
            positionPair.Status == PlacementResolutionStatus.Selected ||
            positionZ.Status == PlacementResolutionStatus.Selected ||
            rotationPair.Status == PlacementResolutionStatus.Selected ||
            rotationZ.Status == PlacementResolutionStatus.Selected;

        if (item.Services.VerboseRecordLogging &&
            (hasUpdate || skipPosition ||
             positionPair.Status == PlacementResolutionStatus.WinnerIsNonDefault ||
             positionZ.Status == PlacementResolutionStatus.WinnerIsNonDefault ||
             rotationPair.Status == PlacementResolutionStatus.WinnerIsNonDefault ||
             rotationZ.Status == PlacementResolutionStatus.WinnerIsNonDefault))
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

        if (positionPair.Status == PlacementResolutionStatus.Selected ||
            positionZ.Status == PlacementResolutionStatus.Selected)
        {
            P3Float existing = patchPlacement.Position;
            patchPlacement.Position = new P3Float(
                positionPair.Status == PlacementResolutionStatus.Selected
                    ? positionPair.Value.X
                    : existing.X,
                positionPair.Status == PlacementResolutionStatus.Selected
                    ? positionPair.Value.Y
                    : existing.Y,
                positionZ.Status == PlacementResolutionStatus.Selected
                    ? positionZ.Value
                    : existing.Z);

            _positionComponents +=
                (positionPair.Status == PlacementResolutionStatus.Selected ? 2 : 0) +
                (positionZ.Status == PlacementResolutionStatus.Selected ? 1 : 0);
        }

        if (rotationPair.Status == PlacementResolutionStatus.Selected ||
            rotationZ.Status == PlacementResolutionStatus.Selected)
        {
            P3Float existing = patchPlacement.Rotation;
            patchPlacement.Rotation = new P3Float(
                rotationPair.Status == PlacementResolutionStatus.Selected
                    ? rotationPair.Value.X
                    : existing.X,
                rotationPair.Status == PlacementResolutionStatus.Selected
                    ? rotationPair.Value.Y
                    : existing.Y,
                rotationZ.Status == PlacementResolutionStatus.Selected
                    ? rotationZ.Value
                    : existing.Z);

            _rotationComponents +=
                (rotationPair.Status == PlacementResolutionStatus.Selected ? 2 : 0) +
                (rotationZ.Status == PlacementResolutionStatus.Selected ? 1 : 0);
        }

        if (isActor)
            _achrUpdated++;
        else
            _refrUpdated++;
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
        PlacementPair pair)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        PlacementValueResolution<AxisPair?> resolution = ResolveBranchValue(
            item,
            record => record.Placement is { } placement
                ? GetPair(placement, pair)
                : null,
            EqualityComparer<AxisPair?>.Default);

        return resolution.Status == PlacementResolutionStatus.Selected &&
            resolution.Value.HasValue
                ? new PairResolution(
                    resolution.Status,
                    resolution.Value.Value,
                    resolution.SourceIndex)
                : new PairResolution(
                    resolution.Status,
                    default,
                    resolution.SourceIndex);
    }

    private static AxisResolution ResolveAxis<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        PlacementAxis axis)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        PlacementValueResolution<float?> resolution = ResolveBranchValue(
            item,
            record => record.Placement is { } placement
                ? GetAxis(placement, axis)
                : null,
            EqualityComparer<float?>.Default);

        return resolution.Status == PlacementResolutionStatus.Selected &&
            resolution.Value.HasValue
                ? new AxisResolution(
                    resolution.Status,
                    resolution.Value.Value,
                    resolution.SourceIndex)
                : new AxisResolution(
                    resolution.Status,
                    default,
                    resolution.SourceIndex);
    }

    private static PlacementValueResolution<TValue> ResolveBranchValue<
        TRecord, TGetter, TValue>(
        RecordWorkItem<TRecord, TGetter> item,
        Func<TGetter, TValue> read,
        IEqualityComparer<TValue> comparer)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        PluginOverrideGraph graph = item.GetGraph();
        TValue rootValue = read(item.GetRecord(graph.Root.ModKey));
        TValue winnerValue = read(item.Winner);
        if (!comparer.Equals(winnerValue, rootValue))
        {
            return new PlacementValueResolution<TValue>(
                PlacementResolutionStatus.WinnerIsNonDefault,
                winnerValue,
                0);
        }

        BranchValueResolution<TValue> resolution = BranchValueResolver.Resolve(
            item,
            plugin => read(item.GetRecord(plugin)),
            comparer);

        return resolution.Status == BranchValueResolutionStatus.Selected
            ? new PlacementValueResolution<TValue>(
                PlacementResolutionStatus.Selected,
                resolution.Value,
                resolution.SourceIndex)
            : new PlacementValueResolution<TValue>(
                PlacementResolutionStatus.NoSurvivingBranchValue,
                winnerValue,
                resolution.SourceIndex);
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
                positionPair.SourceIndex);
            LogUnit(item, "Position Z", positionZ.Status,
                positionZ.SourceIndex);
        }

        LogUnit(item, "Rotation X/Y", rotationPair.Status,
            rotationPair.SourceIndex);
        LogUnit(item, "Rotation Z", rotationZ.Status,
            rotationZ.SourceIndex);
    }

    private static void LogUnit<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        string name,
        PlacementResolutionStatus status,
        int sourceIndex)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        string source = sourceIndex >= 0
            ? item.Contexts[sourceIndex].ModKey.ToString()
            : "none";
        Console.WriteLine($"  {name}: {status}; source={source}.");
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

    private enum PlacementResolutionStatus
    {
        NoSurvivingBranchValue,
        WinnerIsNonDefault,
        Selected,
    }

    private readonly record struct PlacementValueResolution<TValue>(
        PlacementResolutionStatus Status,
        TValue Value,
        int SourceIndex);

    private readonly record struct PairResolution(
        PlacementResolutionStatus Status,
        AxisPair Value,
        int SourceIndex);

    private readonly record struct AxisResolution(
        PlacementResolutionStatus Status,
        float Value,
        int SourceIndex);
}
