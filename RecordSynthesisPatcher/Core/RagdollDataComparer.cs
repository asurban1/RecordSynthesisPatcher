using System.Collections.Generic;
using Noggog;

namespace RecordSynthesisPatcher.Core;

// XRGD stores each bone as a one-byte ID, three unused bytes, and six floats.
// Ignore the unused bytes when deciding whether two visible ragdoll values are
// equal, while preserving the selected source's complete payload when writing.
internal sealed class RagdollDataComparer :
    IEqualityComparer<ReadOnlyMemorySlice<byte>?>
{
    private const int BoneSize = 28;
    private const int BoneIdSize = 1;
    private const int PaddingSize = 3;
    private const int TransformOffset = BoneIdSize + PaddingSize;

    public static readonly RagdollDataComparer Instance = new();

    public bool Equals(
        ReadOnlyMemorySlice<byte>? left,
        ReadOnlyMemorySlice<byte>? right)
    {
        if (!left.HasValue || !right.HasValue)
            return left.HasValue == right.HasValue;

        ReadOnlySpan<byte> leftBytes = left.Value.Span;
        ReadOnlySpan<byte> rightBytes = right.Value.Span;
        return PayloadsEqual(leftBytes, rightBytes);
    }

    private static bool PayloadsEqual(
        ReadOnlySpan<byte> leftBytes,
        ReadOnlySpan<byte> rightBytes)
    {
        if (leftBytes.Length != rightBytes.Length)
            return false;

        // Preserve conservative byte-for-byte behavior for unexpected data.
        if (leftBytes.Length % BoneSize != 0)
            return leftBytes.SequenceEqual(rightBytes);

        for (int offset = 0; offset < leftBytes.Length; offset += BoneSize)
        {
            if (leftBytes[offset] != rightBytes[offset] ||
                !leftBytes.Slice(offset + TransformOffset,
                        BoneSize - TransformOffset)
                    .SequenceEqual(rightBytes.Slice(
                        offset + TransformOffset,
                        BoneSize - TransformOffset)))
            {
                return false;
            }
        }

        return true;
    }

    public int GetHashCode(ReadOnlyMemorySlice<byte>? value) => 0;
}
