using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut? WhenNonNullable<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, TOut?> selector) where TIn : struct =>
    !@this.HasValue ? default :
    selector(@this.Value) ?? default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut WhenNonNullable<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, TOut?> selector,
      TOut @default) where TIn : struct =>
    !@this.HasValue ? @default :
    selector(@this.Value) ?? @default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut WhenNonNullable<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<TOut> @default) where TIn : struct =>
    !@this.HasValue ? @default() :
    selector(@this.Value) ?? @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullable<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<Task<TOut>> @default) where TIn : struct =>
    !@this.HasValue ? await @default() :
    selector(@this.Value) ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullable<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, TOut?> selector,
      Func<ValueTask<TOut>> @default) where TIn : struct =>
    !@this.HasValue ? await @default() :
    selector(@this.Value) ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut?> WhenNonNullableTask<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, Task<TOut?>> selector) where TIn : struct =>
    !@this.HasValue ? default :
    await selector(@this.Value) ?? default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullableTask<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, Task<TOut?>> selector,
      TOut @default) where TIn : struct =>
    !@this.HasValue ? @default :
    await selector(@this.Value) ?? @default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullableTask<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, Task<TOut?>> selector,
      Func<TOut> @default) where TIn : struct =>
    !@this.HasValue ? @default() :
    await selector(@this.Value) ?? @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullableTask<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, Task<TOut?>> selector,
      Func<Task<TOut>> @default) where TIn : struct =>
    !@this.HasValue ? await @default() :
    await selector(@this.Value) ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> WhenNonNullableTask<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, Task<TOut?>> selector,
      Func<ValueTask<TOut>> @default) where TIn : struct =>
    !@this.HasValue ? await @default() :
    await selector(@this.Value) ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut?> WhenNonNullableValueTask<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector) where TIn : struct =>
    !@this.HasValue ? default :
    await selector(@this.Value) ?? default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullableValueTask<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      TOut @default) where TIn : struct =>
    !@this.HasValue ? @default :
    await selector(@this.Value) ?? @default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullableValueTask<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<TOut> @default) where TIn : struct =>
    !@this.HasValue ? @default() :
    await selector(@this.Value) ?? @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullableValueTask<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<Task<TOut>> @default) where TIn : struct =>
    !@this.HasValue ? await @default() :
    await selector(@this.Value) ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> WhenNonNullableValueTask<TIn, TOut>(
      this Nullable<TIn> @this,
      Func<TIn, ValueTask<TOut?>> selector,
      Func<ValueTask<TOut>> @default) where TIn : struct =>
    !@this.HasValue ? await @default() :
    await selector(@this.Value) ?? await @default();
}
