using System.Runtime.CompilerServices;

namespace Ozds.Util;

public readonly record struct MinMax
(int Min, int Max);

public static class RandomExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Next(this Random rand, MinMax minMax) =>
    rand.Next(minMax.Min, minMax.Max);
}
