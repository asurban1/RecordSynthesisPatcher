using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using RecordSynthesisPatcher.Core;

namespace RecordSynthesisPatcher.Modules;

// Reconciles collection presence decisions keyed by FormKey. Additions and
// removals are treated symmetrically; the highest-priority plugin that
// explicitly changed a key relative to its parent branch decides its state.
public sealed class MergerModule : PatcherModule, IMergingActionModule
{
    private readonly Dictionary<string, FieldTotals> _totalsByField =
        new(StringComparer.Ordinal);

    public override string Name => "Merge configured record collections";
    public override int Order => 200;

    public void Process<TRecord, TGetter, TEntry>(
        RecordWorkItem<TRecord, TGetter> item,
        MergeField<TRecord, TGetter, TEntry> field)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        MergeChanges<TEntry>? changes = CollectChanges(item, field);
        if (changes is null)
            return;

        TRecord patchRecord = item.GetOrAddOverride();
        field.Clear(patchRecord);

        // Rebuild from the winner so its order and entry payloads remain
        // authoritative, excluding only explicit removal decisions.
        if (field.Read(item.Winner) is { } winningEntries)
        {
            foreach (TEntry entry in winningEntries)
            {
                object? key = field.GetKey(entry);
                if (!field.IsValidKey(key) || !changes.Removals.Contains(key!))
                    field.Add(patchRecord, entry);
            }
        }

        // Independent additions follow winner entries in descending plugin
        // priority, retaining their order inside each source plugin.
        foreach (Addition<TEntry> addition in changes.Additions)
            field.Add(patchRecord, addition.Entry);

        if (!_totalsByField.TryGetValue(field.Name, out FieldTotals? totals))
        {
            totals = new FieldTotals();
            _totalsByField.Add(field.Name, totals);
        }

        totals.UpdatedRecords++;
        totals.AddedEntries += changes.Additions.Count;
        totals.RemovedEntries += changes.Removals.Count;

