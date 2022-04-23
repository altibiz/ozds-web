using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task ThenWith<TIn>(
      this Task<TIn?> @this,
      Action<TIn> action) =>
    (await @this).With(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask ThenWith<TIn>(
      this ValueTask<TIn?> @this,
      Action<TIn> action) =>
    (await @this).With(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task ThenWithTask<TIn>(
      this Task<TIn?> @this,
      Func<TIn, Task> action) =>
    await (await @this).WithTask(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask ThenWithTask<TIn>(
      this ValueTask<TIn?> @this,
      Func<TIn, Task> action) =>
    await (await @this).WithTask(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task ThenWithValueTask<TIn>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask> action) =>
    await (await @this).WithValueTask(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask ThenWithValueTask<TIn>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask> action) =>
    await (await @this).WithValueTask(@this => action(@this));
}
