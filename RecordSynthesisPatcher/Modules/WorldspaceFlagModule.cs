using System;
using System.Numerics;
using RecordSynthesisPatcher.Core;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Modules;

// The original WRLD prototype intentionally has narrow semantics: only
// SmallWorld and CannotFastTravel removals are sticky. It never adds flags and
// never touches WRLD major-record flags or unrelated ordinary bits.
public sealed class WorldspaceFlagModule : PatcherModule,
    IRecordModule<IWorldspace, IWorldspaceGetter>
{
    private const Worldspace.Flag StickyRemovalMask =
        Worldspace.Flag.SmallWorld |
        Worldspace.Flag.CannotFastTravel;

    private int _updated;
    private int _flagsRemoved;

    public override string Name =>
        "WRLD: preserve Small World / Cannot Fast Travel removals";
    public override int Order => 200;

    public void Process(RecordWorkItem<IWorldspace, IWorldspaceGetter> item)
    {
        if (!item.Services.Settings.WRLD.FlagsMerge ||
            (item.Winner.Flags & StickyRemovalMask) == 0 ||
            item.Contexts.Count < 3)
            return;

        PluginOverrideGraph graph = item.GetGraph();
        Worldspace.Flag removedAnywhere = 0;

        foreach (PluginOverrideNode node in graph.Nodes.Values)
        {
            Worldspace.Flag childFlags = item.GetRecord(node.ModKey).Flags;
            foreach (PluginOverrideNode parent in node.Parents)
            {
                Worldspace.Flag parentFlags = item.GetRecord(parent.ModKey).Flags;
                removedAnywhere |=
                    parentFlags & ~childFlags & StickyRemovalMask;
            }
        }

        Worldspace.Flag flagsToRemove = item.Winner.Flags & removedAnywhere;
        if (flagsToRemove == 0)
            return;

        item.GetOrAddOverride().Flags &= ~flagsToRemove;
        _updated++;
        _flagsRemoved += BitOperations.PopCount((uint)flagsToRemove);

        if (item.Services.VerboseRecordLogging)
        {
            Console.WriteLine(
                $"WRLD flags: {item.Winner.FormKey} " +
                $"winner={item.WinningPlugin} removed={flagsToRemove}");
        }
    }

    public override void Complete(PatcherServices services)
    {
        if (!services.Settings.WRLD.FlagsMerge)
            return;

        Console.WriteLine(
            $"WRLD flags: {_updated:N0} records updated, " +
            $"{_flagsRemoved:N0} flags removed.");
    }
}
