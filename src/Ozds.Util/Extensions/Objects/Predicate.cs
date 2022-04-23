using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool WithPredicate<T>(
      this T? @this,
      Predicate<T> predicate) =>
    !@this.Truthy() ? false : predicate(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool WithPredicate<T>(
      this T? @this,
      Func<T, bool> predicate) =>
    !@this.Truthy() ? false : predicate(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<bool> WithPredicateTask<T>(
      this T? @this,
      Func<T, Task<bool>> predicate) =>
    !@this.Truthy() ? false.ToTask() : predicate(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<bool> WithPredicateValueTask<T>(
      this T? @this,
      Func<T, ValueTask<bool>> predicate) =>
    !@this.Truthy() ? false.ToValueTask() : predicate(@this);
}
