using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Core;

internal sealed class WorldspaceMapComparer : IEqualityComparer<IWorldspaceMapGetter?>
{
    public static readonly WorldspaceMapComparer Instance = new();

    public bool Equals(IWorldspaceMapGetter? left, IWorldspaceMapGetter? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;

        // Compare serialized field values, not getter/overlay identity. Keep
        // float bit patterns significant, including signed zero.
        return left.UsableDimensions.Equals(right.UsableDimensions)
            && left.NorthwestCellCoords.Equals(right.NorthwestCellCoords)
            && left.SoutheastCellCoords.Equals(right.SoutheastCellCoords)
            && SameFloat(left.CameraMinHeight, right.CameraMinHeight)
            && SameFloat(left.CameraMaxHeight, right.CameraMaxHeight)
            && SameFloat(left.CameraInitialPitch, right.CameraInitialPitch);
    }

    private static bool SameFloat(float left, float right) =>
        BitConverter.SingleToInt32Bits(left) == BitConverter.SingleToInt32Bits(right);

    public int GetHashCode(IWorldspaceMapGetter? value) => value is null ? 0 :
        HashCode.Combine(value.UsableDimensions, value.NorthwestCellCoords,
            value.SoutheastCellCoords, BitConverter.SingleToInt32Bits(value.CameraMinHeight),
            BitConverter.SingleToInt32Bits(value.CameraMaxHeight),
            BitConverter.SingleToInt32Bits(value.CameraInitialPitch));
}
