using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public static partial class FieldRegistry
{
    // ARMO, INGR, and KEYM keep their existing registrations. This method
    // supplies the remaining Mutagen records exposing the same sound pair.
    private static void RegisterPortableSounds(
        ICollection<IFieldBinding> b,
        PatcherSettings settings,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddForwardLink(b, settings.ALCH.PickUpSound, "ALCH.PickUpSound", (IIngestibleGetter r) => r.PickUpSound.FormKey, (IIngestible r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, settings.ALCH.PutDownSound, "ALCH.PutDownSound", (IIngestibleGetter r) => r.PutDownSound.FormKey, (IIngestible r, FormKey v) => r.PutDownSound.SetTo(v), f);
        AddForwardLink(b, settings.AMMO.PickUpSound, "AMMO.PickUpSound", (IAmmunitionGetter r) => r.PickUpSound.FormKey, (IAmmunition r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, settings.AMMO.PutDownSound, "AMMO.PutDownSound", (IAmmunitionGetter r) => r.PutDownSound.FormKey, (IAmmunition r, FormKey v) => r.PutDownSound.SetTo(v), f);
        AddForwardLink(b, settings.APPA.PickUpSound, "APPA.PickUpSound", (IAlchemicalApparatusGetter r) => r.PickUpSound.FormKey, (IAlchemicalApparatus r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, settings.APPA.PutDownSound, "APPA.PutDownSound", (IAlchemicalApparatusGetter r) => r.PutDownSound.FormKey, (IAlchemicalApparatus r, FormKey v) => r.PutDownSound.SetTo(v), f);
        AddForwardLink(b, settings.BOOK.PickUpSound, "BOOK.PickUpSound", (IBookGetter r) => r.PickUpSound.FormKey, (IBook r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, settings.BOOK.PutDownSound, "BOOK.PutDownSound", (IBookGetter r) => r.PutDownSound.FormKey, (IBook r, FormKey v) => r.PutDownSound.SetTo(v), f);
        AddForwardLink(b, settings.MISC.PickUpSound, "MISC.PickUpSound", (IMiscItemGetter r) => r.PickUpSound.FormKey, (IMiscItem r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, settings.MISC.PutDownSound, "MISC.PutDownSound", (IMiscItemGetter r) => r.PutDownSound.FormKey, (IMiscItem r, FormKey v) => r.PutDownSound.SetTo(v), f);
        AddForwardLink(b, settings.SCRL.PickUpSound, "SCRL.PickUpSound", (IScrollGetter r) => r.PickUpSound.FormKey, (IScroll r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, settings.SCRL.PutDownSound, "SCRL.PutDownSound", (IScrollGetter r) => r.PutDownSound.FormKey, (IScroll r, FormKey v) => r.PutDownSound.SetTo(v), f);
        AddForwardLink(b, settings.SLGM.PickUpSound, "SLGM.PickUpSound", (ISoulGemGetter r) => r.PickUpSound.FormKey, (ISoulGem r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, settings.SLGM.PutDownSound, "SLGM.PutDownSound", (ISoulGemGetter r) => r.PutDownSound.FormKey, (ISoulGem r, FormKey v) => r.PutDownSound.SetTo(v), f);
        AddForwardLink(b, settings.WEAP.PickUpSound, "WEAP.PickUpSound", (IWeaponGetter r) => r.PickUpSound.FormKey, (IWeapon r, FormKey v) => r.PickUpSound.SetTo(v), f);
        AddForwardLink(b, settings.WEAP.PutDownSound, "WEAP.PutDownSound", (IWeaponGetter r) => r.PutDownSound.FormKey, (IWeapon r, FormKey v) => r.PutDownSound.SetTo(v), f);
    }

    private static void RegisterAmbientColors(
        ICollection<IFieldBinding> b,
        PatcherSettings settings,
        IReadOnlyList<IForwardingActionModule> f)
    {
        AddForward<ICell, ICellGetter, IAmbientColorsGetter?>(
            b, settings.CELL.AmbientColors, "CELL.AmbientColors",
            record => record.Lighting?.AmbientColors,
            SetCellAmbientColors,
            value => value is null, f, AmbientColorsComparer.Instance);

        AddForward<ILightingTemplate, ILightingTemplateGetter, IAmbientColorsGetter?>(
            b, settings.LGTM.AmbientColors, "LGTM.AmbientColors",
            record => record.AmbientColors,
            (record, value) => record.AmbientColors =
                value?.DeepCopy() ?? new AmbientColors(),
            value => value is null, f, AmbientColorsComparer.Instance);

        AddForward<ILightingTemplate, ILightingTemplateGetter, IAmbientColorsGetter?>(
            b, settings.LGTM.DirectionalAmbientColors,
            "LGTM.DirectionalAmbientColors",
            record => record.DirectionalAmbientColors,
            (record, value) => record.DirectionalAmbientColors = value?.DeepCopy(),
            value => value is null, f, AmbientColorsComparer.Instance);

        AddForward<IWeather, IWeatherGetter, IAmbientColorsGetter?>(
            b, settings.WTHR.DirectionalAmbientLightingColorsDay,
            "WTHR.DirectionalAmbientLightingColors.Day",
            record => record.DirectionalAmbientLightingColors?.Day,
            SetWeatherDayAmbientColors,
            value => value is null, f, AmbientColorsComparer.Instance);

        AddForward<IWeather, IWeatherGetter, IAmbientColorsGetter?>(
            b, settings.WTHR.DirectionalAmbientLightingColorsNight,
            "WTHR.DirectionalAmbientLightingColors.Night",
            record => record.DirectionalAmbientLightingColors?.Night,
            SetWeatherNightAmbientColors,
            value => value is null, f, AmbientColorsComparer.Instance);

        AddForward<IWeather, IWeatherGetter, IAmbientColorsGetter?>(
            b, settings.WTHR.DirectionalAmbientLightingColorsSunrise,
            "WTHR.DirectionalAmbientLightingColors.Sunrise",
            record => record.DirectionalAmbientLightingColors?.Sunrise,
            SetWeatherSunriseAmbientColors,
            value => value is null, f, AmbientColorsComparer.Instance);

        AddForward<IWeather, IWeatherGetter, IAmbientColorsGetter?>(
            b, settings.WTHR.DirectionalAmbientLightingColorsSunset,
            "WTHR.DirectionalAmbientLightingColors.Sunset",
            record => record.DirectionalAmbientLightingColors?.Sunset,
            SetWeatherSunsetAmbientColors,
            value => value is null, f, AmbientColorsComparer.Instance);
    }

    private static void SetCellAmbientColors(
        ICell record, IAmbientColorsGetter? value)
    {
        if (record.Lighting is null)
        {
            if (value is null)
                return;

            record.Lighting = new CellLighting();
        }

        // AmbientColors is required inside XCLL. A missing source XCLL is
        // represented by its default ambient group so unrelated XCLL members
        // on the winning record are not destroyed.
        record.Lighting.AmbientColors =
            value?.DeepCopy() ?? new AmbientColors();
    }

    private static void SetWeatherDayAmbientColors(
        IWeather record, IAmbientColorsGetter? value)
    {
        EnsureWeatherAmbientColors(record, value);
        if (record.DirectionalAmbientLightingColors is { } colors)
            colors.Day = value?.DeepCopy() ?? new AmbientColors();
    }

    private static void SetWeatherNightAmbientColors(
        IWeather record, IAmbientColorsGetter? value)
    {
        EnsureWeatherAmbientColors(record, value);
        if (record.DirectionalAmbientLightingColors is { } colors)
            colors.Night = value?.DeepCopy() ?? new AmbientColors();
    }

    private static void SetWeatherSunriseAmbientColors(
        IWeather record, IAmbientColorsGetter? value)
    {
        EnsureWeatherAmbientColors(record, value);
        if (record.DirectionalAmbientLightingColors is { } colors)
            colors.Sunrise = value?.DeepCopy() ?? new AmbientColors();
    }

    private static void SetWeatherSunsetAmbientColors(
        IWeather record, IAmbientColorsGetter? value)
    {
        EnsureWeatherAmbientColors(record, value);
        if (record.DirectionalAmbientLightingColors is { } colors)
            colors.Sunset = value?.DeepCopy() ?? new AmbientColors();
    }

    private static void EnsureWeatherAmbientColors(
        IWeather record, IAmbientColorsGetter? value)
    {
        if (record.DirectionalAmbientLightingColors is null && value is not null)
            record.DirectionalAmbientLightingColors =
                new WeatherAmbientColorSet();
    }
}
