using System;
using System.Collections.Generic;
using System.Numerics;
using RecordSynthesisPatcher.Core;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Modules;

public sealed class RefrFlagModule : PatcherModule,
    IRecordModule<IPlacedObject, IPlacedObjectGetter>
{
    private const int HiddenFromLocalMap = 0x00000200;
    private const int Persistent = 0x00000400;
    private const int InitiallyDisabled = 0x00000800;
    private const int VisibleWhenDistant = 0x00008000;
    private const int ReflectedByAutoWater = 0x10000000;
    private const int DontHavokSettle = 0x20000000;
    private const int NoRespawn = 0x40000000;

    private const int MergeMask =
        HiddenFromLocalMap |
        Persistent |
        InitiallyDisabled |
        VisibleWhenDistant |
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
        if (!item.Services.Settings.REFR.FlagsMerge ||
            item.Contexts.Count < 2)
            return;

        if (item.Contexts.Count == 2 &&
            !_ignoredSourcePlugins.Contains(item.WinningPlugin))
            return;

        PluginOverrideGraph graph = item.GetGraph(_ignoredSourcePlugins);
        int desired = (int)BranchFlagMerger.Resolve(
            item,
            graph,
            _ignoredSourcePlugins,
            record => (uint)record.MajorRecordFlagsRaw,
            (uint)MergeMask);

        int winner = item.Winner.MajorRecordFlagsRaw;
        int added = (desired & MergeMask) & ~(winner & MergeMask);
        int removed = (winner & MergeMask) & ~(desired & MergeMask);
        if (added == 0 && removed == 0)
            return;

        item.GetOrAddOverride().MajorRecordFlagsRaw =
            (winner & ~MergeMask) | (desired & MergeMask);

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
