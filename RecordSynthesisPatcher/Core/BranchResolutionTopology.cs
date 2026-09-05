using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;

namespace RecordSynthesisPatcher.Core;

// Precomputes the small, immutable part of branch resolution once per record.
// Collection fields may resolve hundreds of keys, so parent and leaf discovery
// must not be rebuilt for every individual entry.
internal sealed class BranchResolutionTopology
{
    private BranchResolutionTopology(
        ModKey[] plugins,
        int[][] parents,
        bool[] leaves,
        int rootIndex,
        int winnerIndex)
    {
        Plugins = plugins;
        Parents = parents;
        Leaves = leaves;
        RootIndex = rootIndex;
        WinnerIndex = winnerIndex;
    }

    internal ModKey[] Plugins { get; }
    internal int[][] Parents { get; }
    internal bool[] Leaves { get; }
    internal int RootIndex { get; }
    internal int WinnerIndex { get; }

    internal static BranchResolutionTopology Create(
        IReadOnlyList<ModKey> winnerFirstPlugins,
        ModKey winningPlugin,
        PluginOverrideGraph graph)
    {
        var plugins = new ModKey[winnerFirstPlugins.Count];
        var indexByPlugin = new Dictionary<ModKey, int>(
            winnerFirstPlugins.Count);
        for (int index = 0; index < winnerFirstPlugins.Count; index++)
        {
            ModKey plugin = winnerFirstPlugins[index];
            plugins[index] = plugin;
            indexByPlugin.Add(plugin, index);
        }

        var parents = new int[plugins.Length][];
        var leaves = new bool[plugins.Length];
        for (int index = 0; index < plugins.Length; index++)
        {
            PluginOverrideNode node = graph.Nodes[plugins[index]];
            var parentIndices = new int[node.Parents.Count];
            for (int parent = 0; parent < node.Parents.Count; parent++)
                parentIndices[parent] = indexByPlugin[node.Parents[parent].ModKey];

            parents[index] = parentIndices;
            leaves[index] = node.Children.Count == 0;
        }

        return new BranchResolutionTopology(
            plugins,
            parents,
            leaves,
            indexByPlugin[graph.Root.ModKey],
            indexByPlugin[winningPlugin]);
    }
}
