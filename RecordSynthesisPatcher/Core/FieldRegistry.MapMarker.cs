using System.Collections.Generic;
using Mutagen.Bethesda.Skyrim;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public static partial class FieldRegistry
{
    private static void RegisterMapMarker(
        ICollection<IFieldBinding> bindings,
        PatcherSettings settings,
        IReadOnlyList<IForwardingActionModule> forwarders,
        IReadOnlyList<IFlagMergingActionModule> flagMergers)
    {
        // Resolve each member independently: forwarding a name must not replace
        // a winning marker type or overwrite independently merged flags.
        AddDescription<IPlacedObject, IPlacedObjectGetter>(
            bindings, settings.REFR.MapMarkerName, "REFR.MapMarker.Name",
            record => record.MapMarker?.Name,
            (record, value) =>
            {
                if (record.MapMarker is null && value is null)
                    return;
                record.MapMarker ??= new MapMarker();
                record.MapMarker.Name = value;
            },
            forwarders);

        // An absent marker has no type/flags. Clearing one member must not
        // delete the entire marker (and with it the other selected members).
        AddForward<IPlacedObject, IPlacedObjectGetter, MapMarker.MarkerType>(
            bindings, settings.REFR.MapMarkerType, "REFR.MapMarker.Type",
            record => record.MapMarker?.Type ?? MapMarker.MarkerType.None,
            (record, value) =>
            {
                if (record.MapMarker is null && value == MapMarker.MarkerType.None)
                    return;
                record.MapMarker ??= new MapMarker();
                record.MapMarker.Type = value;
            },
            _ => false, forwarders);

        AddFlagMerge<IPlacedObject, IPlacedObjectGetter>(
            bindings, settings.REFR.MapMarkerFlagsMerge, "REFR.MapMarker.Flags",
            record => (ulong)(record.MapMarker?.Flags ?? 0),
            (record, value) =>
            {
                if (record.MapMarker is null && value == 0)
                    return;
                record.MapMarker ??= new MapMarker();
                record.MapMarker.Flags = (MapMarker.Flag)value;
            },
            EnumMask<MapMarker.Flag>(), flagMergers);
    }
}
