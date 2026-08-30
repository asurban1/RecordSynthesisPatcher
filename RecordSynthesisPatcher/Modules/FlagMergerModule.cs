using System;
using System.Collections.Generic;
using System.Numerics;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using RecordSynthesisPatcher.Core;

namespace RecordSynthesisPatcher.Modules;

// Resolves every registered flag bit independently. An explicit addition and
// an explicit removal carry equal weight; a true descendant may supersede the
// corresponding decision made by its master.
public sealed class FlagMergerModule : PatcherModule, IFlagMergingActionModule
{
    private static readonly IReadOnlySet<ModKey> NoIgnoredPlugins =
        new HashSet<ModKey>();

    private readonly Dictionary<string, FieldTotals> _totalsByField =
        new(StringComparer.Ordinal);

    public override string Name => "Merge configured flag fields";
    public override int Order => 200;

    public void Process<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        FlagMergeField<TRecord, TGetter> field)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        if (item.Contexts.Count < 3)
            return;

        ulong winner = field.Read(item.Winner);
        ulong desired = BranchFlagMerger.Resolve(
            item,
            item.GetGraph(),
            NoIgnoredPlugins,
            field.Read,
            field.Mask);

        ulong added = (desired & field.Mask) & ~(winner & field.Mask);
        ulong removed = (winner & field.Mask) & ~(desired & field.Mask);
        if (added == 0 && removed == 0)
            return;

        field.Write(
            item.GetOrAddOverride(),
            (winner & ~field.Mask) | (desired & field.Mask));

        if (!_totalsByField.TryGetValue(field.Name, out FieldTotals? totals))
        {
            totals = new FieldTotals();
            _totalsByField.Add(field.Name, totals);
        }

        totals.UpdatedRecords++;
        totals.AddedFlags += BitOperations.PopCount(added);
        totals.RemovedFlags += BitOperations.PopCount(removed);

        if (item.Services.VerboseRecordLogging)
        {
            Console.WriteLine(
                $"{field.Name}: {item.Winner.FormKey} " +
                $"winner={item.WinningPlugin} " +
                $"added=0x{added:X} removed=0x{removed:X}");
        }
    }

    public override void Complete(PatcherServices services)
    {
        foreach (var entry in _totalsByField)
        {
            Console.WriteLine(
                $"{entry.Key}: {entry.Value.UpdatedRecords:N0} records updated, " +
                $"{entry.Value.AddedFlags:N0} flags added, " +
                $"{entry.Value.RemovedFlags:N0} flags removed.");
        }
    }

    private sealed class FieldTotals
    {
        public int UpdatedRecords { get; set; }
        public int AddedFlags { get; set; }
        public int RemovedFlags { get; set; }
    }
}
