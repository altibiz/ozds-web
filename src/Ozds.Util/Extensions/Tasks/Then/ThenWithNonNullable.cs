using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TIn?> ThenWithNonNullable<TIn>(
      this Task<TIn?> @this,
      Action<TIn> action) =>
    (await @this).With(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TIn?> ThenWithNonNullable<TIn>(
      this ValueTask<TIn?> @this,
      Action<TIn> action) =>
    (await @this).With(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TIn?> ThenWithNonNullableTask<TIn>(
      this Task<TIn?> @this,
      Func<TIn, Task> action) =>
    await (await @this).WithTask(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TIn?> ThenWithNonNullableTask<TIn>(
      this ValueTask<TIn?> @this,
      Func<TIn, Task> action) =>
    await (await @this).WithTask(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TIn?> ThenWithNonNullableValueTask<TIn>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask> action) =>
    await (await @this).WithValueTask(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TIn?> ThenWithNonNullableValueTask<TIn>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask> action) =>
    await (await @this).WithValueTask(@this => action(@this));
}
