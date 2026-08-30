using System.Collections.Generic;
using System.Drawing;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public static partial class FieldRegistry
{
    private static void RegisterQueuedForwarding(
        ICollection<IFieldBinding> bindings,
        PatcherSettings settings,
        IReadOnlyList<IForwardingActionModule> forwarders)
    {
        RegisterBodyTemplateArmorType<IArmorAddon, IArmorAddonGetter>(
            bindings,
            settings.ARMA.BipedBodyTemplateArmorType,
            "ARMA.BipedBodyTemplate.ArmorType",
            record => record.BodyTemplate,
            (record, value) => record.BodyTemplate = value,
            forwarders);

        AddForward<IArmorAddon, IArmorAddonGetter,
            IGenderedItemGetter<IModelGetter?>?>(
            bindings,
            settings.ARMA.FirstPersonModel,
            "ARMA.FirstPersonModel",
            record => record.FirstPersonModel,
            (record, value) => record.FirstPersonModel = CopyGenderedModel(value),
            value => value is null,
            forwarders,
            ComplexFieldComparers.GenderedModel);

        RegisterBodyTemplateArmorType<IArmor, IArmorGetter>(
            bindings,
            settings.ARMO.BipedBodyTemplateArmorType,
            "ARMO.BipedBodyTemplate.ArmorType",
            record => record.BodyTemplate,
            (record, value) => record.BodyTemplate = value,
            forwarders);

        RegisterBodyTemplateArmorType<IRace, IRaceGetter>(
            bindings,
            settings.RACE.BipedBodyTemplateArmorType,
            "RACE.BipedBodyTemplate.ArmorType",
            record => record.BodyTemplate,
            (record, value) => record.BodyTemplate = value,
            forwarders);

        AddForward<IPlacedObject, IPlacedObjectGetter, P3Float?>(
            bindings,
            settings.REFR.PrimitiveBounds,
            "REFR.Primitive.Bounds",
            record => record.Primitive?.Bounds,
            SetPrimitiveBounds,
            value => value is null,
            forwarders);

        AddForward<IPlacedObject, IPlacedObjectGetter, Color?>(
            bindings,
            settings.REFR.PrimitiveColor,
            "REFR.Primitive.Color",
            record => record.Primitive?.Color,
            SetPrimitiveColor,
            value => value is null,
            forwarders);

        AddForward<IPlacedObject, IPlacedObjectGetter,
            PlacedPrimitive.TypeEnum?>(
            bindings,
            settings.REFR.PrimitiveType,
            "REFR.Primitive.Type",
            record => record.Primitive?.Type,
            SetPrimitiveType,
            value => value is null,
            forwarders);

        AddForward<IPlacedObject, IPlacedObjectGetter, float?>(
            bindings,
            settings.REFR.PrimitiveUnknown,
            "REFR.Primitive.Unknown",
            record => record.Primitive?.Unknown,
            SetPrimitiveUnknown,
            value => value is null,
            forwarders);

        AddForward<IPlacedObject, IPlacedObjectGetter,
            ITeleportDestinationGetter?>(
            bindings,
            settings.REFR.TeleportDestination,
            "REFR.TeleportDestination",
            record => record.TeleportDestination,
            (record, value) =>
                record.TeleportDestination = value?.DeepCopy(),
            value => value is null,
            forwarders,
            ComplexFieldComparers.TeleportDestination);
    }

    private static void RegisterBodyTemplateArmorType<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings,
        bool enabled,
        string name,
        System.Func<TGetter, IBodyTemplateGetter?> readTemplate,
        System.Action<TRecord, BodyTemplate?> writeTemplate,
        IReadOnlyList<IForwardingActionModule> forwarders)
        where TRecord : class, Mutagen.Bethesda.Plugins.Records.IMajorRecord, TGetter
        where TGetter : class, Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter
    {
        AddForward<TRecord, TGetter, ArmorType?>(
            bindings,
            enabled,
            name,
            record => readTemplate(record)?.ArmorType,
            (record, value) =>
            {
                if (!value.HasValue)
                {
                    writeTemplate(record, null);
                    return;
                }

                BodyTemplate template = readTemplate(record)?.DeepCopy() ?? new();
                template.ArmorType = value.Value;
                writeTemplate(record, template);
            },
            value => value is null,
            forwarders);
    }

    private static void RegisterBipedBodyTemplateFlags(
        ICollection<IFieldBinding> bindings,
        PatcherSettings settings,
        IReadOnlyList<IFlagMergingActionModule> mergers)
    {
        RegisterBodyTemplateFlags<IArmorAddon, IArmorAddonGetter>(
            bindings,
            settings.ARMA.BipedBodyTemplateFirstPersonFlagsMerge,
            "ARMA",
            record => record.BodyTemplate,
            (record, value) => record.BodyTemplate = value,
            mergers);

        RegisterBodyTemplateFlags<IArmor, IArmorGetter>(
            bindings,
            settings.ARMO.BipedBodyTemplateFirstPersonFlagsMerge,
            "ARMO",
            record => record.BodyTemplate,
            (record, value) => record.BodyTemplate = value,
            mergers);

        RegisterBodyTemplateFlags<IRace, IRaceGetter>(
            bindings,
            settings.RACE.BipedBodyTemplateFirstPersonFlagsMerge,
            "RACE",
            record => record.BodyTemplate,
            (record, value) => record.BodyTemplate = value,
            mergers);
    }

    private static void RegisterBodyTemplateFlags<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings,
        bool firstPersonEnabled,
        string signature,
        System.Func<TGetter, IBodyTemplateGetter?> readTemplate,
        System.Action<TRecord, BodyTemplate?> writeTemplate,
        IReadOnlyList<IFlagMergingActionModule> mergers)
        where TRecord : class, Mutagen.Bethesda.Plugins.Records.IMajorRecord, TGetter
        where TGetter : class, Mutagen.Bethesda.Plugins.Records.IMajorRecordGetter
    {
        AddFlagMerge<TRecord, TGetter>(
            bindings,
            firstPersonEnabled,
            signature + ".BipedBodyTemplate.FirstPersonFlags",
            record => (ulong)(readTemplate(record)?.FirstPersonFlags ?? 0),
            (record, value) =>
            {
                BodyTemplate template = readTemplate(record)?.DeepCopy() ?? new();
                template.FirstPersonFlags = (BipedObjectFlag)value;
                writeTemplate(record, template);
            },
            EnumMask<BipedObjectFlag>(),
            mergers);
    }

    private static GenderedItem<Model?>? CopyGenderedModel(
        IGenderedItemGetter<IModelGetter?>? value) =>
        value is null
            ? null
            : new GenderedItem<Model?>(
                value.Male?.DeepCopy(),
                value.Female?.DeepCopy());

    private static PlacedPrimitive EnsurePrimitive(IPlacedObject record) =>
        record.Primitive ??= new PlacedPrimitive();

    private static void SetPrimitiveBounds(IPlacedObject record, P3Float? value)
    {
        if (!value.HasValue)
            record.Primitive = null;
        else
            EnsurePrimitive(record).Bounds = value.Value;
    }

    private static void SetPrimitiveColor(IPlacedObject record, Color? value)
    {
        if (!value.HasValue)
            record.Primitive = null;
        else
            EnsurePrimitive(record).Color = value.Value;
    }

    private static void SetPrimitiveType(
        IPlacedObject record,
        PlacedPrimitive.TypeEnum? value)
    {
        if (!value.HasValue)
            record.Primitive = null;
        else
            EnsurePrimitive(record).Type = value.Value;
    }

    private static void SetPrimitiveUnknown(IPlacedObject record, float? value)
    {
        if (!value.HasValue)
            record.Primitive = null;
        else
            EnsurePrimitive(record).Unknown = value.Value;
    }
}
