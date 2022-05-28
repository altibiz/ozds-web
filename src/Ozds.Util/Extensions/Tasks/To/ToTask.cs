using System.Runtime.CompilerServices;

namespace Ozds.Util;

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
}
