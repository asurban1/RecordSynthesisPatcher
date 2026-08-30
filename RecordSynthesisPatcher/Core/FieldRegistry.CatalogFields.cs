using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Strings;
using Noggog;
using RecordSynthesisPatcher.Settings;

#pragma warning disable CS8601 // Mutagen exposes required and optional fields through one nullable copy helper.

namespace RecordSynthesisPatcher.Core;

public static partial class FieldRegistry
{
    private static void RegisterCatalogFields(
        ICollection<IFieldBinding> b, PatcherSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddObjectBounds<IActivator, IActivatorGetter>(b, s.ACTI.ObjectBounds, "ACTI.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IAddonNode, IAddonNodeGetter>(b, s.ADDN.ObjectBounds, "ADDN.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IIngestible, IIngestibleGetter>(b, s.ALCH.ObjectBounds, "ALCH.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IAmmunition, IAmmunitionGetter>(b, s.AMMO.ObjectBounds, "AMMO.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IAlchemicalApparatus, IAlchemicalApparatusGetter>(b, s.APPA.ObjectBounds, "APPA.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IArmor, IArmorGetter>(b, s.ARMO.ObjectBounds, "ARMO.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IArtObject, IArtObjectGetter>(b, s.ARTO.ObjectBounds, "ARTO.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IAcousticSpace, IAcousticSpaceGetter>(b, s.ASPC.ObjectBounds, "ASPC.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IBook, IBookGetter>(b, s.BOOK.ObjectBounds, "BOOK.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IContainer, IContainerGetter>(b, s.CONT.ObjectBounds, "CONT.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IDoor, IDoorGetter>(b, s.DOOR.ObjectBounds, "DOOR.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IDualCastData, IDualCastDataGetter>(b, s.DUAL.ObjectBounds, "DUAL.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IObjectEffect, IObjectEffectGetter>(b, s.ENCH.ObjectBounds, "ENCH.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IExplosion, IExplosionGetter>(b, s.EXPL.ObjectBounds, "EXPL.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IFlora, IFloraGetter>(b, s.FLOR.ObjectBounds, "FLOR.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IFurniture, IFurnitureGetter>(b, s.FURN.ObjectBounds, "FURN.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IGrass, IGrassGetter>(b, s.GRAS.ObjectBounds, "GRAS.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IHazard, IHazardGetter>(b, s.HAZD.ObjectBounds, "HAZD.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IIdleMarker, IIdleMarkerGetter>(b, s.IDLM.ObjectBounds, "IDLM.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IIngredient, IIngredientGetter>(b, s.INGR.ObjectBounds, "INGR.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IKey, IKeyGetter>(b, s.KEYM.ObjectBounds, "KEYM.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<ILight, ILightGetter>(b, s.LIGH.ObjectBounds, "LIGH.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<ILeveledItem, ILeveledItemGetter>(b, s.LVLI.ObjectBounds, "LVLI.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<ILeveledNpc, ILeveledNpcGetter>(b, s.LVLN.ObjectBounds, "LVLN.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<ILeveledSpell, ILeveledSpellGetter>(b, s.LVSP.ObjectBounds, "LVSP.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IMiscItem, IMiscItemGetter>(b, s.MISC.ObjectBounds, "MISC.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IMoveableStatic, IMoveableStaticGetter>(b, s.MSTT.ObjectBounds, "MSTT.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<INpc, INpcGetter>(b, s.NPC_.ObjectBounds, "NPC_.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IProjectile, IProjectileGetter>(b, s.PROJ.ObjectBounds, "PROJ.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IScroll, IScrollGetter>(b, s.SCRL.ObjectBounds, "SCRL.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<ISoulGem, ISoulGemGetter>(b, s.SLGM.ObjectBounds, "SLGM.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<ISoundMarker, ISoundMarkerGetter>(b, s.SOUN.ObjectBounds, "SOUN.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<ISpell, ISpellGetter>(b, s.SPEL.ObjectBounds, "SPEL.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IStatic, IStaticGetter>(b, s.STAT.ObjectBounds, "STAT.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<ITalkingActivator, ITalkingActivatorGetter>(b, s.TACT.ObjectBounds, "TACT.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<ITree, ITreeGetter>(b, s.TREE.ObjectBounds, "TREE.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<ITextureSet, ITextureSetGetter>(b, s.TXST.ObjectBounds, "TXST.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);
        AddObjectBounds<IWeapon, IWeaponGetter>(b, s.WEAP.ObjectBounds, "WEAP.ObjectBounds", x => x.ObjectBounds, (x, v) => x.ObjectBounds = v, f);

        AddDescription<IIngestible, IIngestibleGetter>(b, s.ALCH.Description, "ALCH.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IAmmunition, IAmmunitionGetter>(b, s.AMMO.Description, "AMMO.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IAlchemicalApparatus, IAlchemicalApparatusGetter>(b, s.APPA.Description, "APPA.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IArmor, IArmorGetter>(b, s.ARMO.Description, "ARMO.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IActorValueInformation, IActorValueInformationGetter>(b, s.AVIF.Description, "AVIF.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IBook, IBookGetter>(b, s.BOOK.Description, "BOOK.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IClass, IClassGetter>(b, s.CLAS.Description, "CLAS.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<ICollisionLayer, ICollisionLayerGetter>(b, s.COLL.Description, "COLL.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<ILoadScreen, ILoadScreenGetter>(b, s.LSCR.Description, "LSCR.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IMessage, IMessageGetter>(b, s.MESG.Description, "MESG.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IMagicEffect, IMagicEffectGetter>(b, s.MGEF.Description, "MGEF.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IPerk, IPerkGetter>(b, s.PERK.Description, "PERK.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IQuest, IQuestGetter>(b, s.QUST.Description, "QUST.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IRace, IRaceGetter>(b, s.RACE.Description, "RACE.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IScroll, IScrollGetter>(b, s.SCRL.Description, "SCRL.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IShout, IShoutGetter>(b, s.SHOU.Description, "SHOU.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<ISpell, ISpellGetter>(b, s.SPEL.Description, "SPEL.Description", x => x.Description, (x, v) => x.Description = v, f);
        AddDescription<IWeapon, IWeaponGetter>(b, s.WEAP.Description, "WEAP.Description", x => x.Description, (x, v) => x.Description = v, f);

        AddEnableParent<IPlacedNpc, IPlacedNpcGetter>(b, s.ACHR.EnableParent, "ACHR.EnableParent", x => x.EnableParent, (x, v) => x.EnableParent = v, f);
        AddEnableParent<IPlacedArrow, IPlacedArrowGetter>(b, s.PARW.EnableParent, "PARW.EnableParent", x => x.EnableParent, (x, v) => x.EnableParent = v, f);
        AddEnableParent<IPlacedBarrier, IPlacedBarrierGetter>(b, s.PBAR.EnableParent, "PBAR.EnableParent", x => x.EnableParent, (x, v) => x.EnableParent = v, f);
        AddEnableParent<IPlacedBeam, IPlacedBeamGetter>(b, s.PBEA.EnableParent, "PBEA.EnableParent", x => x.EnableParent, (x, v) => x.EnableParent = v, f);
        AddEnableParent<IPlacedCone, IPlacedConeGetter>(b, s.PCON.EnableParent, "PCON.EnableParent", x => x.EnableParent, (x, v) => x.EnableParent = v, f);
        AddEnableParent<IPlacedFlame, IPlacedFlameGetter>(b, s.PFLA.EnableParent, "PFLA.EnableParent", x => x.EnableParent, (x, v) => x.EnableParent = v, f);
        AddEnableParent<IPlacedTrap, IPlacedTrapGetter>(b, s.PGRE.EnableParent, "PGRE.EnableParent", x => x.EnableParent, (x, v) => x.EnableParent = v, f);
        AddEnableParent<IPlacedHazard, IPlacedHazardGetter>(b, s.PHZD.EnableParent, "PHZD.EnableParent", x => x.EnableParent, (x, v) => x.EnableParent = v, f);
        AddEnableParent<IPlacedMissile, IPlacedMissileGetter>(b, s.PMIS.EnableParent, "PMIS.EnableParent", x => x.EnableParent, (x, v) => x.EnableParent = v, f);
        AddEnableParent<IPlacedObject, IPlacedObjectGetter>(b, s.REFR.EnableParent, "REFR.EnableParent", x => x.EnableParent, (x, v) => x.EnableParent = v, f);

        AddScale<IPlacedArrow, IPlacedArrowGetter>(b, s.PARW.Scale, "PARW.Scale", x => x.Scale, (x, v) => x.Scale = v, f);
        AddScale<IPlacedBarrier, IPlacedBarrierGetter>(b, s.PBAR.Scale, "PBAR.Scale", x => x.Scale, (x, v) => x.Scale = v, f);
        AddScale<IPlacedBeam, IPlacedBeamGetter>(b, s.PBEA.Scale, "PBEA.Scale", x => x.Scale, (x, v) => x.Scale = v, f);
        AddScale<IPlacedCone, IPlacedConeGetter>(b, s.PCON.Scale, "PCON.Scale", x => x.Scale, (x, v) => x.Scale = v, f);
        AddScale<IPlacedFlame, IPlacedFlameGetter>(b, s.PFLA.Scale, "PFLA.Scale", x => x.Scale, (x, v) => x.Scale = v, f);
        AddScale<IPlacedTrap, IPlacedTrapGetter>(b, s.PGRE.Scale, "PGRE.Scale", x => x.Scale, (x, v) => x.Scale = v, f);
        AddScale<IPlacedHazard, IPlacedHazardGetter>(b, s.PHZD.Scale, "PHZD.Scale", x => x.Scale, (x, v) => x.Scale = v, f);
        AddScale<IPlacedMissile, IPlacedMissileGetter>(b, s.PMIS.Scale, "PMIS.Scale", x => x.Scale, (x, v) => x.Scale = v, f);

        AddForward<ILeveledItem, ILeveledItemGetter, Percent>(
            b, s.LVLI.ChanceNone, "LVLI.ChanceNone",
            x => x.ChanceNone, (x, v) => x.ChanceNone = v,
            _ => false, f);
        AddForward<ILeveledNpc, ILeveledNpcGetter, Percent>(
            b, s.LVLN.ChanceNone, "LVLN.ChanceNone",
            x => x.ChanceNone, (x, v) => x.ChanceNone = v,
            _ => false, f);
        AddForward<ILeveledSpell, ILeveledSpellGetter, Percent>(
            b, s.LVSP.ChanceNone, "LVSP.ChanceNone",
            x => x.ChanceNone, (x, v) => x.ChanceNone = v,
            _ => false, f);

        AddForwardLink<IConstructibleObject, IConstructibleObjectGetter>(
            b, s.COBJ.CreatedObject, "COBJ.CreatedObject",
            x => x.CreatedObject.FormKey,
            (x, v) => x.CreatedObject.SetTo(v), f);

        AddLock(b, s.REFR.Lock, f);
    }

    private static void AddObjectBounds<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings, bool enabled, string name,
        Func<TGetter, IObjectBoundsGetter?> read,
        Action<TRecord, ObjectBounds?> write,
        IReadOnlyList<IForwardingActionModule> forwarders)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        AddForward<TRecord, TGetter, IObjectBoundsGetter?>(
            bindings, enabled, name, read,
            (record, value) => write(record, value?.DeepCopy()),
            _ => false, forwarders,
            DelegateComparer<IObjectBoundsGetter?>.Create(SameObjectBounds));
    }

    private static void AddDescription<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings, bool enabled, string name,
        Func<TGetter, ITranslatedStringGetter?> read,
        Action<TRecord, TranslatedString?> write,
        IReadOnlyList<IForwardingActionModule> forwarders)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        AddForward<TRecord, TGetter, ITranslatedStringGetter?>(
            bindings, enabled, name, read,
            (record, value) => write(record, value?.DeepCopy()),
            _ => false, forwarders,
            DelegateComparer<ITranslatedStringGetter?>.Create(SameDescription));
    }

    private static void AddEnableParent<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings, bool enabled, string name,
        Func<TGetter, IEnableParentGetter?> read,
        Action<TRecord, EnableParent?> write,
        IReadOnlyList<IForwardingActionModule> forwarders)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        AddForward<TRecord, TGetter, IEnableParentGetter?>(
            bindings, enabled, name, read,
            (record, value) => write(record, value?.DeepCopy()),
            _ => false, forwarders,
            DelegateComparer<IEnableParentGetter?>.Create(SameEnableParent));
    }

    private static void AddScale<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings, bool enabled, string name,
        Func<TGetter, float?> read,
        Action<TRecord, float?> write,
        IReadOnlyList<IForwardingActionModule> forwarders)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        AddForward(
            bindings, enabled, name, read, write,
            _ => false, forwarders);
    }

    private static void AddLock(
        ICollection<IFieldBinding> bindings, bool enabled,
        IReadOnlyList<IForwardingActionModule> forwarders)
    {
        AddForward<IPlacedObject, IPlacedObjectGetter, ILockDataGetter?>(
            bindings, enabled, "REFR.Lock", x => x.Lock,
            (record, value) => record.Lock = value?.DeepCopy(),
            _ => false, forwarders,
            DelegateComparer<ILockDataGetter?>.Create(SameLock));
    }

    private static bool SameObjectBounds(
        IObjectBoundsGetter? left, IObjectBoundsGetter? right) =>
        ReferenceEquals(left, right) ||
        left is not null && right is not null &&
        left.First.Equals(right.First) &&
        left.Second.Equals(right.Second);

    private static bool SameDescription(
        ITranslatedStringGetter? left, ITranslatedStringGetter? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;

        // Compare the xEdit-visible value. Mutagen may represent identical
        // visible text with different localized-string backing stores; those
        // loader details are not a DNAM conflict. Ordinal comparison still
        // preserves capitalization and punctuation changes exactly.
        return string.Equals(left.String, right.String, StringComparison.Ordinal);
    }

    private static bool SameEnableParent(
        IEnableParentGetter? left, IEnableParentGetter? right) =>
        ReferenceEquals(left, right) ||
        left is not null && right is not null &&
        left.Reference.FormKey == right.Reference.FormKey &&
        left.Flags == right.Flags &&
        left.Versioning == right.Versioning &&
        left.Unknown.Equals(right.Unknown);

    private static bool SameLock(
        ILockDataGetter? left, ILockDataGetter? right) =>
        ReferenceEquals(left, right) ||
        left is not null && right is not null &&
        left.Level == right.Level &&
        left.Key.FormKey == right.Key.FormKey &&
        left.Flags == right.Flags &&
        left.Unused.Equals(right.Unused) &&
        left.Unused2.Equals(right.Unused2);

    private sealed class DelegateComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _equals;
        private DelegateComparer(Func<T, T, bool> equals) => _equals = equals;
        public static DelegateComparer<T> Create(Func<T, T, bool> equals) => new(equals);
        public bool Equals(T? left, T? right) => _equals(left!, right!);
        public int GetHashCode(T value) => 0;
    }
}

