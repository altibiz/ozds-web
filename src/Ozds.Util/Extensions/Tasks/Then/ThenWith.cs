using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TIn> ThenWith<TIn>(
      this Task<TIn> @this,
      Action<TIn> action) =>
    (await @this).WithNullable(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TIn> ThenWith<TIn>(
      this ValueTask<TIn> @this,
      Action<TIn> action) =>
    (await @this).WithNullable(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TIn> ThenWithTask<TIn>(
      this Task<TIn> @this,
      Func<TIn, Task> action) =>
    await (await @this).WithNullable(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TIn> ThenWithTask<TIn>(
      this ValueTask<TIn> @this,
      Func<TIn, Task> action) =>
    await (await @this).WithNullable(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TIn> ThenWithValueTask<TIn>(
      this Task<TIn> @this,
      Func<TIn, ValueTask> action) =>
    await (await @this).WithNullable(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TIn> ThenWithValueTask<TIn>(
      this ValueTask<TIn> @this,
      Func<TIn, ValueTask> action) =>
    await (await @this).WithNullable(@this => action(@this));
}
