using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins.Records;

namespace RecordSynthesisPatcher.Core;

// Resolves each managed bit through the same multi-parent candidate model used
// by scalar and collection fields. Bits are independent decisions: a removal
// and an addition have equal weight, and a root-valued branch cannot erase a
// meaningful state that still survives elsewhere.
public static class BranchFlagMerger
{
    public static ulong Resolve<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        PluginOverrideGraph graph,
        Func<TGetter, ulong> read,
        ulong mask)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        if (graph.WinnerFirstKeys.Length == 0)
            return 0;

        BranchResolutionTopology topology = ReferenceEquals(
                graph, item.GetGraph())
            ? item.GetResolutionTopology()
            : BranchResolutionTopology.Create(
                graph.WinnerFirstKeys,
                graph.WinnerFirstKeys[0],
                graph);
        ulong desired = read(item.GetRecord(graph.WinnerFirstKeys[0])) & mask;

        for (ulong remaining = mask; remaining != 0; remaining &= remaining - 1)
        {
            ulong bit = remaining & (~remaining + 1);
            BranchValueResolution<bool> resolution = BranchValueResolver.Resolve(
                topology,
                plugin => (read(item.GetRecord(plugin)) & bit) != 0,
                EqualityComparer<bool>.Default);

            if (resolution.Status != BranchValueResolutionStatus.Selected)
                continue;

            if (resolution.Value)
                desired |= bit;
            else
                desired &= ~bit;
        }

        return desired & mask;
    }
}
