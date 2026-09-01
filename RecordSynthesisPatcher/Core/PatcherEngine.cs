using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public sealed class PatcherEngine
{
    private readonly PatcherServices _services;

    public PatcherEngine(
        IPatcherState<ISkyrimMod, ISkyrimModGetter> state,
        ISkyrimMod patchMod,
        PatcherSettings settings)
    {
        _services = new PatcherServices(state, patchMod, settings);
    }

    public void Run()
    {
        var stopwatch = Stopwatch.StartNew();
        var modules = DiscoverModules();

        Console.WriteLine("========================================");
        Console.WriteLine("Automatic modular conflict patcher");
        Console.WriteLine($"Indexed plugins: {_services.Masters.PluginCount:N0}");
        Console.WriteLine($"Discovered modules: {modules.Count:N0}");

        foreach (var module in modules)
        {
            Console.WriteLine($"  [{module.Order}] {module.Name}");
            module.Initialize(_services);
        }

        var fieldBindings = FieldRegistry
            .CreateBindings(_services.Settings, modules)
            .ToArray();

        Console.WriteLine($"Enabled registered fields: {fieldBindings.Length:N0}");

        foreach (var binding in fieldBindings)
        {
            Console.WriteLine(
                $"  {binding.FieldName} => {binding.Module.Name}");
        }

        var existingParticipants = modules
            .SelectMany(module => module.GetType()
                .GetInterfaces()
                .Where(type => type.IsGenericType &&
                    type.GetGenericTypeDefinition() == typeof(IRecordModule<,>))
                .Select(type => new RecordParticipant(
                    module,
                    type.GetGenericArguments()[0],
                    type.GetGenericArguments()[1],
                    module,
                    fieldName: null)));

        var configuredParticipants = fieldBindings
            .Select(binding => new RecordParticipant(
                binding.Module,
                binding.RecordType,
                binding.GetterType,
                binding.Processor,
                binding.FieldName));

        var groups = existingParticipants
            .Concat(configuredParticipants)
            .GroupBy(entry => (entry.RecordType, entry.GetterType))
            .OrderBy(group => group.Min(entry => entry.Module.Order))
            .ThenBy(group => group.Key.GetterType.Name, StringComparer.Ordinal)
            .ToList();

        MethodInfo runGroup = typeof(PatcherEngine)
            .GetMethod(nameof(RunGroup), BindingFlags.Instance | BindingFlags.NonPublic)!;

        foreach (var group in groups)
        {
            var participatingModules = group
                .OrderBy(entry => entry.Module.Order)
                .ThenBy(entry => entry.Module.Name, StringComparer.Ordinal)
                .ThenBy(entry => entry.FieldName, StringComparer.Ordinal)
                .ToArray();

            runGroup
                .MakeGenericMethod(group.Key.RecordType, group.Key.GetterType)
                .Invoke(this, new object[] { participatingModules });
        }

        Console.WriteLine("========================================");
        foreach (var module in modules)
            module.Complete(_services);

        Console.WriteLine(
            $"Cached plugin-chain topologies: {_services.Graphs.CachedTopologyCount:N0}");
        Console.WriteLine($"Total elapsed: {stopwatch.Elapsed}");
    }

    private sealed class RecordParticipant
    {
        public RecordParticipant(
            IPatcherModule module,
            Type recordType,
            Type getterType,
            object processor,
            string? fieldName)
        {
            Module = module;
            RecordType = recordType;
            GetterType = getterType;
            Processor = processor;
            FieldName = fieldName;
        }

        public IPatcherModule Module { get; }
        public Type RecordType { get; }
        public Type GetterType { get; }
        public object Processor { get; }
        public string? FieldName { get; }
    }

    private static List<IPatcherModule> DiscoverModules()
    {
        return typeof(PatcherEngine).Assembly
            .GetTypes()
            .Where(type =>
                !type.IsAbstract &&
                !type.IsInterface &&
                typeof(IPatcherModule).IsAssignableFrom(type) &&
                type.GetConstructor(Type.EmptyTypes) is not null)
            .Select(type => (IPatcherModule)System.Activator.CreateInstance(type)!)
            .OrderBy(module => module.Order)
            .ThenBy(module => module.Name, StringComparer.Ordinal)
            .ToList();
    }

    private void RunGroup<TRecord, TGetter>(RecordParticipant[] participants)
        where TRecord : class, IMajorRecord, TGetter
        where TGetter : class, IMajorRecordGetter
    {
        var stopwatch = Stopwatch.StartNew();
        var modules = participants
            .Select(participant =>
                (IRecordModule<TRecord, TGetter>)participant.Processor)
            .ToArray();

        var finalizers = participants
            .Select(participant => participant.Module)
            .Distinct()
            .OfType<IRecordFinalizer<TRecord, TGetter>>()
            .OrderBy(finalizer => finalizer.Order)
            .ThenBy(finalizer => finalizer.Name, StringComparer.Ordinal)
            .ToArray();

        bool includeOriginalRecords =
            modules.Any(module => module.IncludeOriginalRecords) ||
            finalizers.Any(finalizer => finalizer.IncludeOriginalRecords);
        var seen = new HashSet<FormKey>();
        int winners = 0;
        int originalOnly = 0;
        int conflicts = 0;

        Console.WriteLine("----------------------------------------");
        Console.WriteLine(
            $"Scanning {typeof(TGetter).Name} once for " +
            $"{modules.Length:N0} configured action(s).");

        // This mirrors Mutagen's WinningOverrides implementation while retaining
        // the winning plugin's identity. Origin-only records can then be skipped
        // before any override contexts or dependency graphs are materialized.
        foreach (var mod in _services.State.LinkCache.PriorityOrder)
        {
            foreach (var record in mod.EnumerateMajorRecords<TGetter>())
            {
                if (!seen.Add(record.FormKey) || record.IsDeleted)
                    continue;

                winners++;
                bool isOriginalOnly = mod.ModKey == record.FormKey.ModKey;

                if (isOriginalOnly)
                {
                    originalOnly++;
                    if (!includeOriginalRecords)
                        continue;
                }
                else
                {
                    conflicts++;
                }

                var item = new RecordWorkItem<TRecord, TGetter>(
                    _services,
                    record,
                    mod.ModKey);

                foreach (var module in modules)
                {
                    if (!isOriginalOnly || module.IncludeOriginalRecords)
                        module.Process(item);
                }

                foreach (var finalizer in finalizers)
                {
                    if (!isOriginalOnly || finalizer.IncludeOriginalRecords)
                        finalizer.FinalizeRecord(item);
                }

                if (_services.ProgressInterval > 0 &&
                    conflicts > 0 &&
                    conflicts % _services.ProgressInterval == 0)
                {
                    Console.WriteLine(
                        $"  {typeof(TGetter).Name}: {conflicts:N0} conflicts " +
                        $"processed in {stopwatch.Elapsed}.");
                }
            }
        }

        Console.WriteLine($"  Winning records: {winners:N0}");
        Console.WriteLine($"  Untouched originals skipped: {originalOnly:N0}");
        Console.WriteLine($"  Override-chain candidates: {conflicts:N0}");
        Console.WriteLine($"  Scan elapsed: {stopwatch.Elapsed}");
    }
}
