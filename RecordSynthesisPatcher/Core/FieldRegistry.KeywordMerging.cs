using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public static partial class FieldRegistry
{
    // Keyword collections share FormKey identity and the same ancestry-aware
    // addition/removal behavior on every compatible record signature.
    private static void RegisterAdditionalKeywordMerging(
        ICollection<IFieldBinding> bindings,
        PatcherSettings settings,
        IReadOnlyList<IMergingActionModule> mergers)
    {
        AddKeywordMerge<IActivator, IActivatorGetter>(
            bindings, settings.ACTI.KeywordsMerge, "ACTI.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<IIngestible, IIngestibleGetter>(
            bindings, settings.ALCH.KeywordsMerge, "ALCH.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<IAmmunition, IAmmunitionGetter>(
            bindings, settings.AMMO.KeywordsMerge, "AMMO.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<IBook, IBookGetter>(
            bindings, settings.BOOK.KeywordsMerge, "BOOK.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<IFlora, IFloraGetter>(
            bindings, settings.FLOR.KeywordsMerge, "FLOR.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<IFurniture, IFurnitureGetter>(
            bindings, settings.FURN.KeywordsMerge, "FURN.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<IMiscItem, IMiscItemGetter>(
            bindings, settings.MISC.KeywordsMerge, "MISC.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<IRace, IRaceGetter>(
            bindings, settings.RACE.KeywordsMerge, "RACE.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<IScroll, IScrollGetter>(
            bindings, settings.SCRL.KeywordsMerge, "SCRL.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<ISoulGem, ISoulGemGetter>(
            bindings, settings.SLGM.KeywordsMerge, "SLGM.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<ITalkingActivator, ITalkingActivatorGetter>(
            bindings, settings.TACT.KeywordsMerge, "TACT.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);

        AddKeywordMerge<IWeapon, IWeaponGetter>(
            bindings, settings.WEAP.KeywordsMerge, "WEAP.Keywords",
            record => record.Keywords,
            record => record.Keywords ??= NewKeywordList(), mergers);
    }

    private static ExtendedList<IFormLinkGetter<IKeywordGetter>> NewKeywordList() =>
        new();
}
