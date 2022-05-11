namespace Ozds.Util;

public static partial class Enumerables
{
  public static IEnumerable<(T, int)> Index<T>(this IEnumerable<T> @this) =>
    @this.Select((item, index) => (item, index));
}
