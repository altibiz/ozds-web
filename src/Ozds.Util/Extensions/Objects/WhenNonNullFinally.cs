using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut? WhenNonNullFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector) =>
    @this is null ? default :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut WhenNonNullFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      TOut @default) =>
    @this is null ? @default :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut WhenNonNullFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      Func<TOut> @default) =>
    @this is null ? @default() :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      Func<Task<TOut>> @default) =>
    @this is null ? await @default() :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      Func<ValueTask<TOut>> @default) =>
    @this is null ? await @default() :
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector) =>
    @this is null ? default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      TOut @default) =>
    @this is null ? @default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      Func<TOut> @default) =>
    @this is null ? @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      Func<Task<TOut>> @default) =>
    @this is null ? await @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      Func<ValueTask<TOut>> @default) =>
    @this is null ? await @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    @this is null ? default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      TOut @default) =>
    @this is null ? @default :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      Func<TOut> @default) =>
    @this is null ? @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      Func<Task<TOut>> @default) =>
    @this is null ? await @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      Func<ValueTask<TOut>> @default) =>
    @this is null ? await @default() :
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut? WhenNonNullFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector) where TIn : struct =>
    @this is null ? default :
    selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut WhenNonNullFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      TOut @default) where TIn : struct =>
    @this is null ? @default :
    selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut WhenNonNullFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      Func<TOut> @default) where TIn : struct =>
    @this is null ? @default() :
    selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      Func<Task<TOut>> @default) where TIn : struct =>
    @this is null ? await @default() :
    selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinally<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut> selector,
      Func<ValueTask<TOut>> @default) where TIn : struct =>
    @this is null ? await @default() :
    selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector) where TIn : struct =>
    @this is null ? default :
    await selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      TOut @default) where TIn : struct =>
    @this is null ? @default :
    await selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      Func<TOut> @default) where TIn : struct =>
    @this is null ? @default() :
    await selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      Func<Task<TOut>> @default) where TIn : struct =>
    @this is null ? await @default() :
    await selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut>> selector,
      Func<ValueTask<TOut>> @default) where TIn : struct =>
    @this is null ? await @default() :
    await selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector) where TIn : struct =>
    @this is null ? default :
    await selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      TOut @default) where TIn : struct =>
    @this is null ? @default :
    await selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      Func<TOut> @default) where TIn : struct =>
    @this is null ? @default() :
    await selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      Func<Task<TOut>> @default) where TIn : struct =>
    @this is null ? await @default() :
    await selector(@this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullFinallyAsync<TIn, TOut>(
      this TIn? @this,
      Func<TIn, ValueTask<TOut>> selector,
      Func<ValueTask<TOut>> @default) where TIn : struct =>
    @this is null ? await @default() :
    await selector(@this.Value);
}
