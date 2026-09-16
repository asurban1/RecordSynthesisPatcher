using System;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Core;

internal static class CellOutputValidator
{
    public static void Validate(ISkyrimModGetter patch)
    {
        // Validate existing seed/parent cells as well as explicitly processed
        // cells, after all record modules have finished. Do not silently repair
        // malformed inputs or move children between incompatible hierarchies.
        foreach (var block in patch.Cells)
        foreach (var subBlock in block.SubBlocks)
        foreach (var cell in subBlock.Cells)
        {
            if ((cell.Flags & Cell.Flag.IsInteriorCell) != 0)
                continue;

            throw new InvalidOperationException(
                $"Unsafe CELL output: {cell.FormKey} ({cell.EditorID ?? "no EditorID"}) " +
                "is in an interior CELL group but is missing Is Interior Cell. " +
                "RSP stopped before writing its output. Check the winning input " +
                "and any preceding patcher/seed plugin for this CELL; regenerate " +
                "older RSP outputs rather than using them as seeds. " +
                "Existing output files have not been validated by this run.");
        }
    }
}
