using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Skyrim;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public static partial class FieldRegistry
{
    private static void RegisterFlagMerging(
        ICollection<IFieldBinding> bindings,
        PatcherSettings settings,
        IReadOnlyList<IFlagMergingActionModule> mergers)
    {
        AddFlagMerge<ICell, ICellGetter>(
            bindings,
            settings.CELL.FlagsMerge,
            "CELL.Flags",
            record => (ulong)record.Flags,
            (record, value) => record.Flags = (Cell.Flag)value,
            EnumMask<Cell.Flag>(),
            mergers);

        AddFlagMerge<INpc, INpcGetter>(
            bindings,
            settings.NPC_.ConfigurationFlagsMerge,
            "NPC_.Configuration.Flags",
            record => (ulong)record.Configuration.Flags,
            (record, value) =>
                record.Configuration.Flags = (NpcConfiguration.Flag)value,
            EnumMask<NpcConfiguration.Flag>(),
            mergers);

        AddFlagMerge<IWorldspace, IWorldspaceGetter>(
            bindings,
            settings.WRLD.FlagsMerge,
            "WRLD.RecordFlags",
            record => (ulong)record.MajorFlags,
            (record, value) =>
                record.MajorFlags = (Worldspace.MajorFlag)value,
            (ulong)Worldspace.MajorFlag.CanNotWait,
            mergers);

        RegisterBipedBodyTemplateFlags(bindings, settings, mergers);
    }

    private static ulong EnumMask<TEnum>() where TEnum : struct, Enum
    {
        ulong mask = 0;
        foreach (TEnum value in Enum.GetValues<TEnum>())
            mask |= Convert.ToUInt64(value);
        return mask;
    }
}
