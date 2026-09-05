using System.Collections.Generic;

namespace RecordSynthesisPatcher.Core;

internal enum LeafValueResolutionStatus
{
    NoSurvivingBranchValue,
    WinnerIsNonDefault,
    Selected,
}

internal readonly record struct LeafValueCandidate<TValue>(
    int ContextIndex,
    TValue Value);

internal readonly record struct LeafValueResolution<TValue>(
    LeafValueResolutionStatus Status,
    TValue Value,
    int LeafIndex);

// Resolves a forwarding value from the final state of each dependency branch.
// Root-valued leaves are ignored because they cannot erase a meaningful value
// that still survives on an independent branch. Among meaningful leaves, load
// order decides. A non-root winner remains authoritative and blocks recovery.
internal static class BranchLeafValueResolver
{
    internal static LeafValueResolution<TValue> Resolve<TValue>(
        TValue winnerValue,
        TValue rootValue,
        IReadOnlyList<LeafValueCandidate<TValue>> leaves,
        IEqualityComparer<TValue> comparer)
    {
        TValue selectedValue = default!;
        int selectedLeafIndex = int.MaxValue;

        foreach (LeafValueCandidate<TValue> leaf in leaves)
        {
            if (comparer.Equals(leaf.Value, rootValue) ||
                leaf.ContextIndex >= selectedLeafIndex)
            {
                continue;
            }

            selectedValue = leaf.Value;
            selectedLeafIndex = leaf.ContextIndex;
        }

        if (selectedLeafIndex == int.MaxValue)
        {
            return new LeafValueResolution<TValue>(
                LeafValueResolutionStatus.NoSurvivingBranchValue,
                default!,
                -1);
        }

        if (!comparer.Equals(winnerValue, rootValue))
        {
            return new LeafValueResolution<TValue>(
                LeafValueResolutionStatus.WinnerIsNonDefault,
                selectedValue,
                selectedLeafIndex);
        }

        return new LeafValueResolution<TValue>(
            LeafValueResolutionStatus.Selected,
            selectedValue,
            selectedLeafIndex);
    }
}
