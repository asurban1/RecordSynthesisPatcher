using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins.Records;
using RecordSynthesisPatcher.Core;

namespace RecordSynthesisPatcher.Modules;

// This action has no knowledge of record signatures, concrete record types,
// property names, or settings. The registry supplies all field behavior.
public sealed class ForwarderModule : PatcherModule, IForwardingActionModule
{
    private readonly Dictionary<string, int> _forwardedByField =
        new(StringComparer.Ordinal);

    public override string Name => "Forward configured scalar fields";
    public override int Order => 100;

    public void Process<TRecord, TGetter, TValue>(
        RecordWorkItem<TRecord, TGetter> item,
        ForwardField<TRecord, TGetter, TValue> field)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        if (item.Contexts.Count < 3)
            return;

        TValue sourceValue;
        int sourceIndex;

        if (BranchValueResolver.TryResolve(
                item,
                field.Read,
                field.Comparer,
                out sourceValue,
                out sourceIndex))
        {
            Forward(item, field, sourceValue, sourceIndex);
        }
    }

    public override void Complete(PatcherServices services)
    {
        if (_forwardedByField.Count == 0)
            Console.WriteLine("Forwarding: no field values needed updating.");

        foreach (var entry in _forwardedByField)
        {
            Console.WriteLine(
                $"{entry.Key}: {entry.Value:N0} field values forwarded.");
        }

    }

    private void Forward<TRecord, TGetter, TValue>(
        RecordWorkItem<TRecord, TGetter> item,
        ForwardField<TRecord, TGetter, TValue> field,
        TValue sourceValue,
        int sourceIndex)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        field.Write(item.GetOrAddOverride(), sourceValue);

        _forwardedByField.TryGetValue(field.Name, out int forwarded);
        _forwardedByField[field.Name] = forwarded + 1;

        if (item.Services.VerboseRecordLogging)
        {
            Console.WriteLine(
                $"{field.Name}: {item.Winner.FormKey} " +
                $"source={item.Contexts[sourceIndex].ModKey} " +
                $"value={sourceValue}");
        }
    }
}
