using System;
using System.Collections.Generic;
using System.Numerics;
using RecordSynthesisPatcher.Core;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Modules;

public sealed class RefrFlagModule : PatcherModule,
    IRecordModule<IPlacedObject, IPlacedObjectGetter>,
    IRecordFinalizer<IPlacedObject, IPlacedObjectGetter>
{
    private const int HiddenFromLocalMap = 0x00000200;
    private const int Persistent = 0x00000400;
    private const int InitiallyDisabled = 0x00000800;
    private const int VisibleWhenDistant = 0x00008000;
    private const int IsFullLod = 0x00010000;
    private const int ReflectedByAutoWater = 0x10000000;
    private const int DontHavokSettle = 0x20000000;
    private const int NoRespawn = 0x40000000;

    private const int MergeMask =
        HiddenFromLocalMap |
        Persistent |
        InitiallyDisabled |
        VisibleWhenDistant |
        IsFullLod |
        ReflectedByAutoWater |
        DontHavokSettle |
        NoRespawn;

    private IReadOnlySet<ModKey> _ignoredSourcePlugins = null!;
    private int _updated;
    private int _flagsAdded;
    private int _flagsRemoved;

    public override string Name => "Merge important REFR record flags";
    public override int Order => 200;

    public override void Initialize(PatcherServices services)
    {
        _ignoredSourcePlugins = new HashSet<ModKey>
        {
            services.PatchMod.ModKey,
        };
    }

    public void Process(RecordWorkItem<IPlacedObject, IPlacedObjectGetter> item)
    {
        // REFR flags are enforced after every action has had an opportunity to
        // create the output override.
    }

    public void FinalizeRecord(
        RecordWorkItem<IPlacedObject, IPlacedObjectGetter> item)
    {
        if (!item.Services.Settings.REFR.FlagsMerge ||
            item.Contexts.Count < 2)
            return;

        int winner = item.Winner.MajorRecordFlagsRaw;
        int desiredManaged = winner & MergeMask;

        bool hasIndependentBranches =
            item.Contexts.Count > 2 ||
            _ignoredSourcePlugins.Contains(item.WinningPlugin);

        if (hasIndependentBranches)
        {
            PluginOverrideGraph graph = item.GetGraph(_ignoredSourcePlugins);
            desiredManaged = (int)BranchFlagMerger.Resolve(
                item,
                graph,
                record => (uint)record.MajorRecordFlagsRaw,
                (uint)MergeMask) & MergeMask;
        }

        bool differsFromWinner =
            desiredManaged != (winner & MergeMask);
        IPlacedObject? patchRecord = item.ExistingOverride;

        if (!differsFromWinner && patchRecord is null)
            return;

        patchRecord ??= item.GetOrAddOverride();

        int before = patchRecord.MajorRecordFlagsRaw;
        int after = (before & ~MergeMask) | desiredManaged;
        if (before == after)
            return;

        int added = (after & MergeMask) & ~(before & MergeMask);
        int removed = (before & MergeMask) & ~(after & MergeMask);
        patchRecord.MajorRecordFlagsRaw = after;

        _updated++;
        _flagsAdded += BitOperations.PopCount((uint)added);
        _flagsRemoved += BitOperations.PopCount((uint)removed);

        if (item.Services.VerboseRecordLogging)
        {
            Console.WriteLine(
                $"REFR flags: {item.Winner.FormKey} " +
                $"winner={item.WinningPlugin} " +
                $"added=0x{added:X8} removed=0x{removed:X8}");
        }
    }

    public override void Complete(PatcherServices services)
    {
        if (!services.Settings.REFR.FlagsMerge)
            return;

        Console.WriteLine(
            $"REFR flags: {_updated:N0} records updated, " +
            $"{_flagsAdded:N0} flags added, " +
            $"{_flagsRemoved:N0} flags removed.");
    }
}
