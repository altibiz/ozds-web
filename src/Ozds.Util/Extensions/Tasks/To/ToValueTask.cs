using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<TOut> ToValueTask<TOut>(
      this TOut @this) =>
    ValueTask.FromResult(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask ToValueTask(
      this Task @this) =>
    await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> ToValueTask<TOut>(
      this Task<TOut> @this) =>
    await @this;
}
