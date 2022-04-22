namespace Ozds.Util;

public static partial class IEnumerableExtensions
{
  public static bool IsEmpty<T>(
      this IEnumerable<T>? @this) =>
    @this is null || @this.FirstOrDefault() is null;

  public static bool IsEmpty<T>(
      this IAsyncEnumerable<T>? @this) =>
    @this is null ||
    Lambda.Create(async () =>
      {
        await foreach (var _ in @this)
        {
          return false;
        }

        return true;
      })().Result;
}
