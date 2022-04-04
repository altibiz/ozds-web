using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Elasticsearch.Test
{
  public static class AssertExtensions
  {
    // TODO: optimize?
    public static void ElementsEqual<T>(
        IEnumerable<T> expected, IEnumerable<T> actual)
    {
      Assert.True(expected.ToHashSet().SetEquals(actual.ToHashSet()));
    }

    // TODO: optimize?
    public static void Subset<T>(
        IEnumerable<T> expected, IEnumerable<T> actual)
    {
      Assert.Subset(expected.ToHashSet(), actual.ToHashSet());
    }

    // TODO: optimize?
    public static void Superset<T>(
        IEnumerable<T> expected, IEnumerable<T> actual)
    {
      Assert.Superset(expected.ToHashSet(), actual.ToHashSet());
    }

    // TODO: optimize?
    public static void ProperSubset<T>(
        IEnumerable<T> expected, IEnumerable<T> actual)
    {
      Assert.ProperSubset(expected.ToHashSet(), actual.ToHashSet());
    }

    // TODO: optimize?
    public static void ProperSuperset<T>(
        IEnumerable<T> expected, IEnumerable<T> actual)
    {
      Assert.ProperSuperset(expected.ToHashSet(), actual.ToHashSet());
    }

    // TODO: optimize?
    public static void Unique<T>(IEnumerable<T> actual)
    {
      Assert.True(actual.ToHashSet().Count == actual.ToList().Count);
    }

    public static void OneOf<T>(T actual, IEnumerable<T> expected)
    {
      Assert.Contains(actual, expected);
    }
  }
}
