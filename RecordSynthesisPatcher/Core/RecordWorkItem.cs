using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Core;

public sealed class RecordWorkItem<TRecord, TGetter>
    where TRecord : class, IMajorRecord, TGetter
    where TGetter : class, IMajorRecordGetter
{
    private List<IModContext<ISkyrimMod, ISkyrimModGetter, TRecord, TGetter>>? _contexts;
    private Dictionary<ModKey, TGetter>? _recordsByPlugin;
    private PluginOverrideGraph? _defaultGraph;
    private BranchResolutionTopology? _defaultResolutionTopology;
    private TRecord? _patchRecord;

    internal RecordWorkItem(
        PatcherServices services,
        TGetter winner,
        ModKey winningPlugin)
    {
        Services = services;
        Winner = winner;
        WinningPlugin = winningPlugin;
    }

    public PatcherServices Services { get; }
    public TGetter Winner { get; }
    public ModKey WinningPlugin { get; }

    public IReadOnlyList<IModContext<ISkyrimMod, ISkyrimModGetter, TRecord, TGetter>> Contexts =>
        _contexts ??= Services.State.LinkCache
            .ResolveAllContexts<TRecord, TGetter>(Winner.FormKey)
            .ToList();

    public TGetter Original => Contexts[Contexts.Count - 1].Record;

    public TRecord? ExistingOverride => _patchRecord;

    public TGetter GetRecord(ModKey modKey)
    {
        _recordsByPlugin ??= Contexts.ToDictionary(
            context => context.ModKey,
            context => context.Record);

        return _recordsByPlugin[modKey];
    }

    public TRecord GetOrAddOverride()
    {
        return _patchRecord ??= Contexts[0]
            .GetOrAddAsOverride(Services.PatchMod);
    }

    public PluginOverrideGraph GetGraph(IReadOnlySet<ModKey>? excludedPlugins = null)
    {
        if (excludedPlugins is null || excludedPlugins.Count == 0 ||
            !Contexts.Any(context => excludedPlugins.Contains(context.ModKey)))
        {
            return _defaultGraph ??= Services.Graphs.GetOrCreate(
                Contexts.Select(context => context.ModKey).ToArray());
        }

        var filteredKeys = Contexts
            .Where(context => !excludedPlugins.Contains(context.ModKey))
            .Select(context => context.ModKey)
            .ToArray();

        return Services.Graphs.GetOrCreate(filteredKeys);
    }

    internal BranchResolutionTopology GetResolutionTopology()
    {
        if (_defaultResolutionTopology is not null)
            return _defaultResolutionTopology;

        var plugins = new ModKey[Contexts.Count];
        for (int index = 0; index < Contexts.Count; index++)
            plugins[index] = Contexts[index].ModKey;

        return _defaultResolutionTopology = BranchResolutionTopology.Create(
            plugins,
            WinningPlugin,
            GetGraph());
    }
}
