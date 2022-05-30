using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool WithPredicate<T>(
      this T? @this,
      Predicate<T> predicate) =>
    @this is null ? false : predicate(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool WithPredicate<T>(
      this T? @this,
      Func<T, bool> predicate) =>
    @this is null ? false : predicate(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<bool> WithPredicateValueTask<T>(
      this T? @this,
      Func<T, ValueTask<bool>> predicate) =>
    @this is null ? false : await predicate(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool WithPredicate<T>(
      this T? @this,
      Predicate<T> predicate) where T : struct =>
    @this is null ? false : predicate(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool WithPredicate<T>(
      this T? @this,
      Func<T, bool> predicate) where T : struct =>
    @this is null ? false : predicate(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<bool> WithPredicateValueTask<T>(
      this T? @this,
      Func<T, ValueTask<bool>> predicate) where T : struct =>
    @this is null ? false : await predicate(@this.Value);
}
