using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> ThenWithNonNull<T>(
      this Task<T?> @this,
      Action<T> action) =>
    (await @this).WithNonNull(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> ThenWithNonNull<T>(
      this ValueTask<T?> @this,
      Action<T> action) =>
    (await @this).WithNonNull(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> ThenWithNonNullAwait<T>(
      this Task<T?> @this,
      Func<T, ValueTask> action) =>
    await (await @this).WithNonNullAsync(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> ThenWithNonNullAwait<T>(
      this ValueTask<T?> @this,
      Func<T, ValueTask> action) =>
    await (await @this).WithNonNullAsync(@this => action(@this));
}
