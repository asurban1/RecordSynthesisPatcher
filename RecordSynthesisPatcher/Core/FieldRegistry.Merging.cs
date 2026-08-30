using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public static partial class FieldRegistry
{
    private static void RegisterMerging(
        ICollection<IFieldBinding> bindings,
        PatcherSettings settings,
        IReadOnlyList<IMergingActionModule> mergers)
    {
        AddLinkedReferencesMerge<IPlacedNpc, IPlacedNpcGetter>(
            bindings, settings.ACHR.LinkedReferencesMerge,
            "ACHR.LinkedReferences", record => record.LinkedReferences,
            record => record.LinkedReferences.Clear(),
            (record, entry) => record.LinkedReferences.Add(entry.DeepCopy()),
            mergers);

        AddLinkedReferencesMerge<IPlacedArrow, IPlacedArrowGetter>(
            bindings, settings.PARW.LinkedReferencesMerge,
            "PARW.LinkedReferences", record => record.LinkedReferences,
            record => record.LinkedReferences.Clear(),
            (record, entry) => record.LinkedReferences.Add(entry.DeepCopy()),
            mergers);

        AddLinkedReferencesMerge<IPlacedBarrier, IPlacedBarrierGetter>(
            bindings, settings.PBAR.LinkedReferencesMerge,
            "PBAR.LinkedReferences", record => record.LinkedReferences,
            record => record.LinkedReferences.Clear(),
            (record, entry) => record.LinkedReferences.Add(entry.DeepCopy()),
            mergers);

        AddLinkedReferencesMerge<IPlacedBeam, IPlacedBeamGetter>(
            bindings, settings.PBEA.LinkedReferencesMerge,
            "PBEA.LinkedReferences", record => record.LinkedReferences,
            record => record.LinkedReferences.Clear(),
            (record, entry) => record.LinkedReferences.Add(entry.DeepCopy()),
            mergers);

        AddLinkedReferencesMerge<IPlacedCone, IPlacedConeGetter>(
            bindings, settings.PCON.LinkedReferencesMerge,
            "PCON.LinkedReferences", record => record.LinkedReferences,
            record => record.LinkedReferences.Clear(),
            (record, entry) => record.LinkedReferences.Add(entry.DeepCopy()),
            mergers);

        AddLinkedReferencesMerge<IPlacedFlame, IPlacedFlameGetter>(
            bindings, settings.PFLA.LinkedReferencesMerge,
            "PFLA.LinkedReferences", record => record.LinkedReferences,
            record => record.LinkedReferences.Clear(),
            (record, entry) => record.LinkedReferences.Add(entry.DeepCopy()),
            mergers);

        AddLinkedReferencesMerge<IPlacedTrap, IPlacedTrapGetter>(
            bindings, settings.PGRE.LinkedReferencesMerge,
            "PGRE.LinkedReferences", record => record.LinkedReferences,
            record => record.LinkedReferences.Clear(),
            (record, entry) => record.LinkedReferences.Add(entry.DeepCopy()),
            mergers);

        AddLinkedReferencesMerge<IPlacedHazard, IPlacedHazardGetter>(
            bindings, settings.PHZD.LinkedReferencesMerge,
            "PHZD.LinkedReferences", record => record.LinkedReferences,
            record => record.LinkedReferences.Clear(),
            (record, entry) => record.LinkedReferences.Add(entry.DeepCopy()),
            mergers);

        AddLinkedReferencesMerge<IPlacedMissile, IPlacedMissileGetter>(
            bindings, settings.PMIS.LinkedReferencesMerge,
            "PMIS.LinkedReferences", record => record.LinkedReferences,
            record => record.LinkedReferences.Clear(),
            (record, entry) => record.LinkedReferences.Add(entry.DeepCopy()),
            mergers);

        AddLinkedReferencesMerge<IPlacedObject, IPlacedObjectGetter>(
            bindings, settings.REFR.LinkedReferencesMerge,
            "REFR.LinkedReferences", record => record.LinkedReferences,
            record => record.LinkedReferences.Clear(),
            (record, entry) => record.LinkedReferences.Add(entry.DeepCopy()),
            mergers);

        AddMerge<IConstructibleObject, IConstructibleObjectGetter, IContainerEntryGetter>(
            bindings, settings.COBJ.ItemsMerge, "COBJ.Items",
            record => record.Items,
            entry => entry.Item.Item.FormKey,
            record => record.Items?.Clear(),
            (record, entry) =>
            {
                record.Items ??= new ExtendedList<ContainerEntry>();
                record.Items.Add(entry.DeepCopy());
            },
            mergers);

        AddMerge<IFaction, IFactionGetter, IRelationGetter>(
            bindings, settings.FACT.RelationsMerge, "FACT.Relations",
            record => record.Relations,
            entry => entry.Target.FormKey,
            record => record.Relations.Clear(),
            (record, entry) => record.Relations.Add(entry.DeepCopy()),
            mergers);

        AddMerge<IPlacedNpc, IPlacedNpcGetter, IFormLinkGetter<ILocationReferenceTypeGetter>>(
            bindings, settings.ACHR.LocationRefTypesMerge, "ACHR.LocationRefTypes",
            record => record.LocationRefTypes,
            entry => entry.FormKey,
            record => record.LocationRefTypes?.Clear(),
            (record, entry) =>
            {
                record.LocationRefTypes ??=
                    new ExtendedList<IFormLinkGetter<ILocationReferenceTypeGetter>>();
                record.LocationRefTypes.Add(
                    new FormLink<ILocationReferenceTypeGetter>(entry.FormKey));
            },
            mergers);

        AddMerge<IArmorAddon, IArmorAddonGetter, IFormLinkGetter<IRaceGetter>>(
            bindings, settings.ARMA.AdditionalRacesMerge, "ARMA.AdditionalRaces",
            record => record.AdditionalRaces,
            entry => entry.FormKey,
            record => record.AdditionalRaces.Clear(),
            (record, entry) => record.AdditionalRaces.Add(
                new FormLink<IRaceGetter>(entry.FormKey)),
            mergers);

        AddMerge<IArmor, IArmorGetter, IFormLinkGetter<IArmorAddonGetter>>(
            bindings, settings.ARMO.ArmatureMerge, "ARMO.Armature",
            record => record.Armature,
            entry => entry.FormKey,
            record => record.Armature.Clear(),
            (record, entry) => record.Armature.Add(
                new FormLink<IArmorAddonGetter>(entry.FormKey)),
            mergers);

        AddMerge<IArmor, IArmorGetter, IFormLinkGetter<IKeywordGetter>>(
            bindings, settings.ARMO.KeywordsMerge, "ARMO.Keywords",
            record => record.Keywords,
            entry => entry.FormKey,
            record => record.Keywords?.Clear(),
            (record, entry) =>
            {
                record.Keywords ??=
                    new ExtendedList<IFormLinkGetter<IKeywordGetter>>();
                record.Keywords.Add(
                    new FormLink<IKeywordGetter>(entry.FormKey));
            },
            mergers);

        AddMerge<ICell, ICellGetter, IFormLinkGetter<IRegionGetter>>(
            bindings, settings.CELL.RegionsMerge, "CELL.Regions",
            record => record.Regions,
            entry => entry.FormKey,
            record => record.Regions?.Clear(),
            (record, entry) =>
            {
                record.Regions ??=
                    new ExtendedList<IFormLinkGetter<IRegionGetter>>();
                record.Regions.Add(
                    new FormLink<IRegionGetter>(entry.FormKey));
            },
            mergers);

        AddMerge<IContainer, IContainerGetter, IContainerEntryGetter>(
            bindings, settings.CONT.ItemsMerge, "CONT.Items",
            record => record.Items,
            entry => entry.Item.Item.FormKey,
            record => record.Items?.Clear(),
            (record, entry) =>
            {
                record.Items ??= new ExtendedList<ContainerEntry>();
                record.Items.Add(entry.DeepCopy());
            },
            mergers);

        AddMerge<IFormList, IFormListGetter, IFormLinkGetter<ISkyrimMajorRecordGetter>>(
            bindings, settings.FLST.ItemsMerge, "FLST.Items",
            record => record.Items,
            entry => entry.FormKey,
            record => record.Items.Clear(),
            (record, entry) => record.Items.Add(
                new FormLink<ISkyrimMajorRecordGetter>(entry.FormKey)),
            mergers);

        AddKeywordMerge<IIngredient, IIngredientGetter>(
            bindings, settings.INGR.KeywordsMerge, "INGR.Keywords",
            record => record.Keywords,
            record => record.Keywords ??=
                new ExtendedList<IFormLinkGetter<IKeywordGetter>>(),
            mergers);

        AddKeywordMerge<IKey, IKeyGetter>(
            bindings, settings.KEYM.KeywordsMerge, "KEYM.Keywords",
            record => record.Keywords,
            record => record.Keywords ??=
                new ExtendedList<IFormLinkGetter<IKeywordGetter>>(),
            mergers);

        AddKeywordMerge<ILocation, ILocationGetter>(
            bindings, settings.LCTN.KeywordsMerge, "LCTN.Keywords",
            record => record.Keywords,
            record => record.Keywords ??=
                new ExtendedList<IFormLinkGetter<IKeywordGetter>>(),
            mergers);

        AddMergeByKey<ILeveledItem, ILeveledItemGetter, ILeveledItemEntryGetter, LeveledEntryKey?>(
            bindings, settings.LVLI.EntriesMerge, "LVLI.Entries",
            record => record.Entries,
            entry => entry.Data is { } data ? LeveledEntryKey.From(data) : null,
            record => record.Entries?.Clear(),
            (record, entry) =>
            {
                record.Entries ??= new ExtendedList<LeveledItemEntry>();
                record.Entries.Add(entry.DeepCopy());
            },
            mergers);

        AddMergeByKey<ILeveledNpc, ILeveledNpcGetter, ILeveledNpcEntryGetter, LeveledEntryKey?>(
            bindings, settings.LVLN.EntriesMerge, "LVLN.Entries",
            record => record.Entries,
            entry => entry.Data is { } data ? LeveledEntryKey.From(data) : null,
            record => record.Entries?.Clear(),
            (record, entry) =>
            {
                record.Entries ??= new ExtendedList<LeveledNpcEntry>();
                record.Entries.Add(entry.DeepCopy());
            },
            mergers);

        AddMergeByKey<ILeveledSpell, ILeveledSpellGetter, ILeveledSpellEntryGetter, LeveledEntryKey?>(
            bindings, settings.LVSP.EntriesMerge, "LVSP.Entries",
            record => record.Entries,
            entry => entry.Data is { } data ? LeveledEntryKey.From(data) : null,
            record => record.Entries?.Clear(),
            (record, entry) =>
            {
                record.Entries ??= new ExtendedList<LeveledSpellEntry>();
                record.Entries.Add(entry.DeepCopy());
            },
            mergers);

        AddMerge<IMagicEffect, IMagicEffectGetter, IFormLinkGetter<IMagicEffectGetter>>(
            bindings, settings.MGEF.CounterEffectsMerge, "MGEF.CounterEffects",
            record => record.CounterEffects,
            entry => entry.FormKey,
            record => record.CounterEffects.Clear(),
            (record, entry) => record.CounterEffects.Add(
                new FormLink<IMagicEffectGetter>(entry.FormKey)),
            mergers);

        AddKeywordMerge<IMagicEffect, IMagicEffectGetter>(
            bindings, settings.MGEF.KeywordsMerge, "MGEF.Keywords",
            record => record.Keywords,
            record => record.Keywords ??=
                new ExtendedList<IFormLinkGetter<IKeywordGetter>>(),
            mergers);

        AddMerge<INpc, INpcGetter, IFormLinkGetter<ISpellRecordGetter>>(
            bindings, settings.NPC_.ActorEffectsMerge, "NPC_.ActorEffect",
            record => record.ActorEffect,
            entry => entry.FormKey,
            record => record.ActorEffect?.Clear(),
            (record, entry) =>
            {
                record.ActorEffect ??=
                    new ExtendedList<IFormLinkGetter<ISpellRecordGetter>>();
                record.ActorEffect.Add(
                    new FormLink<ISpellRecordGetter>(entry.FormKey));
            },
            mergers);

        AddMerge<INpc, INpcGetter, IRankPlacementGetter>(
            bindings, settings.NPC_.FactionsMerge, "NPC_.Factions",
            record => record.Factions,
            entry => entry.Faction.FormKey,
            record => record.Factions.Clear(),
            (record, entry) => record.Factions.Add(entry.DeepCopy()),
            mergers);

        AddMerge<INpc, INpcGetter, IFormLinkGetter<IHeadPartGetter>>(
            bindings, settings.NPC_.HeadPartsMerge, "NPC_.HeadParts",
            record => record.HeadParts,
            entry => entry.FormKey,
            record => record.HeadParts.Clear(),
            (record, entry) => record.HeadParts.Add(
                new FormLink<IHeadPartGetter>(entry.FormKey)),
            mergers);

        AddMerge<INpc, INpcGetter, IContainerEntryGetter>(
            bindings, settings.NPC_.ItemsMerge, "NPC_.Items",
            record => record.Items,
            entry => entry.Item.Item.FormKey,
            record => record.Items?.Clear(),
            (record, entry) =>
            {
                record.Items ??= new ExtendedList<ContainerEntry>();
                record.Items.Add(entry.DeepCopy());
            },
            mergers);

        AddKeywordMerge<INpc, INpcGetter>(
            bindings, settings.NPC_.KeywordsMerge, "NPC_.Keywords",
            record => record.Keywords,
            record => record.Keywords ??=
                new ExtendedList<IFormLinkGetter<IKeywordGetter>>(),
            mergers);

        AddMerge<INpc, INpcGetter, IPerkPlacementGetter>(
            bindings, settings.NPC_.PerksMerge, "NPC_.Perks",
            record => record.Perks,
            entry => entry.Perk.FormKey,
            record => record.Perks?.Clear(),
            (record, entry) =>
            {
                record.Perks ??= new ExtendedList<PerkPlacement>();
                record.Perks.Add(entry.DeepCopy());
            },
            mergers);

        AddMerge<INpc, INpcGetter, IFormLinkGetter<IPackageGetter>>(
            bindings, settings.NPC_.PackagesMerge, "NPC_.Packages",
            record => record.Packages,
            entry => entry.FormKey,
            record => record.Packages.Clear(),
            (record, entry) => record.Packages.Add(
                new FormLink<IPackageGetter>(entry.FormKey)),
            mergers);

        AddMerge<IOutfit, IOutfitGetter, IFormLinkGetter<IOutfitTargetGetter>>(
            bindings, settings.OTFT.ItemsMerge, "OTFT.Items",
            record => record.Items,
            entry => entry.FormKey,
            record => record.Items?.Clear(),
            (record, entry) =>
            {
                record.Items ??=
                    new ExtendedList<IFormLinkGetter<IOutfitTargetGetter>>();
                record.Items.Add(
                    new FormLink<IOutfitTargetGetter>(entry.FormKey));
            },
            mergers);

        AddMerge<IPlacedObject, IPlacedObjectGetter, IFormLinkGetter<IPlacedObjectGetter>>(
            bindings, settings.REFR.LinkedRoomsMerge, "REFR.LinkedRooms",
            record => record.LinkedRooms,
            entry => entry.FormKey,
            record => record.LinkedRooms.Clear(),
            (record, entry) => record.LinkedRooms.Add(
                new FormLink<IPlacedObjectGetter>(entry.FormKey)),
            mergers);

        AddMerge<IPlacedObject, IPlacedObjectGetter, IFormLinkGetter<IPlacedObjectGetter>>(
            bindings, settings.REFR.LitWaterMerge, "REFR.LitWater",
            record => record.LitWater,
            entry => entry.FormKey,
            record => record.LitWater.Clear(),
            (record, entry) => record.LitWater.Add(
                new FormLink<IPlacedObjectGetter>(entry.FormKey)),
            mergers);

        AddMerge<IPlacedObject, IPlacedObjectGetter, IFormLinkGetter<ILocationReferenceTypeGetter>>(
            bindings, settings.REFR.LocationRefTypesMerge, "REFR.LocationRefTypes",
            record => record.LocationRefTypes,
            entry => entry.FormKey,
            record => record.LocationRefTypes?.Clear(),
            (record, entry) =>
            {
                record.LocationRefTypes ??=
                    new ExtendedList<IFormLinkGetter<ILocationReferenceTypeGetter>>();
                record.LocationRefTypes.Add(
                    new FormLink<ILocationReferenceTypeGetter>(entry.FormKey));
            },
            mergers);

        AddKeywordMerge<ISpell, ISpellGetter>(
            bindings, settings.SPEL.KeywordsMerge, "SPEL.Keywords",
            record => record.Keywords,
            record => record.Keywords ??=
                new ExtendedList<IFormLinkGetter<IKeywordGetter>>(),
            mergers);

        RegisterAdditionalKeywordMerging(bindings, settings, mergers);
    }

    private static void AddKeywordMerge<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings,
        bool enabled,
        string name,
        Func<TGetter, IReadOnlyList<IFormLinkGetter<IKeywordGetter>>?> read,
        Func<TRecord, IList<IFormLinkGetter<IKeywordGetter>>> ensureList,
        IReadOnlyList<IMergingActionModule> mergers)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        AddMerge<TRecord, TGetter, IFormLinkGetter<IKeywordGetter>>(
            bindings,
            enabled,
            name,
            read,
            entry => entry.FormKey,
            record => ensureList(record).Clear(),
            (record, entry) => ensureList(record).Add(
                new FormLink<IKeywordGetter>(entry.FormKey)),
            mergers);
    }

    private static void AddLinkedReferencesMerge<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings,
        bool enabled,
        string name,
        System.Func<TGetter, IReadOnlyList<ILinkedReferencesGetter>?> read,
        System.Action<TRecord> clear,
        System.Action<TRecord, ILinkedReferencesGetter> add,
        IReadOnlyList<IMergingActionModule> mergers)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        AddMergeByKey<
            TRecord, TGetter, ILinkedReferencesGetter, LinkedReferenceKey?>(
            bindings,
            enabled,
            name,
            read,
            LinkedReferenceKey.From,
            clear,
            add,
            mergers);
    }
}
