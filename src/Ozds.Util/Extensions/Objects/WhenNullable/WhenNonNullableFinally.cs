using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut? WhenNonNullableFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector) =>
    !@this.Truthy() ? default :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut WhenNonNullableFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut WhenNonNullableFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullableFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullableFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> WhenNonNullableFinallyTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector) =>
    !@this.Truthy() ? default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullableFinallyTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullableFinallyTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullableFinallyTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullableFinallyTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> WhenNonNullableFinallyValueTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    !@this.Truthy() ? default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullableFinallyValueTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullableFinallyValueTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullableFinallyValueTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullableFinallyValueTask<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    await selector(@this);
}
