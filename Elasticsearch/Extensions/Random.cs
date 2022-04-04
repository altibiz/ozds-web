using System;

namespace Elasticsearch;

public readonly record struct MinMax(int Min, int Max);

public static class RandomExtensions {
  public static int Next(this Random rand, MinMax minMax) {
    return rand.Next(minMax.Min, minMax.Max);
  }
}
