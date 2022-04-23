using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut? When<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, TOut?> selector) =>
    !@this.Truthy() ? default :
    !predicate(@this) ? default :
    selector(@this) ?? default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut When<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, TOut?> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    !predicate(@this) ? @default :
    selector(@this) ?? @default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut When<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, TOut?> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    !predicate(@this) ? @default() :
    selector(@this) ?? @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> When<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, TOut?> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !predicate(@this) ? await @default() :
    selector(@this) ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> When<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, TOut?> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !predicate(@this) ? await @default() :
    selector(@this) ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> WhenTask<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, Task<TOut?>> selector) =>
    !@this.Truthy() ? default :
    !predicate(@this) ? default :
    await selector(@this) ?? default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenTask<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, Task<TOut?>> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    !predicate(@this) ? @default :
    await selector(@this) ?? @default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenTask<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, Task<TOut?>> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    !predicate(@this) ? @default() :
    await selector(@this) ?? @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenTask<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, Task<TOut?>> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !predicate(@this) ? await @default() :
    await selector(@this) ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenTask<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, Task<TOut?>> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !predicate(@this) ? await @default() :
    await selector(@this) ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> WhenValueTask<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, ValueTask<TOut?>> selector) =>
    !@this.Truthy() ? default :
    !predicate(@this) ? default :
    await selector(@this) ?? default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenValueTask<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, ValueTask<TOut?>> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    !predicate(@this) ? @default :
    await selector(@this) ?? @default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenValueTask<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    !predicate(@this) ? @default() :
    await selector(@this) ?? @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenValueTask<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !predicate(@this) ? await @default() :
    await selector(@this) ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenValueTask<TIn, TOut>(
      this TIn? @this,
      Predicate<TIn> predicate,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !predicate(@this) ? await @default() :
    await selector(@this) ?? await @default();
}
