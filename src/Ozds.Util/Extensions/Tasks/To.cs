using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<TOut> ToTask<TOut>(
      this TOut @this) =>
    Task.FromResult(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task ToTask(
      this ValueTask @this) =>
    await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ToTask<TOut>(
      this ValueTask<TOut> @this) =>
    await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<TOut> ToValueTask<TOut>(
      this TOut @this) =>
    ValueTask.FromResult(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task ToValueTask(
      this Task @this) =>
    await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ToValueTask<TOut>(
      this Task<TOut> @this) =>
    await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(
      this ValueTask<T> @this)
  {
    yield return await @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> ToAsyncEnumerableNullable<T>(
      this Task<T?> @this)
  {
    if (await @this is T awaited)
    {
      yield return awaited;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<T> ToAsyncEnumerableNullable<T>(
      this ValueTask<T?> @this)
  {
    if (await @this is T awaited)
    {
      yield return awaited;
    }
  }
}
