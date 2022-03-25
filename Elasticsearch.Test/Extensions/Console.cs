using System;
using System.Collections.Generic;

namespace Elasticsearch.Test {
  public static class ConsoleExtensions {
    public static void WriteElements<T>(IEnumerable<T> enumerable) {
      Console.WriteLine($"{enumerable}: [");
      foreach (var element in enumerable) {
        Console.WriteLine($"  {element}");
      }
      Console.WriteLine($"]");
    }
  }
}
