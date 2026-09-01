using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

namespace RecordSynthesisPatcher.Core;

// Resolves each flag bit independently on the real plugin master graph. A
// decision remains active only on descendants of the branch that made it. A
// default/root-valued leaf therefore cannot erase a meaningful decision that
// still survives on an independent branch.
public static class BranchFlagMerger
{
    public static ulong Resolve<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        PluginOverrideGraph graph,
        IReadOnlySet<ModKey> ignoredPlugins,
        Func<TGetter, ulong> read,
        ulong mask)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        var contextIndex = new Dictionary<ModKey, int>();
        for (int index = 0; index < item.Contexts.Count; index++)
        {
            ModKey modKey = item.Contexts[index].ModKey;
            if (!ignoredPlugins.Contains(modKey))
                contextIndex[modKey] = index;
        }

        if (contextIndex.Count == 0)
            return 0;

        ulong rootFlags = read(item.GetRecord(graph.Root.ModKey)) & mask;
        ulong desired = rootFlags;
        var activeByContext =
            new ActiveDecision?[item.Contexts.Count];

        for (ulong remaining = mask; remaining != 0; remaining &= remaining - 1)
        {
            ulong bit = remaining & (~remaining + 1);
            Array.Clear(activeByContext);

            // Contexts are winner-first, so reverse iteration is origin-first
            // and guarantees every nearest parent has already been evaluated.
            for (int index = item.Contexts.Count - 1; index >= 0; index--)
            {
                var context = item.Contexts[index];
                if (ignoredPlugins.Contains(context.ModKey))
                    continue;

                PluginOverrideNode node = graph.Nodes[context.ModKey];
                if (node.Parents.Count == 0)
                {
                    activeByContext[index] = null;
                    continue;
                }

                bool candidate = (read(context.Record) & bit) != 0;
                ActiveDecision? inherited = null;
                bool matchesParent = false;
                foreach (PluginOverrideNode parent in node.Parents)
                {
                    bool parentValue =
                        (read(item.GetRecord(parent.ModKey)) & bit) != 0;
                    if (candidate != parentValue)
                        continue;

                    matchesParent = true;
                    ActiveDecision? parentDecision = activeByContext[
                        contextIndex[parent.ModKey]];
                    if (parentDecision.HasValue &&
                        (inherited is null ||
                         parentDecision.Value.SourceIndex <
                         inherited.Value.SourceIndex))
                    {
                        inherited = parentDecision;
                    }
                }

                activeByContext[index] = matchesParent
                    ? inherited
                    : new ActiveDecision(candidate, index);
            }

            ActiveDecision? resolved = null;
            int resolvedLeafIndex = int.MaxValue;
            bool resolvedIsRoot = true;
            bool rootValue = (rootFlags & bit) != 0;
            foreach (PluginOverrideNode leaf in graph.Nodes.Values)
            {
                int leafIndex = contextIndex[leaf.ModKey];
                if (leaf.Children.Count != 0 ||
                    activeByContext[leafIndex] is not { } leafDecision)
                    continue;

                bool leafIsRoot = leafDecision.Value == rootValue;

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

            bool resolvedValue = resolved?.Value ?? rootValue;
            if (resolvedValue)
                desired |= bit;
            else
                desired &= ~bit;
        }

        return desired & mask;
    }

    private readonly record struct ActiveDecision(
        bool Value,
        int SourceIndex);
}
