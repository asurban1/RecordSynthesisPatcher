using System;
using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda.Plugins;

namespace RecordSynthesisPatcher.Core;

public sealed class PluginOverrideNode
{
    internal PluginOverrideNode(ModKey modKey)
    {
        ModKey = modKey;
    }

    public ModKey ModKey { get; }
    public List<PluginOverrideNode> Children { get; } = new();
    public List<PluginOverrideNode> Parents { get; } = new();
}

public sealed class PluginOverrideGraph
{
    internal PluginOverrideGraph(
        ModKey[] winnerFirstKeys,
        PluginOverrideNode root,
        Dictionary<ModKey, PluginOverrideNode> nodes)
    {
        WinnerFirstKeys = winnerFirstKeys;
        Root = root;
        Nodes = nodes;
    }

    internal ModKey[] WinnerFirstKeys { get; }
    public PluginOverrideNode Root { get; }
    public IReadOnlyDictionary<ModKey, PluginOverrideNode> Nodes { get; }

    internal static PluginOverrideGraph Build(
        IReadOnlyList<ModKey> winnerFirstKeys,
        PluginMasterIndex masters)
    {
        if (winnerFirstKeys.Count == 0)
            throw new ArgumentException("An override graph needs at least one plugin.");

        var originFirstKeys = winnerFirstKeys.Reverse().ToArray();
        var nodes = originFirstKeys.ToDictionary(
            key => key,
            key => new PluginOverrideNode(key));

        for (int index = 1; index < originFirstKeys.Length; index++)
        {
            ModKey currentKey = originFirstKeys[index];
            var candidateParents = new List<ModKey>();

            for (int earlier = 0; earlier < index; earlier++)
            {
                ModKey candidate = originFirstKeys[earlier];
                if (masters.IsAncestor(candidate, currentKey))
                    candidateParents.Add(candidate);
            }

            if (candidateParents.Count == 0)
            {
                // No master relationship means this override is an
                // independent branch from the record origin. Treating the
                // preceding load-order override as its parent invents an
                // inheritance relationship and mistakes absent additions for
                // explicit removals.
                candidateParents.Add(originFirstKeys[0]);
            }
            else
            {
                // Keep only the nearest record-owning ancestors on each branch.
                var nearestParents = candidateParents
                    .Where(candidate => !candidateParents.Any(other =>
                        other != candidate && masters.IsAncestor(candidate, other)))
                    .ToArray();

                candidateParents.Clear();
                candidateParents.AddRange(nearestParents);
            }

            foreach (ModKey parentKey in candidateParents)
            {
                var parent = nodes[parentKey];
                var child = nodes[currentKey];
                parent.Children.Add(child);
                child.Parents.Add(parent);
            }
        }

        return new PluginOverrideGraph(
            winnerFirstKeys.ToArray(),
            nodes[originFirstKeys[0]],
            nodes);
    }
}

public sealed class PluginGraphCache
{
    private const int MaximumCachedTopologies = 16_384;

    private readonly PluginMasterIndex _masters;
    private readonly Dictionary<int, List<PluginOverrideGraph>> _graphs = new();
    private int _cachedCount;

    public PluginGraphCache(PluginMasterIndex masters)
    {
        _masters = masters;
    }

    public int CachedTopologyCount => _cachedCount;

    public PluginOverrideGraph GetOrCreate(IReadOnlyList<ModKey> winnerFirstKeys)
    {
        int hash = 17;
        foreach (var modKey in winnerFirstKeys)
            hash = unchecked(hash * 31 + modKey.GetHashCode());

        if (_graphs.TryGetValue(hash, out var bucket))
        {
            foreach (var existing in bucket)
            {
                if (SameKeys(existing.WinnerFirstKeys, winnerFirstKeys))
                    return existing;
            }
        }

        var graph = PluginOverrideGraph.Build(winnerFirstKeys, _masters);

        if (_cachedCount < MaximumCachedTopologies)
        {
            bucket ??= _graphs[hash] = new List<PluginOverrideGraph>();
            bucket.Add(graph);
            _cachedCount++;
        }

        return graph;
    }

    private static bool SameKeys(ModKey[] cached, IReadOnlyList<ModKey> requested)
    {
        if (cached.Length != requested.Count)
            return false;

        for (int index = 0; index < cached.Length; index++)
        {
            if (cached[index] != requested[index])
                return false;
        }

        return true;
    }
}
