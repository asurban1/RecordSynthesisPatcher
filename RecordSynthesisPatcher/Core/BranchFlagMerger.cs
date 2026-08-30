using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

namespace RecordSynthesisPatcher.Core;

// Resolves each flag bit independently. The highest-priority plugin that
// changed a bit relative to a parent branch decides whether it is set or clear.
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
        ulong desired = 0;
        bool foundRealWinner = false;

        foreach (var context in item.Contexts)
        {
            if (ignoredPlugins.Contains(context.ModKey))
                continue;

            desired = read(context.Record) & mask;
            foundRealWinner = true;
            break;
        }

        if (!foundRealWinner)
            return 0;

        for (ulong remaining = mask; remaining != 0; remaining &= remaining - 1)
        {
            ulong bit = remaining & (~remaining + 1);

            foreach (var context in item.Contexts)
            {
                if (ignoredPlugins.Contains(context.ModKey))
                    continue;

                PluginOverrideNode sourceNode = graph.Nodes[context.ModKey];
                if (sourceNode.Parents.Count == 0)
                    continue;

                bool sourceSet = (read(context.Record) & bit) != 0;
                bool explicitlyChanged = false;

                foreach (PluginOverrideNode parent in sourceNode.Parents)
                {
                    bool parentSet =
                        (read(item.GetRecord(parent.ModKey)) & bit) != 0;
                    if (parentSet != sourceSet)
                    {
                        explicitlyChanged = true;
                        break;
                    }
                }

                if (!explicitlyChanged)
                    continue;

                if (sourceSet)
                    desired |= bit;
                else
                    desired &= ~bit;

                break;
            }
        }

        return desired & mask;
    }
}
