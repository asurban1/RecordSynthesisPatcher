using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace RecordSynthesisPatcher.Core;

public sealed class PluginMasterIndex
{
    private readonly Dictionary<ModKey, ModKey[]> _directMasters;
    private readonly Dictionary<ModKey, HashSet<ModKey>> _ancestorCache = new();

    public PluginMasterIndex(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
    {
        _directMasters = state.LinkCache.ListedOrder.ToDictionary(
            mod => mod.ModKey,
            mod => mod.MasterReferences
                .Select(master => master.Master)
                .Distinct()
                .ToArray());
    }

    public int PluginCount => _directMasters.Count;

    public bool IsAncestor(ModKey possibleAncestor, ModKey possibleDescendant)
    {
        if (possibleAncestor == possibleDescendant)
            return false;

        return GetAncestors(possibleDescendant).Contains(possibleAncestor);
    }

    private HashSet<ModKey> GetAncestors(ModKey modKey)
    {
        if (_ancestorCache.TryGetValue(modKey, out var cached))
            return cached;

        var result = new HashSet<ModKey>();
        CollectAncestors(modKey, result, new HashSet<ModKey>());
        _ancestorCache[modKey] = result;
        return result;
    }

    private void CollectAncestors(
        ModKey modKey,
        HashSet<ModKey> destination,
        HashSet<ModKey> visiting)
    {
        if (!visiting.Add(modKey))
            return;

        if (_ancestorCache.TryGetValue(modKey, out var cached))
        {
            destination.UnionWith(cached);
            visiting.Remove(modKey);
            return;
        }

        if (_directMasters.TryGetValue(modKey, out var directMasters))
        {
            foreach (var master in directMasters)
            {
                destination.Add(master);
                CollectAncestors(master, destination, visiting);
            }
        }

        visiting.Remove(modKey);
    }
}
