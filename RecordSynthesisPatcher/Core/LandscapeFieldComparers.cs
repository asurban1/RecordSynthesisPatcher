using System.Collections.Generic;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace RecordSynthesisPatcher.Core;

internal sealed class LandscapeVertexArrayComparer :
    IEqualityComparer<IReadOnlyArray2d<P3UInt8>?>
{
    public static readonly LandscapeVertexArrayComparer Instance = new();

    public bool Equals(
        IReadOnlyArray2d<P3UInt8>? left,
        IReadOnlyArray2d<P3UInt8>? right) =>
        LandscapeArray2d.Equals(left, right);

    public int GetHashCode(IReadOnlyArray2d<P3UInt8>? value) => 0;
}

internal sealed class LandscapeVertexHeightMapComparer :
    IEqualityComparer<ILandscapeVertexHeightMapGetter?>
{
    public static readonly LandscapeVertexHeightMapComparer Instance = new();

    public bool Equals(
        ILandscapeVertexHeightMapGetter? left,
        ILandscapeVertexHeightMapGetter? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;

        return left.Offset.Equals(right.Offset) &&
               left.Unknown.Equals(right.Unknown) &&
               LandscapeArray2d.Equals(left.HeightMap, right.HeightMap);
    }

    public int GetHashCode(ILandscapeVertexHeightMapGetter? value) => 0;
}

internal static class LandscapeArray2d
{
    public static bool Equals<T>(
        IReadOnlyArray2d<T>? left,
        IReadOnlyArray2d<T>? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null ||
            left.Width != right.Width || left.Height != right.Height)
        {
            return false;
        }

        var comparer = EqualityComparer<T>.Default;
        for (int y = 0; y < left.Height; y++)
        for (int x = 0; x < left.Width; x++)
        {
            if (!comparer.Equals(left[x, y], right[x, y]))
                return false;
        }

        return true;
    }
}