        LogChanges(
            item, field.Name,
            changes.Additions.Count, changes.Removals.Count);
    }

    public override void Complete(PatcherServices services)
    {
        if (_totalsByField.Count == 0)
            Console.WriteLine("Merging: no collection changes were needed.");

        foreach (var entry in _totalsByField)
        {
            Console.WriteLine(
                $"{entry.Key}: {entry.Value.UpdatedRecords:N0} records updated, " +
                $"{entry.Value.AddedEntries:N0} entries added, " +
                $"{entry.Value.RemovedEntries:N0} entries removed.");
        }
    }

    private static MergeChanges<TEntry>? CollectChanges<TRecord, TGetter, TEntry>(
        RecordWorkItem<TRecord, TGetter> item,
        MergeField<TRecord, TGetter, TEntry> field)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        // With only the original and winner there is no independent branch to
        // reconcile; the winner already contains its own additions/removals.
        if (item.Contexts.Count < 3)
            return null;

        PluginOverrideGraph graph = item.GetGraph();
        var entriesByPlugin = new Dictionary<ModKey, EntryMap<TEntry>>();
        var allKeys = new HashSet<object>();

        foreach (var context in item.Contexts)
        {
            var map = new Dictionary<object, OrderedEntry<TEntry>>();
            if (field.Read(context.Record) is { } entries)
            {
                for (int index = 0; index < entries.Count; index++)
                {
                    TEntry entry = entries[index];
                    object? key = field.GetKey(entry);
                    if (!field.IsValidKey(key))
                        continue;

                    // Presence merging is keyed by FormKey. Retain the first
                    // duplicate for a potential addition and preserve all
                    // winner duplicates during a no-removal rebuild.
                    map.TryAdd(key!, new OrderedEntry<TEntry>(entry, index));
                    allKeys.Add(key!);
                }
            }

            entriesByPlugin.Add(context.ModKey, new EntryMap<TEntry>(map));
        }

        EntryMap<TEntry> winnerMap = entriesByPlugin[item.WinningPlugin];
        var removals = new HashSet<object>();
        var additions = new List<Addition<TEntry>>();
        var contextIndex = new Dictionary<ModKey, int>();
        for (int index = 0; index < item.Contexts.Count; index++)
            contextIndex[item.Contexts[index].ModKey] = index;

        foreach (object key in allKeys)
        {
            bool winnerContains = winnerMap.Entries.ContainsKey(key);
            bool rootContains = entriesByPlugin[graph.Root.ModKey]
                .Entries.ContainsKey(key);
            var activeByPlugin =
                new Dictionary<ModKey, PresenceDecision<TEntry>?>();

            // Carry the active presence decision down every real master path.
            // A descendant supersedes a decision only on the path containing
            // that descendant; sibling branches retain the inherited state.
            for (int sourceIndex = item.Contexts.Count - 1;
                 sourceIndex >= 0;
                 sourceIndex--)
            {
                ModKey sourcePlugin = item.Contexts[sourceIndex].ModKey;
                PluginOverrideNode sourceNode = graph.Nodes[sourcePlugin];
                if (sourceNode.Parents.Count == 0)
                {
                    activeByPlugin[sourcePlugin] = null;
                    continue;
                }

                EntryMap<TEntry> sourceMap = entriesByPlugin[sourcePlugin];
                bool sourceContains = sourceMap.Entries.ContainsKey(key);
                bool matchesParent = false;
                PresenceDecision<TEntry>? inherited = null;

                foreach (PluginOverrideNode parent in sourceNode.Parents)
                {
                    bool parentContains =
                        entriesByPlugin[parent.ModKey].Entries.ContainsKey(key);
                    if (parentContains != sourceContains)
                        continue;

                    matchesParent = true;
                    PresenceDecision<TEntry>? parentDecision =
                        activeByPlugin[parent.ModKey];
                    if (parentDecision is not null &&
                        (inherited is null ||
                         parentDecision.SourceIndex < inherited.SourceIndex))
                    {
                        inherited = parentDecision;
                    }
                }

                sourceMap.Entries.TryGetValue(
                    key, out OrderedEntry<TEntry>? sourceEntry);
                activeByPlugin[sourcePlugin] = matchesParent
                    ? inherited
                    : new PresenceDecision<TEntry>(
                        sourcePlugin,
                        sourceIndex,
                        sourceContains,
                        sourceEntry);
            }

            // Rank states by the priority of the leaf carrying them. A state
            // different from the root wins over a root-valued reversion on an
            // independent branch, but a descendant reversion still suppresses
            // the earlier decision on its own path.
            PresenceDecision<TEntry>? resolved = null;
            int resolvedLeafIndex = int.MaxValue;
            bool resolvedIsRoot = true;
            foreach (PluginOverrideNode leaf in graph.Nodes.Values)
            {
                if (leaf.Children.Count != 0 ||
                    activeByPlugin[leaf.ModKey] is not { } leafDecision)
                    continue;

                int leafIndex = contextIndex[leaf.ModKey];
                bool leafIsRoot = leafDecision.Contains == rootContains;
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

            if (resolved is null || resolved.Contains == winnerContains)
            {
                continue;
            }

            if (resolved.Contains)
            {
                OrderedEntry<TEntry> sourceEntry = resolved.Entry!;
                additions.Add(new Addition<TEntry>(
                    sourceEntry.Entry,
                    resolvedLeafIndex,
                    sourceEntry.Order));
            }
            else
            {
                removals.Add(key);
            }
        }

        if (additions.Count == 0 && removals.Count == 0)
            return null;

        additions.Sort(static (left, right) =>
        {
            int priority = left.SourceIndex.CompareTo(right.SourceIndex);
            return priority != 0
                ? priority
                : left.SourceOrder.CompareTo(right.SourceOrder);
        });

        return new MergeChanges<TEntry>(additions, removals);
    }

    private static void LogChanges<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        string field,
        int additions,
        int removals)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        if (!item.Services.VerboseRecordLogging)
            return;

        Console.WriteLine(
            $"{field}: {item.Winner.FormKey} " +
            $"winner={item.WinningPlugin} " +
            $"added={additions:N0} removed={removals:N0}");
    }

    private sealed record EntryMap<TEntry>(
        Dictionary<object, OrderedEntry<TEntry>> Entries);

    private sealed record OrderedEntry<TEntry>(TEntry Entry, int Order);

    private sealed record PresenceDecision<TEntry>(
        ModKey Plugin,
        int SourceIndex,
        bool Contains,
        OrderedEntry<TEntry>? Entry);

    private sealed record Addition<TEntry>(
        TEntry Entry,
        int SourceIndex,
        int SourceOrder);

    private sealed record MergeChanges<TEntry>(
        List<Addition<TEntry>> Additions,
        HashSet<object> Removals);

    private sealed class FieldTotals
    {
        public int UpdatedRecords { get; set; }
        public int AddedEntries { get; set; }
        public int RemovedEntries { get; set; }
    }
}
