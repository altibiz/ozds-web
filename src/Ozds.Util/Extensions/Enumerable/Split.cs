namespace Ozds.Util;

public static partial class IEnumerableExtensions
{
  public static IEnumerable<IEnumerable<T>> Split<T>(
      this IEnumerable<T> @this,
      Predicate<T> on)
  {
    using (var enumerator = @this.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        yield return enumerator.Split(on);
      }
    }
  }

  private static IEnumerable<T> Split<T>(
      this IEnumerator<T> @this,
      Predicate<T> on)
  {
    while (!on(@this.Current) && @this.MoveNext())
    {
      yield return @this.Current;
    }
  }

  public static IEnumerable<IEnumerable<T>> Split<T>(
      this IEnumerable<T> @this,
      int every)
  {
    using (var enumerator = @this.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        yield return enumerator.Split(every);
      }
    }
  }

  private static IEnumerable<T> Split<T>(
      this IEnumerator<T> @this,
      int every)
  {
    for (var count = 0; count < every && @this.MoveNext(); count++)
    {
      yield return @this.Current;
    }
  }
}
