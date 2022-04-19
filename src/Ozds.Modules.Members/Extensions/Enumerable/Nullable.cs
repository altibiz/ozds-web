namespace Ozds.Modules.Members;

public static partial class IEnumerableExtensions
{
  public static IEnumerable<T?> Nullable<T>(this IEnumerable<T> @this) =>
    @this;

  public static IEnumerable<T> NonNullable<T>(this IEnumerable<T?> @this) =>
    @this.Where(item => item is not null)!;
}
