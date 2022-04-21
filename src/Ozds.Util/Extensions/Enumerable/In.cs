namespace Ozds.Util;

public static class EnumerableExtensions
{
  public static bool In<T, TElement>(
      this T? @this,
      IEnumerable<TElement>? enumerable = null) where TElement : class =>
    @this
      .As<TElement>()
      .When(element =>
        enumerable?.Contains(element) ?? true,
        false);

  public static bool In<T, TElement>(
      this T? @this,
      params TElement[]? enumerable) where TElement : class =>
    @this
      .As<TElement>()
      .When(element =>
        enumerable?.Contains(element) ?? true,
        false);
}
