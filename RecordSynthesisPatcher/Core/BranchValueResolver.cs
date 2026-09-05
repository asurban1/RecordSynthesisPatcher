using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

namespace RecordSynthesisPatcher.Core;

internal enum BranchValueResolutionStatus
{
    NoSurvivingDecision,
    WinnerAlreadyMatches,
    Selected,
}

internal readonly record struct BranchValueResolution<TValue>(
    BranchValueResolutionStatus Status,
    TValue Value,
    int SourceIndex,
    int LeafIndex);

// Resolves values on the real plugin master graph. Each node carries every
// independent decision inherited from its nearest record-owning parents. A
// node creates a replacement decision only when its value differs from all of
// those parents; matching one parent must not discard decisions arriving from
// the others. Descendant changes still replace all decisions on their path.
internal static class BranchValueResolver
{
    public static bool TryResolve<TRecord, TGetter, TValue>(
        RecordWorkItem<TRecord, TGetter> item,
        Func<TGetter, TValue> read,
        IEqualityComparer<TValue> comparer,
        out TValue value,
        out int sourceIndex)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        BranchValueResolution<TValue> resolution = Resolve(
            item.GetResolutionTopology(),
            plugin => read(item.GetRecord(plugin)),
            comparer);

        value = resolution.Value;
        sourceIndex = resolution.SourceIndex;
        return resolution.Status == BranchValueResolutionStatus.Selected;
    }

    internal static BranchValueResolution<TValue> Resolve<
        TRecord, TGetter, TValue>(
        RecordWorkItem<TRecord, TGetter> item,
        Func<ModKey, TValue> read,
        IEqualityComparer<TValue> comparer)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter =>
        Resolve(item.GetResolutionTopology(), read, comparer);

    internal static BranchValueResolution<TValue> Resolve<TValue>(
        BranchResolutionTopology topology,
        Func<ModKey, TValue> read,
        IEqualityComparer<TValue> comparer)
    {
        TValue winnerValue = read(topology.Plugins[topology.WinnerIndex]);
        if (topology.Plugins.Length < 3)
        {
            return new BranchValueResolution<TValue>(
                BranchValueResolutionStatus.NoSurvivingDecision,
                winnerValue,
                topology.WinnerIndex,
                -1);
        }

        var activeByIndex =
            new List<ActiveDecision<TValue>>?[topology.Plugins.Length];

        // Plugins are winner-first, so reverse iteration is origin-first and
        // guarantees that every nearest parent has already been evaluated.
        for (int index = topology.Plugins.Length - 1; index >= 0; index--)
        {
            int[] parents = topology.Parents[index];
            if (parents.Length == 0)
                continue;

            TValue candidate = read(topology.Plugins[index]);
            bool matchesAnyParent = false;
            foreach (int parentIndex in parents)
            {
                if (comparer.Equals(
                        candidate, read(topology.Plugins[parentIndex])))
                {
                    matchesAnyParent = true;
                    break;
                }
            }

            if (!matchesAnyParent)
            {
                // This node explicitly resolves the value against every
                // incoming branch, replacing their decisions on this path.
                activeByIndex[index] =
                    new List<ActiveDecision<TValue>>(1)
                    {
                        new(candidate, index),
                    };
                continue;
            }

            // Matching one parent is not an explicit rejection of the other
            // independent parents. Preserve every distinct source decision.
            List<ActiveDecision<TValue>>? inherited = null;
            foreach (int parentIndex in parents)
            {
                if (activeByIndex[parentIndex] is not { } parentDecisions)
                    continue;

                inherited ??= new List<ActiveDecision<TValue>>();
                foreach (ActiveDecision<TValue> decision in parentDecisions)
                {
                    if (!ContainsSource(inherited, decision.SourceIndex))
                        inherited.Add(decision);
                }
            }

            activeByIndex[index] = inherited;
        }

        TValue rootValue = read(topology.Plugins[topology.RootIndex]);
        ActiveDecision<TValue>? resolved = null;
        int resolvedLeafIndex = int.MaxValue;

        for (int leafIndex = 0; leafIndex < topology.Plugins.Length; leafIndex++)
        {
            if (!topology.Leaves[leafIndex] ||
                activeByIndex[leafIndex] is not { } leafDecisions)
            {
                continue;
            }

            ActiveDecision<TValue>? leafDecision = null;
            foreach (ActiveDecision<TValue> decision in leafDecisions)
            {
                // A root-valued decision closes its own descendant path. It
                // cannot erase a meaningful value surviving independently.
                if (comparer.Equals(decision.Value, rootValue))
                    continue;

                if (leafDecision is null ||
                    decision.SourceIndex < leafDecision.Value.SourceIndex)
                {
                    leafDecision = decision;
                }
            }

            if (leafDecision is null)
                continue;

            if (resolved is null ||
                leafIndex < resolvedLeafIndex ||
                leafIndex == resolvedLeafIndex &&
                leafDecision.Value.SourceIndex < resolved.Value.SourceIndex)
            {
                resolved = leafDecision;
                resolvedLeafIndex = leafIndex;
            }
        }

        if (resolved is null)
        {
            return new BranchValueResolution<TValue>(
                BranchValueResolutionStatus.NoSurvivingDecision,
                winnerValue,
                topology.WinnerIndex,
                -1);
        }

        if (comparer.Equals(resolved.Value.Value, winnerValue))
        {
            return new BranchValueResolution<TValue>(
                BranchValueResolutionStatus.WinnerAlreadyMatches,
                resolved.Value.Value,
                resolved.Value.SourceIndex,
                resolvedLeafIndex);
        }

        return new BranchValueResolution<TValue>(
            BranchValueResolutionStatus.Selected,
            resolved.Value.Value,
            resolved.Value.SourceIndex,
            resolvedLeafIndex);
    }

    private static bool ContainsSource<TValue>(
        List<ActiveDecision<TValue>> decisions,
        int sourceIndex)
    {
        foreach (ActiveDecision<TValue> decision in decisions)
        {
            if (decision.SourceIndex == sourceIndex)
                return true;
        }

        return false;
    }

    private readonly record struct ActiveDecision<TValue>(
        TValue Value,
        int SourceIndex);
}
