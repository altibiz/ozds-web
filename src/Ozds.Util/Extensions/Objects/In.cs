using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T>(
      this T? @this,
      IEnumerable<T?>? enumerable) =>
    @this switch
    {
      null => false,
      not null =>
        enumerable switch
        {
          null => false,
          not null => enumerable.Contains(@this)
        }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T, TElement>(
      this T? @this,
      params T?[]? enumerable) =>
    @this switch
    {
      null => false,
      not null =>
        enumerable switch
        {
          null => false,
          not null => enumerable.Contains(@this)
        }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T, TElement>(
      this T? @this,
      IEnumerable<TElement?>? enumerable)
        where TElement : class =>
    @this switch
    {
      TElement element =>
        enumerable switch
        {
          null => false,
          not null => enumerable.Contains(element)
        },
      _ => false,
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T, TElement>(
      this T? @this,
      params TElement?[]? enumerable)
        where TElement : class =>
    @this switch
    {
      TElement element =>
        enumerable switch
        {
          null => false,
          not null => enumerable.Contains(element)
        },
      _ => false,
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T>(
      this T? @this,
      IEnumerable<T?>? enumerable)
        where T : struct =>
    @this switch
    {
      null => false,
      not null =>
        enumerable switch
        {
          null => false,
          not null => enumerable.Contains(@this)
        }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T, TElement>(
      this T? @this,
      params T?[]? enumerable)
        where T : struct =>
    @this switch
    {
      null => false,
      not null =>
        enumerable switch
        {
          null => false,
          not null => enumerable.Contains(@this)
        }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T, TElement>(
      this T? @this,
      IEnumerable<TElement?>? enumerable)
        where TElement : class
        where T : struct =>
    @this switch
    {
      TElement element =>
        enumerable switch
        {
          null => false,
          not null => enumerable.Contains(element)
        },
      _ => false,
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool In<T, TElement>(
      this T? @this,
      params TElement?[]? enumerable)
        where TElement : class
        where T : struct =>
    @this switch
    {
      TElement element =>
        enumerable switch
        {
          null => false,
          not null => enumerable.Contains(element)
        },
      _ => false,
    };
}
