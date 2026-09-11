using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public static partial class FieldRegistry
{
    private static void RegisterWeaponCriticalData(
        ICollection<IFieldBinding> bindings,
        PatcherSettings settings,
        IReadOnlyList<IForwardingActionModule> forwarders)
    {
        // Resolve CRDT members independently; preserve the winner's other
        // members, unused data, and unknown flag bits.
        AddForward<IWeapon, IWeaponGetter, ushort>(
            bindings, settings.WEAP.CriticalDamage, "WEAP.Critical.Damage",
            record => record.Critical?.Damage ?? 0,
            (record, value) =>
            {
                record.Critical ??= new CriticalData();
                record.Critical.Damage = value;
            },
            _ => false, forwarders);

        AddForward<IWeapon, IWeaponGetter, float>(
            bindings, settings.WEAP.CriticalPercentMult, "WEAP.Critical.PercentMult",
            record => record.Critical?.PercentMult ?? 0f,
            (record, value) =>
            {
                record.Critical ??= new CriticalData();
                record.Critical.PercentMult = value;
            },
            _ => false, forwarders);

        AddForward<IWeapon, IWeaponGetter, bool>(
            bindings, settings.WEAP.CriticalOnDeath, "WEAP.Critical.OnDeath",
            record => ((record.Critical?.Flags ?? 0) & CriticalData.Flag.OnDeath) != 0,
            (record, value) =>
            {
                record.Critical ??= new CriticalData();
                if (value) record.Critical.Flags |= CriticalData.Flag.OnDeath;
                else record.Critical.Flags &= ~CriticalData.Flag.OnDeath;
            },
            _ => false, forwarders);

        AddForwardLink<IWeapon, IWeaponGetter>(
            bindings, settings.WEAP.CriticalEffect, "WEAP.Critical.Effect",
            record => record.Critical?.Effect.FormKey ?? FormKey.Null,
            (record, value) =>
            {
                record.Critical ??= new CriticalData();
                record.Critical.Effect.SetTo(value);
            },
            forwarders);
    }
}
