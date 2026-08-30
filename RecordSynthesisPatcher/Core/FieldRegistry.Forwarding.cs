using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public static partial class FieldRegistry
{
    private static void RegisterForwarding(
        ICollection<IFieldBinding> bindings,
        PatcherSettings settings,
        IReadOnlyList<IForwardingActionModule> forwarders)
    {
        RegisterAchr(bindings, settings.ACHR, forwarders);
        RegisterArma(bindings, settings.ARMA, forwarders);
        RegisterArmo(bindings, settings.ARMO, forwarders);
        RegisterAspc(bindings, settings.ASPC, forwarders);
        RegisterCell(bindings, settings.CELL, forwarders);
        RegisterCont(bindings, settings.CONT, forwarders);
        RegisterFact(bindings, settings.FACT, forwarders);
        RegisterFlst(bindings, settings.FLST, forwarders);
        RegisterGlob(bindings, settings.GLOB, forwarders);
        RegisterIngr(bindings, settings.INGR, forwarders);
        RegisterIpct(bindings, settings.IPCT, forwarders);
        RegisterKeym(bindings, settings.KEYM, forwarders);
        RegisterLctn(bindings, settings.LCTN, forwarders);
        RegisterLvli(bindings, settings.LVLI, forwarders);
        RegisterLvln(bindings, settings.LVLN, forwarders);
        RegisterMgef(bindings, settings.MGEF, forwarders);
        RegisterNpc(bindings, settings.NPC_, forwarders);
        RegisterOtft(bindings, settings.OTFT, forwarders);
        RegisterRegn(bindings, settings.REGN, forwarders);
        RegisterRefr(bindings, settings.REFR, forwarders);
        RegisterSndr(bindings, settings.SNDR, forwarders);
        RegisterSpel(bindings, settings.SPEL, forwarders);
        RegisterWrld(bindings, settings.WRLD, forwarders);
        RegisterCatalogEditorIds(bindings, settings, forwarders);
        RegisterCatalogFields(bindings, settings, forwarders);
        RegisterPortableSounds(bindings, settings, forwarders);
        RegisterAmbientColors(bindings, settings, forwarders);
        RegisterQueuedForwarding(bindings, settings, forwarders);
    }

    private static void RegisterAchr(
        ICollection<IFieldBinding> b, AchrForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IPlacedNpc, IPlacedNpcGetter>(b, s.EditorID, "ACHR", f);
        AddForwardLink(b, s.Base, "ACHR.Base", (IPlacedNpcGetter r) => r.Base.FormKey, (IPlacedNpc r, FormKey v) => r.Base.SetTo(v), f);
        AddForward(b, s.Count, "ACHR.Count", (IPlacedNpcGetter r) => r.Count, (IPlacedNpc r, int? v) => r.Count = v, IsDefault, f);
        AddForwardLink(b, s.Emittance, "ACHR.Emittance", (IPlacedNpcGetter r) => r.Emittance.FormKey, (IPlacedNpc r, FormKey v) => r.Emittance.SetTo(v), f);
        AddForwardLink(b, s.EncounterZone, "ACHR.EncounterZone", (IPlacedNpcGetter r) => r.EncounterZone.FormKey, (IPlacedNpc r, FormKey v) => r.EncounterZone.SetTo(v), f);
        AddForward(b, s.FactionRank, "ACHR.FactionRank", (IPlacedNpcGetter r) => r.FactionRank, (IPlacedNpc r, int? v) => r.FactionRank = v, IsDefault, f);
        AddForward(b, s.FavorCost, "ACHR.FavorCost", (IPlacedNpcGetter r) => r.FavorCost, (IPlacedNpc r, float? v) => r.FavorCost = v, IsDefault, f);
        AddForward(b, s.HeadTrackingWeight, "ACHR.HeadTrackingWeight", (IPlacedNpcGetter r) => r.HeadTrackingWeight, (IPlacedNpc r, float? v) => r.HeadTrackingWeight = v, IsDefault, f);
        AddForward(b, s.Health, "ACHR.Health", (IPlacedNpcGetter r) => r.Health, (IPlacedNpc r, float? v) => r.Health = v, IsDefault, f);
        AddForwardLink(b, s.Horse, "ACHR.Horse", (IPlacedNpcGetter r) => r.Horse.FormKey, (IPlacedNpc r, FormKey v) => r.Horse.SetTo(v), f);
        AddForward(b, s.IsIgnoredBySandbox, "ACHR.IsIgnoredBySandbox", (IPlacedNpcGetter r) => r.IsIgnoredBySandbox, (IPlacedNpc r, bool v) => r.IsIgnoredBySandbox = v, IsDefault, f);
        AddForward(b, s.IsIgnoredBySandbox2, "ACHR.IsIgnoredBySandbox2", (IPlacedNpcGetter r) => r.IsIgnoredBySandbox2, (IPlacedNpc r, bool v) => r.IsIgnoredBySandbox2 = v, IsDefault, f);
        AddForward(b, s.LevelModifier, "ACHR.LevelModifier", (IPlacedNpcGetter r) => r.LevelModifier, (IPlacedNpc r, Level? v) => r.LevelModifier = v, IsDefault, f);
        AddForwardLink(b, s.LocationReference, "ACHR.LocationReference", (IPlacedNpcGetter r) => r.LocationReference.FormKey, (IPlacedNpc r, FormKey v) => r.LocationReference.SetTo(v), f);
        AddForwardLink(b, s.MerchantContainer, "ACHR.MerchantContainer", (IPlacedNpcGetter r) => r.MerchantContainer.FormKey, (IPlacedNpc r, FormKey v) => r.MerchantContainer.SetTo(v), f);
        AddForwardLink(b, s.MultiBoundReference, "ACHR.MultiBoundReference", (IPlacedNpcGetter r) => r.MultiBoundReference.FormKey, (IPlacedNpc r, FormKey v) => r.MultiBoundReference.SetTo(v), f);
        AddForwardLink(b, s.Owner, "ACHR.Owner", (IPlacedNpcGetter r) => r.Owner.FormKey, (IPlacedNpc r, FormKey v) => r.Owner.SetTo(v), f);
        AddForwardLink(b, s.PersistentLocation, "ACHR.PersistentLocation", (IPlacedNpcGetter r) => r.PersistentLocation.FormKey, (IPlacedNpc r, FormKey v) => r.PersistentLocation.SetTo(v), f);
        AddForward(b, s.Radius, "ACHR.Radius", (IPlacedNpcGetter r) => r.Radius, (IPlacedNpc r, float? v) => r.Radius = v, IsDefault, f);
        AddForward(b, s.Scale, "ACHR.Scale", (IPlacedNpcGetter r) => r.Scale, (IPlacedNpc r, float? v) => r.Scale = v, IsDefault, f);
    }

    private static void RegisterArma(
        ICollection<IFieldBinding> b, ArmaForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IArmorAddon, IArmorAddonGetter>(b, s.EditorID, "ARMA", f);
        AddForwardLink(b, s.ArtObject, "ARMA.ArtObject", (IArmorAddonGetter r) => r.ArtObject.FormKey, (IArmorAddon r, FormKey v) => r.ArtObject.SetTo(v), f);
        AddForward(b, s.DetectionSoundValue, "ARMA.DetectionSoundValue", (IArmorAddonGetter r) => r.DetectionSoundValue, (IArmorAddon r, byte v) => r.DetectionSoundValue = v, IsDefault, f);
        AddForwardLink(b, s.FootstepSound, "ARMA.FootstepSound", (IArmorAddonGetter r) => r.FootstepSound.FormKey, (IArmorAddon r, FormKey v) => r.FootstepSound.SetTo(v), f);
        AddForwardLink(b, s.Race, "ARMA.Race", (IArmorAddonGetter r) => r.Race.FormKey, (IArmorAddon r, FormKey v) => r.Race.SetTo(v), f);
        AddForward(b, s.WeaponAdjust, "ARMA.WeaponAdjust", (IArmorAddonGetter r) => r.WeaponAdjust, (IArmorAddon r, float v) => r.WeaponAdjust = v, IsDefault, f);
    }

    private static void RegisterArmo(
        ICollection<IFieldBinding> b, ArmoForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IArmor, IArmorGetter>(b, s.EditorID, "ARMO", f);
        AddForwardLink(b, s.AlternateBlockMaterial, "ARMO.AlternateBlockMaterial", (IArmorGetter r) => r.AlternateBlockMaterial.FormKey, (IArmor r, FormKey v) => r.AlternateBlockMaterial.SetTo(v), f);
        AddForward(b, s.ArmorRating, "ARMO.ArmorRating", (IArmorGetter r) => r.ArmorRating, (IArmor r, float v) => r.ArmorRating = v, IsDefault, f);
        AddForwardLink(b, s.BashImpactDataSet, "ARMO.BashImpactDataSet", (IArmorGetter r) => r.BashImpactDataSet.FormKey, (IArmor r, FormKey v) => r.BashImpactDataSet.SetTo(v), f);
        AddForward(b, s.EnchantmentAmount, "ARMO.EnchantmentAmount", (IArmorGetter r) => r.EnchantmentAmount, (IArmor r, ushort? v) => r.EnchantmentAmount = v, IsDefault, f);
        AddForwardLink(b, s.EquipmentType, "ARMO.EquipmentType", (IArmorGetter r) => r.EquipmentType.FormKey, (IArmor r, FormKey v) => r.EquipmentType.SetTo(v), f);
        AddForwardLink(b, s.ObjectEffect, "ARMO.ObjectEffect", (IArmorGetter r) => r.ObjectEffect.FormKey, (IArmor r, FormKey v) => r.ObjectEffect.SetTo(v), f);
        AddForwardLink(b, s.PickUpSound, "ARMO.PickUpSound", (IArmorGetter r) => r.PickUpSound.FormKey, (IArmor r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, s.PutDownSound, "ARMO.PutDownSound", (IArmorGetter r) => r.PutDownSound.FormKey, (IArmor r, FormKey v) => r.PutDownSound.SetTo(v), f);
        AddForwardLink(b, s.Race, "ARMO.Race", (IArmorGetter r) => r.Race.FormKey, (IArmor r, FormKey v) => r.Race.SetTo(v), f);
        AddForward(b, s.RagdollConstraintTemplate, "ARMO.RagdollConstraintTemplate", (IArmorGetter r) => r.RagdollConstraintTemplate, (IArmor r, string? v) => r.RagdollConstraintTemplate = v, string.IsNullOrWhiteSpace, f);
        AddForwardLink(b, s.TemplateArmor, "ARMO.TemplateArmor", (IArmorGetter r) => r.TemplateArmor.FormKey, (IArmor r, FormKey v) => r.TemplateArmor.SetTo(v), f);
        AddForward(b, s.Value, "ARMO.Value", (IArmorGetter r) => r.Value, (IArmor r, uint v) => r.Value = v, IsDefault, f);
        AddForward(b, s.Weight, "ARMO.Weight", (IArmorGetter r) => r.Weight, (IArmor r, float v) => r.Weight = v, IsDefault, f);
    }

    private static void RegisterAspc(
        ICollection<IFieldBinding> b, AspcForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IAcousticSpace, IAcousticSpaceGetter>(b, s.EditorID, "ASPC", f);
        AddForwardLink(b, s.AmbientSound, "ASPC.AmbientSound", (IAcousticSpaceGetter r) => r.AmbientSound.FormKey, (IAcousticSpace r, FormKey v) => r.AmbientSound.SetTo(v), f);
        AddForwardLink(b, s.EnvironmentType, "ASPC.EnvironmentType", (IAcousticSpaceGetter r) => r.EnvironmentType.FormKey, (IAcousticSpace r, FormKey v) => r.EnvironmentType.SetTo(v), f);
        AddForwardLink(b, s.UseSoundFromRegion, "ASPC.UseSoundFromRegion", (IAcousticSpaceGetter r) => r.UseSoundFromRegion.FormKey, (IAcousticSpace r, FormKey v) => r.UseSoundFromRegion.SetTo(v), f);
    }

    private static void RegisterCell(
        ICollection<IFieldBinding> b, CellForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<ICell, ICellGetter>(b, s.EditorID, "CELL", f);
        AddForwardLink(b, s.AcousticSpace, "CELL.AcousticSpace", (ICellGetter r) => r.AcousticSpace.FormKey, (ICell r, FormKey v) => r.AcousticSpace.SetTo(v), f);
        AddForwardLink(b, s.EncounterZone, "CELL.EncounterZone", (ICellGetter r) => r.EncounterZone.FormKey, (ICell r, FormKey v) => r.EncounterZone.SetTo(v), f);
        AddForward(b, s.FactionRank, "CELL.FactionRank", (ICellGetter r) => r.FactionRank, (ICell r, int? v) => r.FactionRank = v, IsDefault, f);
        AddForwardLink(b, s.ImageSpace, "CELL.ImageSpace", (ICellGetter r) => r.ImageSpace.FormKey, (ICell r, FormKey v) => r.ImageSpace.SetTo(v), f);
        AddForwardLink(b, s.LightingTemplate, "CELL.LightingTemplate", (ICellGetter r) => r.LightingTemplate.FormKey, (ICell r, FormKey v) => r.LightingTemplate.SetTo(v), f);
        AddForwardLink(b, s.Location, "CELL.Location", (ICellGetter r) => r.Location.FormKey, (ICell r, FormKey v) => r.Location.SetTo(v), f);
        AddForwardLink(b, s.LockList, "CELL.LockList", (ICellGetter r) => r.LockList.FormKey, (ICell r, FormKey v) => r.LockList.SetTo(v), f);
        AddForward(b, s.MaxHeightData, "CELL.MaxHeightData", (ICellGetter r) => r.MaxHeightData, (ICell r, ICellMaxHeightDataGetter? v) => r.MaxHeightData = v?.DeepCopy(), v => v is null, f, CellMaxHeightDataComparer.Instance);
        AddForwardLink(b, s.Music, "CELL.Music", (ICellGetter r) => r.Music.FormKey, (ICell r, FormKey v) => r.Music.SetTo(v), f);
        AddForward(b, s.OcclusionData, "CELL.OcclusionData", (ICellGetter r) => r.OcclusionData, (ICell r, ReadOnlyMemorySlice<byte>? v) => r.OcclusionData = v is null ? null : v.Value.ToArray(), v => v is null, f, NullableByteSliceComparer.Instance);
        AddForwardLink(b, s.Owner, "CELL.Owner", (ICellGetter r) => r.Owner.FormKey, (ICell r, FormKey v) => r.Owner.SetTo(v), f);
        AddForwardLink(b, s.SkyAndWeatherFromRegion, "CELL.SkyAndWeatherFromRegion", (ICellGetter r) => r.SkyAndWeatherFromRegion.FormKey, (ICell r, FormKey v) => r.SkyAndWeatherFromRegion.SetTo(v), f);
        AddForwardLink(b, s.Water, "CELL.Water", (ICellGetter r) => r.Water.FormKey, (ICell r, FormKey v) => r.Water.SetTo(v), f);
        AddForward(b, s.WaterEnvironmentMap, "CELL.WaterEnvironmentMap", (ICellGetter r) => r.WaterEnvironmentMap, (ICell r, string? v) => r.WaterEnvironmentMap = v, string.IsNullOrWhiteSpace, f);
        AddForward(b, s.WaterHeight, "CELL.WaterHeight", (ICellGetter r) => r.WaterHeight, (ICell r, float? v) => r.WaterHeight = v, IsDefault, f);
        AddForward(b, s.WaterNoiseTexture, "CELL.WaterNoiseTexture", (ICellGetter r) => r.WaterNoiseTexture, (ICell r, string? v) => r.WaterNoiseTexture = v, string.IsNullOrWhiteSpace, f);
        AddForward(b, s.WaterVelocity, "CELL.WaterVelocity", (ICellGetter r) => r.WaterVelocity, (ICell r, ICellWaterVelocityGetter? v) => r.WaterVelocity = v?.DeepCopy(), v => v is null, f, CellWaterVelocityComparer.Instance);
    }

    private static void RegisterCont(
        ICollection<IFieldBinding> b, ContForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IContainer, IContainerGetter>(b, s.EditorID, "CONT", f);
        AddForwardLink(b, s.CloseSound, "CONT.CloseSound", (IContainerGetter r) => r.CloseSound.FormKey, (IContainer r, FormKey v) => r.CloseSound.SetTo(v), f);
        AddForwardLink(b, s.OpenSound, "CONT.OpenSound", (IContainerGetter r) => r.OpenSound.FormKey, (IContainer r, FormKey v) => r.OpenSound.SetTo(v), f);
        AddForward(b, s.Weight, "CONT.Weight", (IContainerGetter r) => r.Weight, (IContainer r, float v) => r.Weight = v, IsDefault, f);
    }

    private static void RegisterFact(
        ICollection<IFieldBinding> b, FactForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IFaction, IFactionGetter>(b, s.EditorID, "FACT", f);
        AddForwardLink(b, s.ExteriorJailMarker, "FACT.ExteriorJailMarker", (IFactionGetter r) => r.ExteriorJailMarker.FormKey, (IFaction r, FormKey v) => r.ExteriorJailMarker.SetTo(v), f);
        AddForwardLink(b, s.FollowerWaitMarker, "FACT.FollowerWaitMarker", (IFactionGetter r) => r.FollowerWaitMarker.FormKey, (IFaction r, FormKey v) => r.FollowerWaitMarker.SetTo(v), f);
        AddForwardLink(b, s.JailOutfit, "FACT.JailOutfit", (IFactionGetter r) => r.JailOutfit.FormKey, (IFaction r, FormKey v) => r.JailOutfit.SetTo(v), f);
        AddForwardLink(b, s.MerchantContainer, "FACT.MerchantContainer", (IFactionGetter r) => r.MerchantContainer.FormKey, (IFaction r, FormKey v) => r.MerchantContainer.SetTo(v), f);
        AddForwardLink(b, s.PlayerInventoryContainer, "FACT.PlayerInventoryContainer", (IFactionGetter r) => r.PlayerInventoryContainer.FormKey, (IFaction r, FormKey v) => r.PlayerInventoryContainer.SetTo(v), f);
        AddForwardLink(b, s.SharedCrimeFactionList, "FACT.SharedCrimeFactionList", (IFactionGetter r) => r.SharedCrimeFactionList.FormKey, (IFaction r, FormKey v) => r.SharedCrimeFactionList.SetTo(v), f);
        AddForwardLink(b, s.StolenGoodsContainer, "FACT.StolenGoodsContainer", (IFactionGetter r) => r.StolenGoodsContainer.FormKey, (IFaction r, FormKey v) => r.StolenGoodsContainer.SetTo(v), f);
        AddForwardLink(b, s.VendorBuySellList, "FACT.VendorBuySellList", (IFactionGetter r) => r.VendorBuySellList.FormKey, (IFaction r, FormKey v) => r.VendorBuySellList.SetTo(v), f);
    }

    private static void RegisterFlst(
        ICollection<IFieldBinding> b, FlstForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f) =>
        AddEditorId<IFormList, IFormListGetter>(b, s.EditorID, "FLST", f);

    private static void RegisterGlob(
        ICollection<IFieldBinding> b, GlobForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IGlobalFloat, IGlobalFloatGetter>(b, s.EditorID, "GLOB", f);
        AddEditorId<IGlobalInt, IGlobalIntGetter>(b, s.EditorID, "GLOB", f);
        AddEditorId<IGlobalShort, IGlobalShortGetter>(b, s.EditorID, "GLOB", f);
        AddEditorId<IGlobalUnknown, IGlobalUnknownGetter>(b, s.EditorID, "GLOB", f);
        AddForward(b, s.Data, "GLOB.Data", (IGlobalFloatGetter r) => r.Data, (IGlobalFloat r, float? v) => r.Data = v, IsDefault, f);
        AddForward(b, s.Data, "GLOB.Data", (IGlobalIntGetter r) => r.Data, (IGlobalInt r, int? v) => r.Data = v, IsDefault, f);
        AddForward(b, s.Data, "GLOB.Data", (IGlobalShortGetter r) => r.Data, (IGlobalShort r, short? v) => r.Data = v, IsDefault, f);
        AddForward(b, s.Data, "GLOB.Data", (IGlobalUnknownGetter r) => r.Data, (IGlobalUnknown r, float? v) => r.Data = v, IsDefault, f);
    }

    private static void RegisterIngr(
        ICollection<IFieldBinding> b, IngrForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IIngredient, IIngredientGetter>(b, s.EditorID, "INGR", f);
        AddForwardLink(b, s.EquipType, "INGR.EquipType", (IIngredientGetter r) => r.EquipType.FormKey, (IIngredient r, FormKey v) => r.EquipType.SetTo(v), f);
        AddForward(b, s.IngredientValue, "INGR.IngredientValue", (IIngredientGetter r) => r.IngredientValue, (IIngredient r, int v) => r.IngredientValue = v, IsDefault, f);
        AddForwardLink(b, s.PickUpSound, "INGR.PickUpSound", (IIngredientGetter r) => r.PickUpSound.FormKey, (IIngredient r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, s.PutDownSound, "INGR.PutDownSound", (IIngredientGetter r) => r.PutDownSound.FormKey, (IIngredient r, FormKey v) => r.PutDownSound.SetTo(v), f);
        AddForward(b, s.Value, "INGR.Value", (IIngredientGetter r) => r.Value, (IIngredient r, uint v) => r.Value = v, IsDefault, f);
        AddForward(b, s.Weight, "INGR.Weight", (IIngredientGetter r) => r.Weight, (IIngredient r, float v) => r.Weight = v, IsDefault, f);
    }

    private static void RegisterIpct(
        ICollection<IFieldBinding> b, IpctForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IImpact, IImpactGetter>(b, s.EditorID, "IPCT", f);
        AddForward(b, s.AngleThreshold, "IPCT.AngleThreshold", (IImpactGetter r) => r.AngleThreshold, (IImpact r, float v) => r.AngleThreshold = v, IsDefault, f);
        AddForward(b, s.Duration, "IPCT.Duration", (IImpactGetter r) => r.Duration, (IImpact r, float v) => r.Duration = v, IsDefault, f);
        AddForwardLink(b, s.Hazard, "IPCT.Hazard", (IImpactGetter r) => r.Hazard.FormKey, (IImpact r, FormKey v) => r.Hazard.SetTo(v), f);
        AddForward(b, s.NoDecalData, "IPCT.NoDecalData", (IImpactGetter r) => r.NoDecalData, (IImpact r, bool v) => r.NoDecalData = v, IsDefault, f);
        AddForward(b, s.Orientation, "IPCT.Orientation", (IImpactGetter r) => r.Orientation, (IImpact r, Impact.OrientationType v) => r.Orientation = v, IsDefault, f);
        AddForward(b, s.PlacementRadius, "IPCT.PlacementRadius", (IImpactGetter r) => r.PlacementRadius, (IImpact r, float v) => r.PlacementRadius = v, IsDefault, f);
        AddForward(b, s.Result, "IPCT.Result", (IImpactGetter r) => r.Result, (IImpact r, Impact.ResultType v) => r.Result = v, IsDefault, f);
        AddForwardLink(b, s.SecondaryTextureSet, "IPCT.SecondaryTextureSet", (IImpactGetter r) => r.SecondaryTextureSet.FormKey, (IImpact r, FormKey v) => r.SecondaryTextureSet.SetTo(v), f);
        AddForwardLink(b, s.Sound1, "IPCT.Sound1", (IImpactGetter r) => r.Sound1.FormKey, (IImpact r, FormKey v) => r.Sound1.SetTo(v), f);
        AddForwardLink(b, s.Sound2, "IPCT.Sound2", (IImpactGetter r) => r.Sound2.FormKey, (IImpact r, FormKey v) => r.Sound2.SetTo(v), f);
        AddForward(b, s.SoundLevel, "IPCT.SoundLevel", (IImpactGetter r) => r.SoundLevel, (IImpact r, SoundLevel v) => r.SoundLevel = v, IsDefault, f);
        AddForwardLink(b, s.TextureSet, "IPCT.TextureSet", (IImpactGetter r) => r.TextureSet.FormKey, (IImpact r, FormKey v) => r.TextureSet.SetTo(v), f);
    }

    private static void RegisterKeym(
        ICollection<IFieldBinding> b, KeymForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IKey, IKeyGetter>(b, s.EditorID, "KEYM", f);
        AddForwardLink(b, s.PickUpSound, "KEYM.PickUpSound", (IKeyGetter r) => r.PickUpSound.FormKey, (IKey r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, s.PutDownSound, "KEYM.PutDownSound", (IKeyGetter r) => r.PutDownSound.FormKey, (IKey r, FormKey v) => r.PutDownSound.SetTo(v), f);
        AddForward(b, s.Value, "KEYM.Value", (IKeyGetter r) => r.Value, (IKey r, uint v) => r.Value = v, IsDefault, f);
        AddForward(b, s.Weight, "KEYM.Weight", (IKeyGetter r) => r.Weight, (IKey r, float v) => r.Weight = v, IsDefault, f);
    }

    private static void RegisterLctn(
        ICollection<IFieldBinding> b, LctnForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<ILocation, ILocationGetter>(b, s.EditorID, "LCTN", f);
        AddForwardLink(b, s.HorseMarkerRef, "LCTN.HorseMarkerRef", (ILocationGetter r) => r.HorseMarkerRef.FormKey, (ILocation r, FormKey v) => r.HorseMarkerRef.SetTo(v), f);
        AddForwardLink(b, s.Music, "LCTN.Music", (ILocationGetter r) => r.Music.FormKey, (ILocation r, FormKey v) => r.Music.SetTo(v), f);
        AddForwardLink(b, s.ParentLocation, "LCTN.ParentLocation", (ILocationGetter r) => r.ParentLocation.FormKey, (ILocation r, FormKey v) => r.ParentLocation.SetTo(v), f);
        AddForwardLink(b, s.UnreportedCrimeFaction, "LCTN.UnreportedCrimeFaction", (ILocationGetter r) => r.UnreportedCrimeFaction.FormKey, (ILocation r, FormKey v) => r.UnreportedCrimeFaction.SetTo(v), f);
        AddForwardLink(b, s.WorldLocationMarkerRef, "LCTN.WorldLocationMarkerRef", (ILocationGetter r) => r.WorldLocationMarkerRef.FormKey, (ILocation r, FormKey v) => r.WorldLocationMarkerRef.SetTo(v), f);
        AddForward(b, s.WorldLocationRadius, "LCTN.WorldLocationRadius", (ILocationGetter r) => r.WorldLocationRadius, (ILocation r, float? v) => r.WorldLocationRadius = v, IsDefault, f);
    }

    private static void RegisterLvli(
        ICollection<IFieldBinding> b, LvliForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<ILeveledItem, ILeveledItemGetter>(b, s.EditorID, "LVLI", f);
        AddForwardLink(b, s.Global, "LVLI.Global", (ILeveledItemGetter r) => r.Global.FormKey, (ILeveledItem r, FormKey v) => r.Global.SetTo(v), f);
    }

    private static void RegisterLvln(
        ICollection<IFieldBinding> b, LvlnForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<ILeveledNpc, ILeveledNpcGetter>(b, s.EditorID, "LVLN", f);
        AddForwardLink(b, s.Global, "LVLN.Global", (ILeveledNpcGetter r) => r.Global.FormKey, (ILeveledNpc r, FormKey v) => r.Global.SetTo(v), f);
    }

    private static void RegisterMgef(
        ICollection<IFieldBinding> b, MgefForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IMagicEffect, IMagicEffectGetter>(b, s.EditorID, "MGEF", f);
        AddForward(b, s.BaseCost, "MGEF.BaseCost", (IMagicEffectGetter r) => r.BaseCost, (IMagicEffect r, float v) => r.BaseCost = v, IsDefault, f);
        AddForwardLink(b, s.CastingArt, "MGEF.CastingArt", (IMagicEffectGetter r) => r.CastingArt.FormKey, (IMagicEffect r, FormKey v) => r.CastingArt.SetTo(v), f);
        AddForwardLink(b, s.CastingLight, "MGEF.CastingLight", (IMagicEffectGetter r) => r.CastingLight.FormKey, (IMagicEffect r, FormKey v) => r.CastingLight.SetTo(v), f);
        AddForward(b, s.CastingSoundLevel, "MGEF.CastingSoundLevel", (IMagicEffectGetter r) => r.CastingSoundLevel, (IMagicEffect r, SoundLevel v) => r.CastingSoundLevel = v, IsDefault, f);
        AddForward(b, s.CastType, "MGEF.CastType", (IMagicEffectGetter r) => r.CastType, (IMagicEffect r, CastType v) => r.CastType = v, IsDefault, f);
        AddForwardLink(b, s.DualCastArt, "MGEF.DualCastArt", (IMagicEffectGetter r) => r.DualCastArt.FormKey, (IMagicEffect r, FormKey v) => r.DualCastArt.SetTo(v), f);
        AddForward(b, s.DualCastScale, "MGEF.DualCastScale", (IMagicEffectGetter r) => r.DualCastScale, (IMagicEffect r, float v) => r.DualCastScale = v, IsDefault, f);
        AddForwardLink(b, s.EnchantArt, "MGEF.EnchantArt", (IMagicEffectGetter r) => r.EnchantArt.FormKey, (IMagicEffect r, FormKey v) => r.EnchantArt.SetTo(v), f);
        AddForwardLink(b, s.EnchantShader, "MGEF.EnchantShader", (IMagicEffectGetter r) => r.EnchantShader.FormKey, (IMagicEffect r, FormKey v) => r.EnchantShader.SetTo(v), f);
        AddForwardLink(b, s.EnchantVisuals, "MGEF.EnchantVisuals", (IMagicEffectGetter r) => r.EnchantVisuals.FormKey, (IMagicEffect r, FormKey v) => r.EnchantVisuals.SetTo(v), f);
        AddForwardLink(b, s.EquipAbility, "MGEF.EquipAbility", (IMagicEffectGetter r) => r.EquipAbility.FormKey, (IMagicEffect r, FormKey v) => r.EquipAbility.SetTo(v), f);
        AddForwardLink(b, s.Explosion, "MGEF.Explosion", (IMagicEffectGetter r) => r.Explosion.FormKey, (IMagicEffect r, FormKey v) => r.Explosion.SetTo(v), f);
        AddForwardLink(b, s.HitEffectArt, "MGEF.HitEffectArt", (IMagicEffectGetter r) => r.HitEffectArt.FormKey, (IMagicEffect r, FormKey v) => r.HitEffectArt.SetTo(v), f);
        AddForwardLink(b, s.HitShader, "MGEF.HitShader", (IMagicEffectGetter r) => r.HitShader.FormKey, (IMagicEffect r, FormKey v) => r.HitShader.SetTo(v), f);
        AddForwardLink(b, s.HitVisuals, "MGEF.HitVisuals", (IMagicEffectGetter r) => r.HitVisuals.FormKey, (IMagicEffect r, FormKey v) => r.HitVisuals.SetTo(v), f);
        AddForwardLink(b, s.ImageSpaceModifier, "MGEF.ImageSpaceModifier", (IMagicEffectGetter r) => r.ImageSpaceModifier.FormKey, (IMagicEffect r, FormKey v) => r.ImageSpaceModifier.SetTo(v), f);
        AddForwardLink(b, s.ImpactData, "MGEF.ImpactData", (IMagicEffectGetter r) => r.ImpactData.FormKey, (IMagicEffect r, FormKey v) => r.ImpactData.SetTo(v), f);
        AddForward(b, s.MagicSkill, "MGEF.MagicSkill", (IMagicEffectGetter r) => r.MagicSkill, (IMagicEffect r, ActorValue v) => r.MagicSkill = v, IsDefault, f);
        AddForwardLink(b, s.MenuDisplayObject, "MGEF.MenuDisplayObject", (IMagicEffectGetter r) => r.MenuDisplayObject.FormKey, (IMagicEffect r, FormKey v) => r.MenuDisplayObject.SetTo(v), f);
        AddForward(b, s.MinimumSkillLevel, "MGEF.MinimumSkillLevel", (IMagicEffectGetter r) => r.MinimumSkillLevel, (IMagicEffect r, uint v) => r.MinimumSkillLevel = v, IsDefault, f);
        AddForwardLink(b, s.PerkToApply, "MGEF.PerkToApply", (IMagicEffectGetter r) => r.PerkToApply.FormKey, (IMagicEffect r, FormKey v) => r.PerkToApply.SetTo(v), f);
        AddForwardLink(b, s.Projectile, "MGEF.Projectile", (IMagicEffectGetter r) => r.Projectile.FormKey, (IMagicEffect r, FormKey v) => r.Projectile.SetTo(v), f);
        AddForward(b, s.ResistValue, "MGEF.ResistValue", (IMagicEffectGetter r) => r.ResistValue, (IMagicEffect r, ActorValue v) => r.ResistValue = v, IsDefault, f);
        AddForward(b, s.ScriptEffectAIDelayTime, "MGEF.ScriptEffectAIDelayTime", (IMagicEffectGetter r) => r.ScriptEffectAIDelayTime, (IMagicEffect r, float v) => r.ScriptEffectAIDelayTime = v, IsDefault, f);
        AddForward(b, s.ScriptEffectAIScore, "MGEF.ScriptEffectAIScore", (IMagicEffectGetter r) => r.ScriptEffectAIScore, (IMagicEffect r, float v) => r.ScriptEffectAIScore = v, IsDefault, f);
        AddForward(b, s.SecondActorValue, "MGEF.SecondActorValue", (IMagicEffectGetter r) => r.SecondActorValue, (IMagicEffect r, ActorValue v) => r.SecondActorValue = v, IsDefault, f);
        AddForward(b, s.SecondActorValueWeight, "MGEF.SecondActorValueWeight", (IMagicEffectGetter r) => r.SecondActorValueWeight, (IMagicEffect r, float v) => r.SecondActorValueWeight = v, IsDefault, f);
        AddForward(b, s.SkillUsageMultiplier, "MGEF.SkillUsageMultiplier", (IMagicEffectGetter r) => r.SkillUsageMultiplier, (IMagicEffect r, float v) => r.SkillUsageMultiplier = v, IsDefault, f);
        AddForward(b, s.SpellmakingArea, "MGEF.SpellmakingArea", (IMagicEffectGetter r) => r.SpellmakingArea, (IMagicEffect r, uint v) => r.SpellmakingArea = v, IsDefault, f);
        AddForward(b, s.SpellmakingCastingTime, "MGEF.SpellmakingCastingTime", (IMagicEffectGetter r) => r.SpellmakingCastingTime, (IMagicEffect r, float v) => r.SpellmakingCastingTime = v, IsDefault, f);
        AddForward(b, s.TaperCurve, "MGEF.TaperCurve", (IMagicEffectGetter r) => r.TaperCurve, (IMagicEffect r, float v) => r.TaperCurve = v, IsDefault, f);
        AddForward(b, s.TaperDuration, "MGEF.TaperDuration", (IMagicEffectGetter r) => r.TaperDuration, (IMagicEffect r, float v) => r.TaperDuration = v, IsDefault, f);
        AddForward(b, s.TaperWeight, "MGEF.TaperWeight", (IMagicEffectGetter r) => r.TaperWeight, (IMagicEffect r, float v) => r.TaperWeight = v, IsDefault, f);
        AddForward(b, s.TargetType, "MGEF.TargetType", (IMagicEffectGetter r) => r.TargetType, (IMagicEffect r, TargetType v) => r.TargetType = v, IsDefault, f);
    }

    private static void RegisterNpc(
        ICollection<IFieldBinding> b, NpcForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<INpc, INpcGetter>(b, s.EditorID, "NPC_", f);
        AddForwardLink(b, s.AttackRace, "NPC_.AttackRace", (INpcGetter r) => r.AttackRace.FormKey, (INpc r, FormKey v) => r.AttackRace.SetTo(v), f);
        AddForwardLink(b, s.Class, "NPC_.Class", (INpcGetter r) => r.Class.FormKey, (INpc r, FormKey v) => r.Class.SetTo(v), f);
        AddForwardLink(b, s.CombatOverridePackageList, "NPC_.CombatOverridePackageList", (INpcGetter r) => r.CombatOverridePackageList.FormKey, (INpc r, FormKey v) => r.CombatOverridePackageList.SetTo(v), f);
        AddForwardLink(b, s.CombatStyle, "NPC_.CombatStyle", (INpcGetter r) => r.CombatStyle.FormKey, (INpc r, FormKey v) => r.CombatStyle.SetTo(v), f);
        AddForwardLink(b, s.CrimeFaction, "NPC_.CrimeFaction", (INpcGetter r) => r.CrimeFaction.FormKey, (INpc r, FormKey v) => r.CrimeFaction.SetTo(v), f);
        AddForwardLink(b, s.DeathItem, "NPC_.DeathItem", (INpcGetter r) => r.DeathItem.FormKey, (INpc r, FormKey v) => r.DeathItem.SetTo(v), f);
        AddForwardLink(b, s.DefaultOutfit, "NPC_.DefaultOutfit", (INpcGetter r) => r.DefaultOutfit.FormKey, (INpc r, FormKey v) => r.DefaultOutfit.SetTo(v), f);
        AddForwardLink(b, s.DefaultPackageList, "NPC_.DefaultPackageList", (INpcGetter r) => r.DefaultPackageList.FormKey, (INpc r, FormKey v) => r.DefaultPackageList.SetTo(v), f);
        AddForwardLink(b, s.FarAwayModel, "NPC_.FarAwayModel", (INpcGetter r) => r.FarAwayModel.FormKey, (INpc r, FormKey v) => r.FarAwayModel.SetTo(v), f);
        AddForwardLink(b, s.GiftFilter, "NPC_.GiftFilter", (INpcGetter r) => r.GiftFilter.FormKey, (INpc r, FormKey v) => r.GiftFilter.SetTo(v), f);
        AddForwardLink(b, s.GuardWarnOverridePackageList, "NPC_.GuardWarnOverridePackageList", (INpcGetter r) => r.GuardWarnOverridePackageList.FormKey, (INpc r, FormKey v) => r.GuardWarnOverridePackageList.SetTo(v), f);
        AddForwardLink(b, s.HairColor, "NPC_.HairColor", (INpcGetter r) => r.HairColor.FormKey, (INpc r, FormKey v) => r.HairColor.SetTo(v), f);
        AddForwardLink(b, s.HeadTexture, "NPC_.HeadTexture", (INpcGetter r) => r.HeadTexture.FormKey, (INpc r, FormKey v) => r.HeadTexture.SetTo(v), f);
        AddForward(b, s.Height, "NPC_.Height", (INpcGetter r) => r.Height, (INpc r, float v) => r.Height = v, IsDefault, f);
        AddForwardLink(b, s.ObserveDeadBodyOverridePackageList, "NPC_.ObserveDeadBodyOverridePackageList", (INpcGetter r) => r.ObserveDeadBodyOverridePackageList.FormKey, (INpc r, FormKey v) => r.ObserveDeadBodyOverridePackageList.SetTo(v), f);
        AddForwardLink(b, s.Race, "NPC_.Race", (INpcGetter r) => r.Race.FormKey, (INpc r, FormKey v) => r.Race.SetTo(v), f);
        AddForwardLink(b, s.SleepingOutfit, "NPC_.SleepingOutfit", (INpcGetter r) => r.SleepingOutfit.FormKey, (INpc r, FormKey v) => r.SleepingOutfit.SetTo(v), f);
        AddForward(b, s.SoundLevel, "NPC_.SoundLevel", (INpcGetter r) => r.SoundLevel, (INpc r, SoundLevel v) => r.SoundLevel = v, IsDefault, f);
        AddForwardLink(b, s.SpectatorOverridePackageList, "NPC_.SpectatorOverridePackageList", (INpcGetter r) => r.SpectatorOverridePackageList.FormKey, (INpc r, FormKey v) => r.SpectatorOverridePackageList.SetTo(v), f);
        AddForwardLink(b, s.Template, "NPC_.Template", (INpcGetter r) => r.Template.FormKey, (INpc r, FormKey v) => r.Template.SetTo(v), f);
        AddForwardLink(b, s.Voice, "NPC_.Voice", (INpcGetter r) => r.Voice.FormKey, (INpc r, FormKey v) => r.Voice.SetTo(v), f);
        AddForward(b, s.Weight, "NPC_.Weight", (INpcGetter r) => r.Weight, (INpc r, float v) => r.Weight = v, IsDefault, f);
        AddForwardLink(b, s.WornArmor, "NPC_.WornArmor", (INpcGetter r) => r.WornArmor.FormKey, (INpc r, FormKey v) => r.WornArmor.SetTo(v), f);
    }

    private static void RegisterOtft(
        ICollection<IFieldBinding> b, OtftForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f) =>
        AddEditorId<IOutfit, IOutfitGetter>(b, s.EditorID, "OTFT", f);

    private static void RegisterRefr(
        ICollection<IFieldBinding> b, RefrForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IPlacedObject, IPlacedObjectGetter>(b, s.EditorID, "REFR", f);
        AddForward(b, s.Action, "REFR.Action", (IPlacedObjectGetter r) => r.Action, (IPlacedObject r, PlacedObject.ActionFlag? v) => r.Action = v, IsDefault, f);
        AddForwardLink(b, s.AttachRef, "REFR.AttachRef", (IPlacedObjectGetter r) => r.AttachRef.FormKey, (IPlacedObject r, FormKey v) => r.AttachRef.SetTo(v), f);
        AddForwardLink(b, s.Base, "REFR.Base", (IPlacedObjectGetter r) => r.Base.FormKey, (IPlacedObject r, FormKey v) => r.Base.SetTo(v), f);
        AddForward(b, s.Charge, "REFR.Charge", (IPlacedObjectGetter r) => r.Charge, (IPlacedObject r, float? v) => r.Charge = v, IsDefault, f);
        AddForward(b, s.CollisionLayer, "REFR.CollisionLayer", (IPlacedObjectGetter r) => r.CollisionLayer, (IPlacedObject r, uint? v) => r.CollisionLayer = v, IsDefault, f);
        AddForwardLink(b, s.Emittance, "REFR.Emittance", (IPlacedObjectGetter r) => r.Emittance.FormKey, (IPlacedObject r, FormKey v) => r.Emittance.SetTo(v), f);
        AddForwardLink(b, s.EncounterZone, "REFR.EncounterZone", (IPlacedObjectGetter r) => r.EncounterZone.FormKey, (IPlacedObject r, FormKey v) => r.EncounterZone.SetTo(v), f);
        AddForward(b, s.FactionRank, "REFR.FactionRank", (IPlacedObjectGetter r) => r.FactionRank, (IPlacedObject r, int? v) => r.FactionRank = v, IsDefault, f);
        AddForward(b, s.FavorCost, "REFR.FavorCost", (IPlacedObjectGetter r) => r.FavorCost, (IPlacedObject r, float? v) => r.FavorCost = v, IsDefault, f);
        AddForward(b, s.HeadTrackingWeight, "REFR.HeadTrackingWeight", (IPlacedObjectGetter r) => r.HeadTrackingWeight, (IPlacedObject r, float? v) => r.HeadTrackingWeight = v, IsDefault, f);
        AddForwardLink(b, s.ImageSpace, "REFR.ImageSpace", (IPlacedObjectGetter r) => r.ImageSpace.FormKey, (IPlacedObject r, FormKey v) => r.ImageSpace.SetTo(v), f);
        AddForward(b, s.IsIgnoredBySandbox, "REFR.IsIgnoredBySandbox", (IPlacedObjectGetter r) => r.IsIgnoredBySandbox, (IPlacedObject r, bool v) => r.IsIgnoredBySandbox = v, IsDefault, f);
        AddForward(b, s.IsMultiBoundPrimitive, "REFR.IsMultiBoundPrimitive", (IPlacedObjectGetter r) => r.IsMultiBoundPrimitive, (IPlacedObject r, bool v) => r.IsMultiBoundPrimitive = v, IsDefault, f);
        AddForward(b, s.IsOpenByDefault, "REFR.IsOpenByDefault", (IPlacedObjectGetter r) => r.IsOpenByDefault, (IPlacedObject r, bool v) => r.IsOpenByDefault = v, IsDefault, f);
        AddForward(b, s.ItemCount, "REFR.ItemCount", (IPlacedObjectGetter r) => r.ItemCount, (IPlacedObject r, int? v) => r.ItemCount = v, IsDefault, f);
        AddForwardLink(b, s.LeveledItemBaseObject, "REFR.LeveledItemBaseObject", (IPlacedObjectGetter r) => r.LeveledItemBaseObject.FormKey, (IPlacedObject r, FormKey v) => r.LeveledItemBaseObject.SetTo(v), f);
        AddForward(b, s.LevelModifier, "REFR.LevelModifier", (IPlacedObjectGetter r) => r.LevelModifier, (IPlacedObject r, Level? v) => r.LevelModifier = v, IsDefault, f);
        AddForwardLink(b, s.LightingTemplate, "REFR.LightingTemplate", (IPlacedObjectGetter r) => r.LightingTemplate.FormKey, (IPlacedObject r, FormKey v) => r.LightingTemplate.SetTo(v), f);
        AddForwardLink(b, s.LocationReference, "REFR.LocationReference", (IPlacedObjectGetter r) => r.LocationReference.FormKey, (IPlacedObject r, FormKey v) => r.LocationReference.SetTo(v), f);
        AddForwardLink(b, s.MultiBoundReference, "REFR.MultiBoundReference", (IPlacedObjectGetter r) => r.MultiBoundReference.FormKey, (IPlacedObject r, FormKey v) => r.MultiBoundReference.SetTo(v), f);
        AddForwardLink(b, s.Owner, "REFR.Owner", (IPlacedObjectGetter r) => r.Owner.FormKey, (IPlacedObject r, FormKey v) => r.Owner.SetTo(v), f);
        AddForwardLink(b, s.PersistentLocation, "REFR.PersistentLocation", (IPlacedObjectGetter r) => r.PersistentLocation.FormKey, (IPlacedObject r, FormKey v) => r.PersistentLocation.SetTo(v), f);
        AddForward(b, s.Radius, "REFR.Radius", (IPlacedObjectGetter r) => r.Radius, (IPlacedObject r, float? v) => r.Radius = v, IsDefault, f);
        AddForward(b, s.Scale, "REFR.Scale", (IPlacedObjectGetter r) => r.Scale, (IPlacedObject r, float? v) => r.Scale = v, IsDefault, f);
        AddForwardLink(b, s.SpawnContainer, "REFR.SpawnContainer", (IPlacedObjectGetter r) => r.SpawnContainer.FormKey, (IPlacedObject r, FormKey v) => r.SpawnContainer.SetTo(v), f);
        AddForwardLink(b, s.TeleportMessageBox, "REFR.TeleportMessageBox", (IPlacedObjectGetter r) => r.TeleportMessageBox.FormKey, (IPlacedObject r, FormKey v) => r.TeleportMessageBox.SetTo(v), f);
        AddForwardLink(b, s.XCZC, "REFR.XCZC", (IPlacedObjectGetter r) => r.XCZC.FormKey, (IPlacedObject r, FormKey v) => r.XCZC.SetTo(v), f);
        AddForwardLink(b, s.XCZR, "REFR.XCZR", (IPlacedObjectGetter r) => r.XCZR.FormKey, (IPlacedObject r, FormKey v) => r.XCZR.SetTo(v), f);
    }

    private static void RegisterRegn(
        ICollection<IFieldBinding> b, RegnForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddForward(b, s.Grasses, "REGN.Grasses", (IRegionGetter r) => r.Grasses, (IRegion r, IRegionGrassesGetter? v) => r.Grasses = v?.DeepCopy(), v => v is null, f, RegionDataComparers.Grasses);
        AddForward(b, s.Land, "REGN.Land", (IRegionGetter r) => r.Land, (IRegion r, IRegionLandGetter? v) => r.Land = v?.DeepCopy(), v => v is null, f, RegionDataComparers.Land);
        AddForward(b, s.Map, "REGN.Map", (IRegionGetter r) => r.Map, (IRegion r, IRegionMapGetter? v) => r.Map = v?.DeepCopy(), v => v is null, f, RegionDataComparers.Map);
        AddForward(b, s.Objects, "REGN.Objects", (IRegionGetter r) => r.Objects, (IRegion r, IRegionObjectsGetter? v) => r.Objects = v?.DeepCopy(), v => v is null, f, RegionDataComparers.Objects);
        AddForward(b, s.Sounds, "REGN.Sounds", (IRegionGetter r) => r.Sounds, (IRegion r, IRegionSoundsGetter? v) => r.Sounds = v?.DeepCopy(), v => v is null, f, RegionDataComparers.Sounds);
        AddForward(b, s.Weather, "REGN.Weather", (IRegionGetter r) => r.Weather, (IRegion r, IRegionWeatherGetter? v) => r.Weather = v?.DeepCopy(), v => v is null, f, RegionDataComparers.Weather);
    }

    private static void RegisterSndr(
        ICollection<IFieldBinding> b, SndrForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<ISoundDescriptor, ISoundDescriptorGetter>(b, s.EditorID, "SNDR", f);
        AddForwardLink(b, s.AlternateSoundFor, "SNDR.AlternateSoundFor", (ISoundDescriptorGetter r) => r.AlternateSoundFor.FormKey, (ISoundDescriptor r, FormKey v) => r.AlternateSoundFor.SetTo(v), f);
        AddForwardLink(b, s.Category, "SNDR.Category", (ISoundDescriptorGetter r) => r.Category.FormKey, (ISoundDescriptor r, FormKey v) => r.Category.SetTo(v), f);
        AddForwardLink(b, s.OutputModel, "SNDR.OutputModel", (ISoundDescriptorGetter r) => r.OutputModel.FormKey, (ISoundDescriptor r, FormKey v) => r.OutputModel.SetTo(v), f);
        AddForward(b, s.PercentFrequencyShift, "SNDR.PercentFrequencyShift", (ISoundDescriptorGetter r) => r.PercentFrequencyShift, (ISoundDescriptor r, sbyte v) => r.PercentFrequencyShift = v, IsDefault, f);
        AddForward(b, s.PercentFrequencyVariance, "SNDR.PercentFrequencyVariance", (ISoundDescriptorGetter r) => r.PercentFrequencyVariance, (ISoundDescriptor r, sbyte v) => r.PercentFrequencyVariance = v, IsDefault, f);
        AddForward(b, s.Priority, "SNDR.Priority", (ISoundDescriptorGetter r) => r.Priority, (ISoundDescriptor r, byte v) => r.Priority = v, IsDefault, f);
        AddForward(b, s.StaticAttenuation, "SNDR.StaticAttenuation", (ISoundDescriptorGetter r) => r.StaticAttenuation, (ISoundDescriptor r, float v) => r.StaticAttenuation = v, IsDefault, f);
        AddForward(b, s.Type, "SNDR.Type", (ISoundDescriptorGetter r) => r.Type, (ISoundDescriptor r, SoundDescriptor.DescriptorType? v) => r.Type = v, IsDefault, f);
        AddForward(b, s.Variance, "SNDR.Variance", (ISoundDescriptorGetter r) => r.Variance, (ISoundDescriptor r, byte v) => r.Variance = v, IsDefault, f);
    }

    private static void RegisterSpel(
        ICollection<IFieldBinding> b, SpelForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<ISpell, ISpellGetter>(b, s.EditorID, "SPEL", f);
        AddForward(b, s.BaseCost, "SPEL.BaseCost", (ISpellGetter r) => r.BaseCost, (ISpell r, uint v) => r.BaseCost = v, IsDefault, f);
        AddForward(b, s.CastDuration, "SPEL.CastDuration", (ISpellGetter r) => r.CastDuration, (ISpell r, float v) => r.CastDuration = v, IsDefault, f);
        AddForward(b, s.CastType, "SPEL.CastType", (ISpellGetter r) => r.CastType, (ISpell r, CastType v) => r.CastType = v, IsDefault, f);
        AddForward(b, s.ChargeTime, "SPEL.ChargeTime", (ISpellGetter r) => r.ChargeTime, (ISpell r, float v) => r.ChargeTime = v, IsDefault, f);
        AddForwardLink(b, s.EquipmentType, "SPEL.EquipmentType", (ISpellGetter r) => r.EquipmentType.FormKey, (ISpell r, FormKey v) => r.EquipmentType.SetTo(v), f);
        AddForwardLink(b, s.HalfCostPerk, "SPEL.HalfCostPerk", (ISpellGetter r) => r.HalfCostPerk.FormKey, (ISpell r, FormKey v) => r.HalfCostPerk.SetTo(v), f);
        AddForwardLink(b, s.MenuDisplayObject, "SPEL.MenuDisplayObject", (ISpellGetter r) => r.MenuDisplayObject.FormKey, (ISpell r, FormKey v) => r.MenuDisplayObject.SetTo(v), f);
        AddForward(b, s.Range, "SPEL.Range", (ISpellGetter r) => r.Range, (ISpell r, float v) => r.Range = v, IsDefault, f);
        AddForward(b, s.TargetType, "SPEL.TargetType", (ISpellGetter r) => r.TargetType, (ISpell r, TargetType v) => r.TargetType = v, IsDefault, f);
        AddForward(b, s.Type, "SPEL.Type", (ISpellGetter r) => r.Type, (ISpell r, SpellType v) => r.Type = v, IsDefault, f);
    }

    private static void RegisterWrld(
        ICollection<IFieldBinding> b, WrldForwardingSettings s,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddEditorId<IWorldspace, IWorldspaceGetter>(b, s.EditorID, "WRLD", f);
        AddForwardLink(b, s.Climate, "WRLD.Climate", (IWorldspaceGetter r) => r.Climate.FormKey, (IWorldspace r, FormKey v) => r.Climate.SetTo(v), f);
        AddForward(b, s.DistantLodMultiplier, "WRLD.DistantLodMultiplier", (IWorldspaceGetter r) => r.DistantLodMultiplier, (IWorldspace r, float? v) => r.DistantLodMultiplier = v, IsDefault, f);
        AddForwardLink(b, s.EncounterZone, "WRLD.EncounterZone", (IWorldspaceGetter r) => r.EncounterZone.FormKey, (IWorldspace r, FormKey v) => r.EncounterZone.SetTo(v), f);
        AddForwardLink(b, s.InteriorLighting, "WRLD.InteriorLighting", (IWorldspaceGetter r) => r.InteriorLighting.FormKey, (IWorldspace r, FormKey v) => r.InteriorLighting.SetTo(v), f);
        AddForwardLink(b, s.Location, "WRLD.Location", (IWorldspaceGetter r) => r.Location.FormKey, (IWorldspace r, FormKey v) => r.Location.SetTo(v), f);
        AddForwardLink(b, s.LodWater, "WRLD.LodWater", (IWorldspaceGetter r) => r.LodWater.FormKey, (IWorldspace r, FormKey v) => r.LodWater.SetTo(v), f);
        AddForward(b, s.LodWaterHeight, "WRLD.LodWaterHeight", (IWorldspaceGetter r) => r.LodWaterHeight, (IWorldspace r, float? v) => r.LodWaterHeight = v, IsDefault, f);
        AddForwardLink(b, s.Music, "WRLD.Music", (IWorldspaceGetter r) => r.Music.FormKey, (IWorldspace r, FormKey v) => r.Music.SetTo(v), f);
        AddForwardLink(b, s.Water, "WRLD.Water", (IWorldspaceGetter r) => r.Water.FormKey, (IWorldspace r, FormKey v) => r.Water.SetTo(v), f);
        AddForward(b, s.WorldMapOffsetScale, "WRLD.WorldMapOffsetScale", (IWorldspaceGetter r) => r.WorldMapOffsetScale, (IWorldspace r, float v) => r.WorldMapOffsetScale = v, IsDefault, f);
    }
}
