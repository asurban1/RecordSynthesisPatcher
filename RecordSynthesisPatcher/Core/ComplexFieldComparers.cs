using System.Collections.Generic;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Core;

internal static class ComplexFieldComparers
{
    public static readonly IEqualityComparer<
        IGenderedItemGetter<IModelGetter?>?> GenderedModel =
        new DelegateEqualityComparer<IGenderedItemGetter<IModelGetter?>?>(
            SameGenderedModel);

    public static readonly IEqualityComparer<
        ITeleportDestinationGetter?> TeleportDestination =
        new DelegateEqualityComparer<ITeleportDestinationGetter?>(
            (left, right) => Same(
                left,
                right,
                (first, second) =>
                    first.DeepCopy().Equals(second.DeepCopy())));

    private static bool SameGenderedModel(
        IGenderedItemGetter<IModelGetter?>? left,
        IGenderedItemGetter<IModelGetter?>? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;

        return SameModel(left.Male, right.Male) &&
               SameModel(left.Female, right.Female);
    }

    private static bool SameModel(IModelGetter? left, IModelGetter? right) =>
        Same(
            left,
            right,
            (first, second) =>
                first.DeepCopy().Equals(second.DeepCopy()));

    private static bool Same<T>(
        T? left,
        T? right,
        System.Func<T, T, bool> equals)
        where T : class
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;
        return equals(left, right);
    }
}
