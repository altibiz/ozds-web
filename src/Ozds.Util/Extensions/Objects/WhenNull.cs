using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WhenNull<T>(
      this T? @this,
      T @default) =>
    @this is null ? @default :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenNullAsync<T>(
      this T? @this,
      Task<T> @default) =>
    @this is null ? await @default :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenNullAsync<T>(
      this T? @this,
      ValueTask<T> @default) =>
    @this is null ? await @default :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WhenNull<T>(
      this T? @this,
      Func<T> @default) =>
    @this is null ? @default() :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenNullAsync<T>(
      this T? @this,
      Func<Task<T>> @default) =>
    @this is null ? await @default() :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenNullAsync<T>(
      this T? @this,
      Func<ValueTask<T>> @default) =>
    @this is null ? await @default() :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WhenNull<T>(
      this T? @this,
      T @default) where T : struct =>
    @this is null ? @default :
    @this.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenNullAsync<T>(
      this T? @this,
      Task<T> @default) where T : struct =>
    @this is null ? await @default :
    @this.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenNullAsync<T>(
      this T? @this,
      ValueTask<T> @default) where T : struct =>
    @this is null ? await @default :
    @this.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WhenNull<T>(
      this T? @this,
      Func<T> @default) where T : struct =>
    @this is null ? @default() :
    @this.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenNullAsync<T>(
      this T? @this,
      Func<Task<T>> @default) where T : struct =>
    @this is null ? await @default() :
    @this.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenNullAsync<T>(
      this T? @this,
      Func<ValueTask<T>> @default) where T : struct =>
    @this is null ? await @default() :
    @this.Value;
}
