using System.Collections.Generic;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace RecordSynthesisPatcher.Core;

internal sealed class CellWaterVelocityComparer :
    IEqualityComparer<ICellWaterVelocityGetter?>
{
    public static readonly CellWaterVelocityComparer Instance = new();

    public bool Equals(
        ICellWaterVelocityGetter? left,
        ICellWaterVelocityGetter? right)
    {
        if (ReferenceEquals(left, right)) return true;
        bool leftDefault = IsDefault(left);
        bool rightDefault = IsDefault(right);
        if (leftDefault || rightDefault)
            return leftDefault == rightDefault;

        return left!.Offset.Equals(right!.Offset) &&
               left.Unknown == right.Unknown &&
               left.Angle.Equals(right.Angle) &&
               left.Unknown2.Span.SequenceEqual(right.Unknown2.Span);
    }

    public int GetHashCode(ICellWaterVelocityGetter? value) => 0;

    // Mutagen can materialize a visually absent XWCU block as a non-null
    // structure containing only zeroes. Treat it as the same default state as
    // null so it cannot outrank a surviving velocity vector from another
    // branch.
    private static bool IsDefault(ICellWaterVelocityGetter? value)
    {
        if (value is null)
            return true;

        if (!value.Offset.Equals(new P3Float(0f, 0f, 0f)) ||
            !value.Angle.Equals(new P3Float(0f, 0f, 0f)) ||
            value.Unknown != 0)
        {
            return false;
        }

        foreach (byte item in value.Unknown2.Span)
        {
            if (item != 0)
                return false;
        }

        return true;
    }
}

internal sealed class CellMaxHeightDataComparer :
    IEqualityComparer<ICellMaxHeightDataGetter?>
{
    public static readonly CellMaxHeightDataComparer Instance = new();

    public bool Equals(
        ICellMaxHeightDataGetter? left,
        ICellMaxHeightDataGetter? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null || left.Offset != right.Offset)
            return false;

        IReadOnlyArray2d<byte> a = left.HeightMap;
        IReadOnlyArray2d<byte> b = right.HeightMap;
        if (a.Width != b.Width || a.Height != b.Height)
            return false;

        for (int y = 0; y < a.Height; y++)
        for (int x = 0; x < a.Width; x++)
        {
            if (a[x, y] != b[x, y])
                return false;
        }

        return true;
    }

    public int GetHashCode(ICellMaxHeightDataGetter? value) => 0;
}

internal sealed class NullableByteSliceComparer :
    IEqualityComparer<ReadOnlyMemorySlice<byte>?>
{
    public static readonly NullableByteSliceComparer Instance = new();

    public bool Equals(
        ReadOnlyMemorySlice<byte>? left,
        ReadOnlyMemorySlice<byte>? right)
    {
        if (!left.HasValue || !right.HasValue)
            return left.HasValue == right.HasValue;
        return left.Value.Span.SequenceEqual(right.Value.Span);
    }

    public int GetHashCode(ReadOnlyMemorySlice<byte>? value) => 0;
}
