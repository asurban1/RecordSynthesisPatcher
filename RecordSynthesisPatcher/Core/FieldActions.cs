using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;

namespace RecordSynthesisPatcher.Core;

// Action modules understand the shape of a field, never the specific Skyrim
// record or property that the registry associates with that action.
public interface IForwardingActionModule : IPatcherModule
{
    void Process<TRecord, TGetter, TValue>(
        RecordWorkItem<TRecord, TGetter> item,
        ForwardField<TRecord, TGetter, TValue> field)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter;
}

public interface IMergingActionModule : IPatcherModule
{
    void Process<TRecord, TGetter, TEntry>(
        RecordWorkItem<TRecord, TGetter> item,
        MergeField<TRecord, TGetter, TEntry> field)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter;
}

public interface IFlagMergingActionModule : IPatcherModule
{
    void Process<TRecord, TGetter>(
        RecordWorkItem<TRecord, TGetter> item,
        FlagMergeField<TRecord, TGetter> field)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter;
}

public sealed class ForwardField<TRecord, TGetter, TValue>
    where TRecord : class, IMajorRecord, TGetter
    where TGetter : class, IMajorRecordGetter
{
    public ForwardField(
        string name,
        Func<TGetter, TValue> read,
        Action<TRecord, TValue> write,
        Func<TValue, bool> isDefault,
        IEqualityComparer<TValue>? comparer = null)
    {
        Name = name;
        Read = read;
        Write = write;
        // Retained as registration metadata for compatibility. Forwarding no
        // longer rejects default values: a default that differs from the
        // original is an explicit clear/removal.
        IsDefault = isDefault;
        Comparer = comparer ?? EqualityComparer<TValue>.Default;
    }

    public string Name { get; }
    public Func<TGetter, TValue> Read { get; }
    public Action<TRecord, TValue> Write { get; }
    public Func<TValue, bool> IsDefault { get; }
    public IEqualityComparer<TValue> Comparer { get; }
}

public sealed class MergeField<TRecord, TGetter, TEntry>
    where TRecord : class, IMajorRecord, TGetter
    where TGetter : class, IMajorRecordGetter
{
    public MergeField(
        string name,
        Func<TGetter, IReadOnlyList<TEntry>?> read,
        Func<TEntry, object?> getKey,
        Func<object?, bool> isValidKey,
        Action<TRecord> clear,
        Action<TRecord, TEntry> add)
    {
        Name = name;
        Read = read;
        GetKey = getKey;
        IsValidKey = isValidKey;
        Clear = clear;
        Add = add;
    }

    public string Name { get; }
    public Func<TGetter, IReadOnlyList<TEntry>?> Read { get; }
    public Func<TEntry, object?> GetKey { get; }
    public Func<object?, bool> IsValidKey { get; }
    public Action<TRecord> Clear { get; }
    public Action<TRecord, TEntry> Add { get; }
}

public sealed class FlagMergeField<TRecord, TGetter>
    where TRecord : class, IMajorRecord, TGetter
    where TGetter : class, IMajorRecordGetter
{
    public FlagMergeField(
        string name,
        Func<TGetter, ulong> read,
        Action<TRecord, ulong> write,
        ulong mask)
    {
        Name = name;
        Read = read;
        Write = write;
        Mask = mask;
    }

    public string Name { get; }
    public Func<TGetter, ulong> Read { get; }
    public Action<TRecord, ulong> Write { get; }
    public ulong Mask { get; }
}

public interface IFieldBinding
{
    IPatcherModule Module { get; }
    string FieldName { get; }
    Type RecordType { get; }
    Type GetterType { get; }
    object Processor { get; }
}

internal sealed class FieldBinding<TRecord, TGetter> :
    IFieldBinding,
    IRecordModule<TRecord, TGetter>
    where TRecord : class, IMajorRecord, TGetter
    where TGetter : class, IMajorRecordGetter
{
    private readonly Action<RecordWorkItem<TRecord, TGetter>> _process;

    public FieldBinding(
        IPatcherModule module,
        string fieldName,
        Action<RecordWorkItem<TRecord, TGetter>> process)
    {
        Module = module;
        FieldName = fieldName;
        _process = process;
    }

    public IPatcherModule Module { get; }
    public string FieldName { get; }
    public Type RecordType => typeof(TRecord);
    public Type GetterType => typeof(TGetter);
    public object Processor => this;

    public string Name => Module.Name;
    public int Order => Module.Order;
    public bool IncludeOriginalRecords => Module.IncludeOriginalRecords;

    public void Process(RecordWorkItem<TRecord, TGetter> item) => _process(item);

    // The engine manages the owning module's lifecycle exactly once.
    public void Initialize(PatcherServices services) { }
    public void Complete(PatcherServices services) { }
}
