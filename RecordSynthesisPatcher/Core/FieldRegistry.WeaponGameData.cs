using System.Collections.Generic;
using Mutagen.Bethesda.Skyrim;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public static partial class FieldRegistry
{
    private static void RegisterWeaponGameData(
        ICollection<IFieldBinding> bindings,
        PatcherSettings settings,
        IReadOnlyList<IForwardingActionModule> forwarders)
    {
        // DATA fields resolve independently; never replace the winner's entire
        // BasicStats block, and do not confuse base damage with CRDT damage.
        AddForward<IWeapon, IWeaponGetter, uint>(
            bindings, settings.WEAP.GameDataValue, "WEAP.BasicStats.Value",
            record => record.BasicStats?.Value ?? 0,
            (record, value) =>
            {
                record.BasicStats ??= new WeaponBasicStats();
                record.BasicStats.Value = value;
            },
            _ => false, forwarders);

        AddForward<IWeapon, IWeaponGetter, float>(
            bindings, settings.WEAP.GameDataWeight, "WEAP.BasicStats.Weight",
            record => record.BasicStats?.Weight ?? 0f,
            (record, value) =>
            {
                record.BasicStats ??= new WeaponBasicStats();
                record.BasicStats.Weight = value;
            },
            _ => false, forwarders);

        AddForward<IWeapon, IWeaponGetter, ushort>(
            bindings, settings.WEAP.GameDataDamage, "WEAP.BasicStats.Damage",
            record => record.BasicStats?.Damage ?? 0,
            (record, value) =>
            {
                record.BasicStats ??= new WeaponBasicStats();
                record.BasicStats.Damage = value;
            },
            _ => false, forwarders);
    }
}
