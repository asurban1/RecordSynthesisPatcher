using Mutagen.Bethesda.Plugins.Records;

namespace RecordSynthesisPatcher.Core;

public interface IPatcherModule
{
    string Name { get; }
    int Order { get; }
    bool IncludeOriginalRecords { get; }

    void Initialize(PatcherServices services);
    void Complete(PatcherServices services);
}

public abstract class PatcherModule : IPatcherModule
{
    public abstract string Name { get; }
    public virtual int Order => 0;
    public virtual bool IncludeOriginalRecords => false;

    public virtual void Initialize(PatcherServices services) { }
    public virtual void Complete(PatcherServices services) { }
}

public interface IRecordModule<TRecord, TGetter> : IPatcherModule
    where TRecord : class, IMajorRecord, TGetter
    where TGetter : class, IMajorRecordGetter
{
    void Process(RecordWorkItem<TRecord, TGetter> item);
}
