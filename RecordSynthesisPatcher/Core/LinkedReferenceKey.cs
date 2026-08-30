using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Core;

internal readonly record struct LinkedReferenceKey(
    FormKey KeywordOrReference,
    FormKey Reference)
{
    public static LinkedReferenceKey? From(ILinkedReferencesGetter entry)
    {
        FormKey reference = entry.Reference.FormKey;
        return reference.IsNull
            ? null
            : new LinkedReferenceKey(
                entry.KeywordOrReference.FormKey,
                reference);
    }
}
