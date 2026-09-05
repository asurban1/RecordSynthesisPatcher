using System;
using System.Numerics;
using RecordSynthesisPatcher.Core;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Modules;

// WRLD flag handling intentionally has narrow semantics: only surviving
// SmallWorld and CannotFastTravel removals are preserved. It never adds flags
// and never touches WRLD major-record flags or unrelated ordinary bits.
public sealed class WorldspaceFlagModule : PatcherModule,
    IRecordModule<IWorldspace, IWorldspaceGetter>,
    IRecordFinalizer<IWorldspace, IWorldspaceGetter>
{
    private const Worldspace.Flag RemovalMask =
        Worldspace.Flag.SmallWorld |
        Worldspace.Flag.CannotFastTravel;

    private int _updated;
    private int _flagsRemoved;

    public override string Name =>
        "WRLD: preserve Small World / Cannot Fast Travel removals";
    public override int Order => 200;

    public void Process(RecordWorkItem<IWorldspace, IWorldspaceGetter> item)
    {
        // Ordinary actions run first. Surviving WRLD removals are enforced
        // after every configured field has finished writing the record.
    }

    public void FinalizeRecord(
        RecordWorkItem<IWorldspace, IWorldspaceGetter> item)
    {
        if (!item.Services.Settings.WRLD.FlagsMerge ||
            (item.Winner.Flags & RemovalMask) == 0 ||
            item.Contexts.Count < 3)
            return;

        ulong desired = BranchFlagMerger.Resolve(
            item,
            item.GetGraph(),
            record => (ulong)record.Flags,
            (ulong)RemovalMask);
        Worldspace.Flag flagsToRemove = item.Winner.Flags &
            ~(Worldspace.Flag)desired & RemovalMask;
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
