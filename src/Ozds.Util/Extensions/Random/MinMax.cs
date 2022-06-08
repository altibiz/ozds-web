using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public readonly record struct MinMax
(int Min, int Max);

public static class RandomExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Next(this Random rand, MinMax minMax) =>
    rand.Next(minMax.Min, minMax.Max);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static decimal NextDecimal(this Random rand, MinMax minMax) =>
    (decimal)(rand.NextDouble() * (minMax.Max - minMax.Min) + minMax.Min);
}
