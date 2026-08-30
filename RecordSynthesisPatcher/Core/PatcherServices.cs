using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using RecordSynthesisPatcher.Settings;

namespace RecordSynthesisPatcher.Core;

public sealed class PatcherServices
{
    public PatcherServices(
        IPatcherState<ISkyrimMod, ISkyrimModGetter> state,
        ISkyrimMod patchMod,
        PatcherSettings settings)
    {
        State = state;
        PatchMod = patchMod;
        Settings = settings;
        Masters = new PluginMasterIndex(state);
        Graphs = new PluginGraphCache(Masters);

        VerboseRecordLogging = settings.General.VerboseRecordLogging;
        ProgressInterval = settings.General.ProgressInterval;
    }

    public IPatcherState<ISkyrimMod, ISkyrimModGetter> State { get; }
    public ISkyrimMod PatchMod { get; }
    public PatcherSettings Settings { get; }
    public PluginMasterIndex Masters { get; }
    public PluginGraphCache Graphs { get; }

    // Logging every changed record can become a major bottleneck on large lists.
    public bool VerboseRecordLogging { get; set; }
    public int ProgressInterval { get; set; }
}
