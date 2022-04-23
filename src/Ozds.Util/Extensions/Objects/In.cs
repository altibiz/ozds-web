using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T>(
      this T? @this,
      IEnumerable<T?>? enumerable) =>
    @this
      .WhenNonNullable(
        element => enumerable
          .WhenNonNullable(
            enumerable => enumerable.Contains(element),
            false),
        false);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T, TElement>(
      this T? @this,
      params T?[]? enumerable) =>
    @this
      .WhenNonNullable(
        element => enumerable
          .WhenNonNullable(
            enumerable => enumerable.Contains(element),
            false),
        false);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T, TElement>(
      this T? @this,
      IEnumerable<TElement?>? enumerable) where TElement : class =>
    @this
      .As<TElement>()
      .WhenNonNullable(
        element => enumerable
          .WhenNonNullable(
            enumerable => enumerable.Contains(element),
            false),
        false);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T, TElement>(
      this T? @this,
      params TElement?[]? enumerable) where TElement : class =>
    @this
      .As<TElement>()
      .WhenNonNullable(
        element => enumerable
          .WhenNonNullable(
            enumerable => enumerable.Contains(element),
            false),
        false);
}
