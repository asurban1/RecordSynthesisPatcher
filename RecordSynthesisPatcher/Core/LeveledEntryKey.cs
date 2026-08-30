using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Core;

// A leveled-list entry is identified by its complete LVLO payload rather than
// only its reference. The same form can legitimately appear at different
// levels or counts, and those entries must remain distinct.
internal readonly record struct LeveledEntryKey(
    FormKey Reference,
    short Level,
    short Count,
    short Unknown,
    short Unknown2)
{
    public static LeveledEntryKey From(ILeveledItemEntryDataGetter data) =>
        new(data.Reference.FormKey, data.Level, data.Count, data.Unknown, data.Unknown2);

    public static LeveledEntryKey From(ILeveledNpcEntryDataGetter data) =>
        new(data.Reference.FormKey, data.Level, data.Count, data.Unknown, data.Unknown2);

    public static LeveledEntryKey From(ILeveledSpellEntryDataGetter data) =>
        new(data.Reference.FormKey, data.Level, data.Count, data.Unknown, data.Unknown2);
}
