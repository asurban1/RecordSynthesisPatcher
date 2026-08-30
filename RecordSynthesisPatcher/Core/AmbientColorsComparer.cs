using System.Collections.Generic;
using Mutagen.Bethesda.Skyrim;

namespace RecordSynthesisPatcher.Core;

internal sealed class AmbientColorsComparer :
    IEqualityComparer<IAmbientColorsGetter?>
{
    public static readonly AmbientColorsComparer Instance = new();

    public bool Equals(
        IAmbientColorsGetter? left, IAmbientColorsGetter? right) =>
        ReferenceEquals(left, right) ||
        left is not null && right is not null &&
        left.DirectionalXMinus.Equals(right.DirectionalXMinus) &&
        left.DirectionalXPlus.Equals(right.DirectionalXPlus) &&
        left.DirectionalYMinus.Equals(right.DirectionalYMinus) &&
        left.DirectionalYPlus.Equals(right.DirectionalYPlus) &&
        left.DirectionalZMinus.Equals(right.DirectionalZMinus) &&
        left.DirectionalZPlus.Equals(right.DirectionalZPlus) &&
        left.Specular.Equals(right.Specular) &&
        left.Scale.Equals(right.Scale);

    public int GetHashCode(IAmbientColorsGetter? value) => 0;
}
