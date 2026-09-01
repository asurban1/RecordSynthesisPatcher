using System;
using System.Collections.Generic;
using RecordSynthesisPatcher.Core;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace RecordSynthesisPatcher.Modules;

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

    public override string Name => "Forward lost REFR / ACHR position and rotation edits";
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
        var winnerPlacement = item.Winner.Placement;
        if (winnerPlacement is null || item.Contexts.Count < 3)
            return;

        var originalPlacement = item.Original.Placement;
        if (originalPlacement is null)
            return;

        bool skipPosition = forwardPosition && HasSafeDisableZ(item);
        if (skipPosition)
            _safeDisabledSkipped++;

        PairResolution positionPairResult = forwardPosition && !skipPosition
            ? ResolvePair(item, PlacementPair.Position)
            : default;
        AxisResolution positionZResult = forwardPosition && !skipPosition
            ? ResolveAxis(item, PlacementAxis.PositionZ)
            : default;
        PairResolution rotationPairResult = forwardRotation
            ? ResolvePair(item, PlacementPair.Rotation)
            : default;
        AxisResolution rotationZResult = forwardRotation
            ? ResolveAxis(item, PlacementAxis.RotationZ)
            : default;

        bool usePositionPair = positionPairResult.Use;
        bool usePositionZ = positionZResult.Use;
        bool useRotationPair = rotationPairResult.Use;
        bool useRotationZ = rotationZResult.Use;

        if (!usePositionPair && !usePositionZ &&
            !useRotationPair && !useRotationZ)
        {
            return;
        }

        var patchPlacement = item.GetOrAddOverride().Placement;
        if (patchPlacement is null)
            return;

        if (usePositionPair || usePositionZ)
        {
            var existing = patchPlacement.Position;
            patchPlacement.Position = new P3Float(
                usePositionPair ? positionPairResult.Value.X : existing.X,
                usePositionPair ? positionPairResult.Value.Y : existing.Y,
                usePositionZ ? positionZResult.Value : existing.Z);

            _positionComponents +=
                (usePositionPair ? 2 : 0) +
                (usePositionZ ? 1 : 0);
        }

        if (useRotationPair || useRotationZ)
        {
            var existing = patchPlacement.Rotation;
            patchPlacement.Rotation = new P3Float(
                useRotationPair ? rotationPairResult.Value.X : existing.X,
                useRotationPair ? rotationPairResult.Value.Y : existing.Y,
                useRotationZ ? rotationZResult.Value : existing.Z);

            _rotationComponents +=
                (useRotationPair ? 2 : 0) +
                (useRotationZ ? 1 : 0);
        }

        if (isActor)
            _achrUpdated++;
        else
            _refrUpdated++;

        if (item.Services.VerboseRecordLogging)
        {
            Console.WriteLine(
                $"{(isActor ? "ACHR" : "REFR")} placement: " +
                $"{item.Winner.FormKey} winner={item.WinningPlugin}");
        }
    }

    private static bool HasSafeDisableZ<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        foreach (var context in item.Contexts)
        {
            if (context.Record.Placement is { } placement &&
                SameValue(placement.Position.Z, SafeDisableZ))
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
        bool isRotation = pair == PlacementPair.Rotation;
        IEqualityComparer<AxisPair?> comparer = isRotation
            ? RotationPairComparer.Instance
            : EqualityComparer<AxisPair?>.Default;

        AxisPair winnerPair = GetPair(item.Winner.Placement!, pair);
        AxisPair originalPair = GetPair(item.Original.Placement!, pair);

        // A non-default winning pair is authoritative. Recover a branch pair
        // only when both winning components still match the original pair.
        if (!comparer.Equals(winnerPair, originalPair))
            return default;

        bool resolved = BranchValueResolver.TryResolve(
            item,
            record => record.Placement is { } placement
                ? GetPair(placement, pair)
                : (AxisPair?)null,
            comparer,
            out AxisPair? value,
            out _);

        return resolved && value.HasValue
            ? new PairResolution(true, value.Value)
            : default;
    }

    private static AxisResolution ResolveAxis<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        PlacementAxis axis)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        bool isRotation = axis == PlacementAxis.RotationZ;
        bool resolved = BranchValueResolver.TryResolve(
            item,
            record => record.Placement is { } placement
                ? GetAxis(placement, axis)
                : (float?)null,
            isRotation
                ? RotationComparer.Instance
                : EqualityComparer<float?>.Default,
            out float? value,
            out _);

        return resolved && value.HasValue
            ? new AxisResolution(true, value.Value)
            : default;
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

    private static float GetAxis(IPlacementGetter placement, PlacementAxis axis)
    {
        return axis switch
        {
            PlacementAxis.PositionZ => placement.Position.Z,
            PlacementAxis.RotationZ => placement.Rotation.Z,
            _ => throw new ArgumentOutOfRangeException(nameof(axis)),
        };
    }

    private static bool SameValue(float first, float second) =>
        first.Equals(second);

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
        bool Use,
        AxisPair Value);

    private readonly record struct AxisResolution(
        bool Use,
        float Value);

    private sealed class RotationPairComparer : IEqualityComparer<AxisPair?>
    {
        public static readonly RotationPairComparer Instance = new();

        public bool Equals(AxisPair? left, AxisPair? right)
        {
            if (!left.HasValue || !right.HasValue)
                return left.HasValue == right.HasValue;

            return RotationComparer.Instance.Equals(left.Value.X, right.Value.X) &&
                RotationComparer.Instance.Equals(left.Value.Y, right.Value.Y);
        }

        public int GetHashCode(AxisPair? value) => 0;
    }

    private sealed class RotationComparer : IEqualityComparer<float?>
    {
        private const float FullCircle = MathF.PI * 2f;
        private const float FullTurnTolerance = 0.0000012f;

        public static readonly RotationComparer Instance = new();

        public bool Equals(float? left, float? right)
        {
            if (!left.HasValue || !right.HasValue)
                return left.HasValue == right.HasValue;

            float difference = left.Value - right.Value;
            if (difference.Equals(0f))
                return true;

            // Use tolerance only to absorb float error around a genuine full
            // turn. Near-zero differences are always meaningful, including
            // edits that straddle xEdit's four-decimal display boundary.
            float turns = MathF.Round(difference / FullCircle);
            if (turns.Equals(0f))
                return false;

            float remainder = difference - (turns * FullCircle);
            return MathF.Abs(remainder) <= FullTurnTolerance;
        }

        public int GetHashCode(float? value) => 0;
    }
}
