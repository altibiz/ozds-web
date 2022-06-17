using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenWith<T>(
      this Task<T> @this,
      Action<T> action) =>
    (await @this).With(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenWith<T>(
      this ValueTask<T> @this,
      Action<T> action) =>
    (await @this).With(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThenWithAwait<T>(
      this Task<T> @this,
      Func<T, ValueTask> action) =>
    await (await @this).WithAsync(@this => action(@this));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> ThenWithAwait<T>(
      this ValueTask<T> @this,
      Func<T, ValueTask> action) =>
    await (await @this).WithAsync(@this => action(@this));
}
