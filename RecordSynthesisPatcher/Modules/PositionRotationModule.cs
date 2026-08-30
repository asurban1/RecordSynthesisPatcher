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
            $"  Safe-disabled (-30000 Z) records skipped: " +
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

        // A safe-disabled reference must never be revived by transform recovery.
        foreach (var context in item.Contexts)
        {
            if (context.Record.Placement is { } placement &&
                SameValue(placement.Position.Z, SafeDisableZ))
            {
                _safeDisabledSkipped++;
                return;
            }
        }

        AxisResolution xResult = forwardPosition
            ? ResolveAxis(item, PlacementAxis.PositionX)
            : default;
        AxisResolution yResult = forwardPosition
            ? ResolveAxis(item, PlacementAxis.PositionY)
            : default;

        // Never synthesize horizontal coordinates from two different mods.
        // When independent branches change one axis each, the higher-priority
        // branch wins and the other component remains with the record winner.
        if (xResult.Use && yResult.Use &&
            xResult.SourceIndex != yResult.SourceIndex)
        {
            if (xResult.SourceIndex < yResult.SourceIndex)
                yResult = default;
            else
                xResult = default;
        }

        AxisResolution zResult = forwardPosition
            ? ResolveAxis(item, PlacementAxis.PositionZ)
            : default;
        AxisResolution rotationXResult = forwardRotation
            ? ResolveAxis(item, PlacementAxis.RotationX)
            : default;
        AxisResolution rotationYResult = forwardRotation
            ? ResolveAxis(item, PlacementAxis.RotationY)
            : default;
        AxisResolution rotationZResult = forwardRotation
            ? ResolveAxis(item, PlacementAxis.RotationZ)
            : default;

        bool useX = xResult.Use;
        bool useY = yResult.Use;
        bool useZ = zResult.Use;
        bool useRotationX = rotationXResult.Use;
        bool useRotationY = rotationYResult.Use;
        bool useRotationZ = rotationZResult.Use;

        if (!useX && !useY && !useZ &&
            !useRotationX && !useRotationY && !useRotationZ)
        {
            return;
        }

        var patchPlacement = item.GetOrAddOverride().Placement;
        if (patchPlacement is null)
            return;

        if (useX || useY || useZ)
        {
            var existing = patchPlacement.Position;
            patchPlacement.Position = new P3Float(
                useX ? xResult.Value : existing.X,
                useY ? yResult.Value : existing.Y,
                useZ ? zResult.Value : existing.Z);

            _positionComponents += BoolCount(useX, useY, useZ);
        }

        if (useRotationX || useRotationY || useRotationZ)
        {
            var existing = patchPlacement.Rotation;
            patchPlacement.Rotation = new P3Float(
                useRotationX ? rotationXResult.Value : existing.X,
                useRotationY ? rotationYResult.Value : existing.Y,
                useRotationZ ? rotationZResult.Value : existing.Z);

            _rotationComponents += BoolCount(
                useRotationX,
                useRotationY,
                useRotationZ);
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

    private static AxisResolution ResolveAxis<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        PlacementAxis axis)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        bool isRotation = axis is PlacementAxis.RotationX or
            PlacementAxis.RotationY or PlacementAxis.RotationZ;
        bool resolved = BranchValueResolver.TryResolve(
            item,
            record => record.Placement is { } placement
                ? GetAxis(placement, axis)
                : (float?)null,
            isRotation
                ? RotationComparer.Instance
                : EqualityComparer<float?>.Default,
            out float? value,
            out int sourceIndex);

        return resolved && value.HasValue
            ? new AxisResolution(true, value.Value, sourceIndex)
            : default;
    }

    private static float GetAxis(IPlacementGetter placement, PlacementAxis axis)
    {
        return axis switch
        {
            PlacementAxis.PositionX => placement.Position.X,
            PlacementAxis.PositionY => placement.Position.Y,
            PlacementAxis.PositionZ => placement.Position.Z,
            PlacementAxis.RotationX => placement.Rotation.X,
            PlacementAxis.RotationY => placement.Rotation.Y,
            PlacementAxis.RotationZ => placement.Rotation.Z,
            _ => throw new ArgumentOutOfRangeException(nameof(axis)),
        };
    }

    private static bool SameValue(float first, float second) =>
        first.Equals(second);

    private static int BoolCount(bool first, bool second, bool third) =>
        (first ? 1 : 0) + (second ? 1 : 0) + (third ? 1 : 0);

    private enum PlacementAxis
    {
        PositionX,
        PositionY,
        PositionZ,
        RotationX,
        RotationY,
        RotationZ,
    }

    private readonly record struct AxisResolution(
        bool Use,
        float Value,
        int SourceIndex);

    private sealed class RotationComparer : IEqualityComparer<float?>
    {
        private const float FullCircle = MathF.PI * 2f;
        // Keep enough tolerance for equivalent +/- full-circle float
        // encodings, but do not swallow a one-unit xEdit display change
        // (0.0001 degree is about 0.000001745 radians).
        private const float Tolerance = 0.0000012f;

        public static readonly RotationComparer Instance = new();

        public bool Equals(float? left, float? right)
        {
            if (!left.HasValue || !right.HasValue)
                return left.HasValue == right.HasValue;

            float difference = MathF.Abs(Normalize(left.Value) - Normalize(right.Value));
            difference = MathF.Min(difference, FullCircle - difference);
            return difference <= Tolerance;
        }

        public int GetHashCode(float? value) => 0;

        private static float Normalize(float value)
        {
            float normalized = value % FullCircle;
            return normalized < 0f ? normalized + FullCircle : normalized;
        }
    }
}
