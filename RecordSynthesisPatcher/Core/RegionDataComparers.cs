using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Core;

internal sealed class DelegateEqualityComparer<T> : IEqualityComparer<T>
{
    private readonly Func<T, T, bool> _equals;

    public DelegateEqualityComparer(Func<T, T, bool> equals) =>
        _equals = equals;

    public bool Equals(T? left, T? right) => _equals(left!, right!);
    public int GetHashCode(T value) => 0;
}

internal static class RegionDataComparers
{
    public static readonly IEqualityComparer<IRegionGrassesGetter?> Grasses =
        new DelegateEqualityComparer<IRegionGrassesGetter?>(
            (left, right) => Same(left, right,
                (first, second) => first.DeepCopy().Equals(second.DeepCopy())));

    public static readonly IEqualityComparer<IRegionLandGetter?> Land =
        new DelegateEqualityComparer<IRegionLandGetter?>(
            (left, right) => Same(left, right,
                (first, second) => first.DeepCopy().Equals(second.DeepCopy())));

    public static readonly IEqualityComparer<IRegionMapGetter?> Map =
        new DelegateEqualityComparer<IRegionMapGetter?>(
            (left, right) => Same(left, right,
                (first, second) => first.DeepCopy().Equals(second.DeepCopy())));

    public static readonly IEqualityComparer<IRegionObjectsGetter?> Objects =
        new DelegateEqualityComparer<IRegionObjectsGetter?>(
            (left, right) => Same(left, right,
                (first, second) => first.DeepCopy().Equals(second.DeepCopy())));

    public static readonly IEqualityComparer<IRegionSoundsGetter?> Sounds =
        new DelegateEqualityComparer<IRegionSoundsGetter?>(
            (left, right) => Same(left, right,
                (first, second) => first.DeepCopy().Equals(second.DeepCopy())));

    public static readonly IEqualityComparer<IRegionWeatherGetter?> Weather =
        new DelegateEqualityComparer<IRegionWeatherGetter?>(
            (left, right) => Same(left, right,
                (first, second) => first.DeepCopy().Equals(second.DeepCopy())));

    private static bool Same<TGetter>(
        TGetter? left,
        TGetter? right,
        Func<TGetter, TGetter, bool> equals)
        where TGetter : class
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;
        return equals(left, right);
    }
}
