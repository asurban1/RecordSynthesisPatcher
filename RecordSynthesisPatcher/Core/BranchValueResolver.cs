using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

namespace RecordSynthesisPatcher.Core;

// Resolves scalar or whole-object values on the real plugin master graph.
// Blank/null is an ordinary value. A decision is created only when a node's
// value differs from every nearest record-owning parent. Matching a parent
// carries that parent's active decision down that path; a decision made on one
// fork is therefore superseded only on that fork, never globally.
internal static class BranchValueResolver
{
    public static bool TryResolve<TRecord, TGetter, TValue>(
        RecordWorkItem<TRecord, TGetter> item,
        System.Func<TGetter, TValue> read,
        IEqualityComparer<TValue> comparer,
        out TValue value,
        out int sourceIndex)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        value = read(item.Winner);
        sourceIndex = 0;

        if (item.Contexts.Count < 3)
            return false;

        PluginOverrideGraph graph = item.GetGraph();
        var contextIndex = new Dictionary<ModKey, int>();
        for (int index = 0; index < item.Contexts.Count; index++)
            contextIndex[item.Contexts[index].ModKey] = index;

        var activeByPlugin =
            new Dictionary<ModKey, ActiveDecision<TValue>?>();

        // Contexts are winner-first, so reverse iteration is origin-first and
        // guarantees every nearest parent has already been evaluated.
        for (int index = item.Contexts.Count - 1; index >= 0; index--)
        {
            var context = item.Contexts[index];
            PluginOverrideNode node = graph.Nodes[context.ModKey];
            if (node.Parents.Count == 0)
            {
                activeByPlugin[context.ModKey] = null;
                continue;
            }

            TValue candidate = read(context.Record);
            ActiveDecision<TValue>? inherited = null;
            bool matchesParent = false;

            foreach (PluginOverrideNode parent in node.Parents)
            {
                if (!comparer.Equals(
                        candidate, read(item.GetRecord(parent.ModKey))))
                    continue;

                matchesParent = true;
                ActiveDecision<TValue>? parentDecision =
                    activeByPlugin[parent.ModKey];
                if (parentDecision is not null &&
                    (inherited is null ||
                     parentDecision.SourceIndex < inherited.SourceIndex))
                {
                    inherited = parentDecision;
                }
            }

            activeByPlugin[context.ModKey] = matchesParent
                ? inherited
                : new ActiveDecision<TValue>(candidate, index);
        }

        // A branch's current leaf priority, rather than the load position of
        // the original decision, decides between independent surviving states.
        // Leaves that still carry only the root state have no active decision
        // and cannot erase a meaningful change from another branch.
        ActiveDecision<TValue>? resolved = null;
        int resolvedLeafIndex = int.MaxValue;
        bool resolvedIsRoot = true;
        TValue rootValue = read(item.GetRecord(graph.Root.ModKey));
        foreach (PluginOverrideNode leaf in graph.Nodes.Values)
        {
            if (leaf.Children.Count != 0 ||
                activeByPlugin[leaf.ModKey] is not { } leafDecision)
                continue;

            int leafIndex = contextIndex[leaf.ModKey];
            bool leafIsRoot = comparer.Equals(leafDecision.Value, rootValue);

            // A default/root-valued decision can remove a change only on its
            // own descendant path. It cannot erase a non-default value that
            // still survives on an independent leaf branch.
            if (resolved is null ||
                (resolvedIsRoot && !leafIsRoot) ||
                (resolvedIsRoot == leafIsRoot &&
                 leafIndex < resolvedLeafIndex))
            {
                resolved = leafDecision;
                resolvedLeafIndex = leafIndex;
                resolvedIsRoot = leafIsRoot;
            }
        }

        if (resolved is null ||
            comparer.Equals(resolved.Value, read(item.Winner)))
        {
            return false;
        }

        value = resolved.Value;
        sourceIndex = resolved.SourceIndex;
        return true;
    }

    private sealed record ActiveDecision<TValue>(
        TValue Value,
        int SourceIndex);
}
