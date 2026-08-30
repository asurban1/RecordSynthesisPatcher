using System;
using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

// The registry is split by action so the generic modules remain completely
// unaware of Skyrim record types and fields.
public static partial class FieldRegistry
{
    public static IEnumerable<IFieldBinding> CreateBindings(
        PatcherSettings settings,
        IReadOnlyList<IPatcherModule> modules)
    {
        var bindings = new List<IFieldBinding>();
        var forwarders = modules.OfType<IForwardingActionModule>().ToArray();
        var mergers = modules.OfType<IMergingActionModule>().ToArray();
        var flagMergers = modules.OfType<IFlagMergingActionModule>().ToArray();

        RegisterForwarding(bindings, settings, forwarders);
        RegisterGeneratedSafeForwarding(bindings, settings, forwarders);
        RegisterMerging(bindings, settings, mergers);
        RegisterFlagMerging(bindings, settings, flagMergers);
        RegisterGeneratedSafeFlags(bindings, settings, flagMergers);

        return bindings;
    }

    private static void AddForward<TRecord, TGetter, TValue>(
        ICollection<IFieldBinding> bindings,
        bool enabled,
        string name,
        Func<TGetter, TValue> read,
        Action<TRecord, TValue> write,
        Func<TValue, bool> isDefault,
        IReadOnlyList<IForwardingActionModule> forwarders,
        IEqualityComparer<TValue>? comparer = null)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        if (!enabled)
            return;

        var field = new ForwardField<TRecord, TGetter, TValue>(
            name,
            read,
            write,
            isDefault,
            comparer);

        foreach (var forwarder in forwarders)
        {
            bindings.Add(new FieldBinding<TRecord, TGetter>(
                forwarder,
                field.Name,
                item => forwarder.Process(item, field)));
        }
    }

    private static void AddForwardLink<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings,
        bool enabled,
        string name,
        Func<TGetter, FormKey> read,
        Action<TRecord, FormKey> write,
        IReadOnlyList<IForwardingActionModule> forwarders)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        AddForward(
            bindings,
            enabled,
            name,
            read,
            write,
            value => value.IsNull,
            forwarders);
    }

    private static void AddEditorId<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings,
        bool enabled,
        string signature,
        IReadOnlyList<IForwardingActionModule> forwarders)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        AddForward<TRecord, TGetter, string?>(
            bindings,
            enabled,
            signature + ".EditorID",
            record => record.EditorID,
            (record, value) => record.EditorID = value,
            string.IsNullOrWhiteSpace,
            forwarders);
    }

    private static bool IsDefault<T>(T value) =>
        EqualityComparer<T>.Default.Equals(value, default!);

    private static void AddMerge<TRecord, TGetter, TEntry>(
        ICollection<IFieldBinding> bindings,
        bool enabled,
        string name,
        Func<TGetter, IReadOnlyList<TEntry>?> read,
        Func<TEntry, FormKey> getKey,
        Action<TRecord> clear,
        Action<TRecord, TEntry> add,
        IReadOnlyList<IMergingActionModule> mergers)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        if (!enabled)
            return;

        var field = new MergeField<TRecord, TGetter, TEntry>(
            name,
            read,
            entry => getKey(entry),
            key => key is FormKey formKey && !formKey.IsNull,
            clear,
            add);

        foreach (var merger in mergers)
        {
            bindings.Add(new FieldBinding<TRecord, TGetter>(
                merger,
                field.Name,
                item => merger.Process(item, field)));
        }
    }

    private static void AddMergeByKey<TRecord, TGetter, TEntry, TKey>(
        ICollection<IFieldBinding> bindings,
        bool enabled,
        string name,
        Func<TGetter, IReadOnlyList<TEntry>?> read,
        Func<TEntry, TKey> getKey,
        Action<TRecord> clear,
        Action<TRecord, TEntry> add,
        IReadOnlyList<IMergingActionModule> mergers)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        if (!enabled)
            return;

        var field = new MergeField<TRecord, TGetter, TEntry>(
            name, read, entry => getKey(entry), key => key is not null,
            clear, add);

        foreach (var merger in mergers)
        {
            bindings.Add(new FieldBinding<TRecord, TGetter>(
                merger, field.Name, item => merger.Process(item, field)));
        }
    }

    private static void AddFlagMerge<TRecord, TGetter>(
        ICollection<IFieldBinding> bindings,
        bool enabled,
        string name,
        Func<TGetter, ulong> read,
        Action<TRecord, ulong> write,
        ulong mask,
        IReadOnlyList<IFlagMergingActionModule> mergers)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        if (!enabled || mask == 0)
            return;

        var field = new FlagMergeField<TRecord, TGetter>(
            name, read, write, mask);

        foreach (var merger in mergers)
        {
            bindings.Add(new FieldBinding<TRecord, TGetter>(
                merger,
                field.Name,
                item => merger.Process(item, field)));
        }
    }
}
