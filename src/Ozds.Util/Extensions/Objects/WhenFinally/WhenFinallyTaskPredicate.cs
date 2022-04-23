using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> WhenFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, TOut> selector) =>
    !@this.Truthy() ? default :
    !await predicate(@this) ? default :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, TOut> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    !await predicate(@this) ? @default :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, TOut> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    !await predicate(@this) ? @default() :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, TOut> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !await predicate(@this) ? await @default() :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, TOut> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !await predicate(@this) ? await @default() :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> WhenFinallyTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, Task<TOut>> selector) =>
    !@this.Truthy() ? default :
    !await predicate(@this) ? default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenFinallyTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, Task<TOut>> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    !await predicate(@this) ? @default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenFinallyTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, Task<TOut>> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    !await predicate(@this) ? @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenFinallyTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, Task<TOut>> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !await predicate(@this) ? await @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenFinallyTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, Task<TOut>> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !await predicate(@this) ? await @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> WhenFinallyValueTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, ValueTask<TOut>> selector) =>
    !@this.Truthy() ? default :
    !await predicate(@this) ? default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenFinallyValueTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, ValueTask<TOut>> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    !await predicate(@this) ? @default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenFinallyValueTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, ValueTask<TOut>> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    !await predicate(@this) ? @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenFinallyValueTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, ValueTask<TOut>> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !await predicate(@this) ? await @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenFinallyValueTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, ValueTask<TOut>> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !await predicate(@this) ? await @default() :
    await selector(@this);
}
