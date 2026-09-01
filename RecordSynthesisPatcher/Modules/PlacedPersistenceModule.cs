using System;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using RecordSynthesisPatcher.Core;

namespace RecordSynthesisPatcher.Modules;

// Persistent placed records belong in a cell's persistent collection. Merely
// changing the record-header bit leaves the override under the winner's old
// cell path, which produces a structurally inconsistent plugin. Run last so
// the already-completed output record can be moved without losing any edits.
public sealed class PlacedPersistenceModule : PatcherModule,
    IRecordModule<IPlacedObject, IPlacedObjectGetter>,
    IRecordFinalizer<IPlacedObject, IPlacedObjectGetter>,
    IRecordModule<IPlacedNpc, IPlacedNpcGetter>,
    IRecordFinalizer<IPlacedNpc, IPlacedNpcGetter>
{
    private const int Persistent = 0x00000400;

    private int _refrRelocated;
    private int _achrRelocated;
    private int _failed;

    public override string Name =>
        "Keep placed-record persistence and cell placement consistent";

    public override int Order => 1_000;

    public void Process(
        RecordWorkItem<IPlacedObject, IPlacedObjectGetter> item)
    {
        // Relocation must happen after all ordinary actions and flag finalizers.
    }

    public void Process(
        RecordWorkItem<IPlacedNpc, IPlacedNpcGetter> item)
    {
        // Relocation must happen after all ordinary actions and flag finalizers.
    }

    public void FinalizeRecord(
        RecordWorkItem<IPlacedObject, IPlacedObjectGetter> item)
    {
        if (RelocateIfNeeded(item, "REFR"))
            _refrRelocated++;
    }

    public void FinalizeRecord(
        RecordWorkItem<IPlacedNpc, IPlacedNpcGetter> item)
    {
        if (RelocateIfNeeded(item, "ACHR"))
            _achrRelocated++;
    }

    public override void Complete(PatcherServices services)
    {
        Console.WriteLine(
            $"Placed persistence: {_refrRelocated:N0} REFR and " +
            $"{_achrRelocated:N0} ACHR records relocated; " +
            $"{_failed:N0} unresolved destinations.");
    }

    private bool RelocateIfNeeded<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        string signature)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        TRecord? patchRecord = item.ExistingOverride;
        if (patchRecord is null)
            return false;

        bool winnerPersistent = IsPersistent(item.Winner);
        bool outputPersistent = IsPersistent(patchRecord);
        if (winnerPersistent == outputPersistent)
            return false;

        IModContext<ISkyrimMod, ISkyrimModGetter, ICell, ICellGetter>?
            destinationSource = FindDestinationCell(item, outputPersistent);
        bool hasCurrentCell = item.Contexts[0]
            .TryGetParentContext<ICell, ICellGetter>(out var currentSource);
        if (destinationSource is null || !hasCurrentCell)
        {
            _failed++;
            Console.WriteLine(
                $"WARNING: {signature} {item.Winner.FormKey} changed " +
                $"Persistent={outputPersistent}, but its destination CELL " +
                "could not be resolved. The record was left in its original " +
                "output collection.");
            return false;
        }

        IModContext<ISkyrimMod, ISkyrimModGetter, ICell, ICellGetter>
            winningCellContext = item.Services.State.LinkCache
                .ResolveContext<ICell, ICellGetter>(
                    destinationSource.Record.FormKey);
        ICell destinationCell =
            winningCellContext.GetOrAddAsOverride(item.Services.PatchMod);
        ICell currentCell =
            currentSource!.GetOrAddAsOverride(item.Services.PatchMod);

        // The override was originally created through the winning record's
        // context. Remove it from that cell only, then retain the same mutable
        // instance in the collection implied by its final flag.
        currentCell.Remove<TRecord>(patchRecord.FormKey);
        if (outputPersistent)
            destinationCell.Persistent.Add(patchRecord);
        else
            destinationCell.Temporary.Add(patchRecord);

        if (item.Services.VerboseRecordLogging)
        {
            Console.WriteLine(
                $"{signature} persistence: {item.Winner.FormKey} " +
                $"winner={item.WinningPlugin} persistent={outputPersistent} " +
                $"cell={destinationCell.FormKey}");
        }

        return true;
    }

    private static
        IModContext<ISkyrimMod, ISkyrimModGetter, ICell, ICellGetter>?
        FindDestinationCell<TRecord, TGetter>(
            RecordWorkItem<TRecord, TGetter> item,
            bool persistent)
        where TRecord : class, IMajorRecord, TGetter, IPlaced
        where TGetter : class, IMajorRecordGetter, IPlacedGetter
    {
        foreach (var context in item.Contexts)
        {
            if (context.ModKey == item.Services.PatchMod.ModKey ||
                IsPersistent(context.Record) != persistent)
            {
                continue;
            }

            if (context.TryGetParentContext<ICell, ICellGetter>(
                    out var cellContext))
            {
                return cellContext;
            }
        }

        return null;
    }

    private static bool IsPersistent(IMajorRecordGetter record) =>
        (record.MajorRecordFlagsRaw & Persistent) != 0;
}
