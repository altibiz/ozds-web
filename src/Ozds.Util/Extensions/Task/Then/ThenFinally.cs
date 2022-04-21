namespace Ozds.Util;

public static partial class TaskExtensions
{
  public static async Task<TOut?> ThenFinally<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, TOut> selector) =>
    (await @this).FinallyWhen(@this => selector(@this));

  public static async ValueTask<TOut?> ThenFinally<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, TOut> selector) =>
    (await @this).FinallyWhen(@this => selector(@this));

  public static async Task<TOut?> ThenFinally<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, Task<TOut>> selector) =>
    await (await @this).FinallyWhen(@this => selector(@this));

  public static async Task<TOut?> ThenFinally<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, Task<TOut>> selector) =>
    await (await @this).FinallyWhen(@this => selector(@this));

  public static async Task<TOut?> ThenFinally<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await (await @this).FinallyWhen(@this => selector(@this));

  public static async ValueTask<TOut?> ThenFinally<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await (await @this).FinallyWhen(@this => selector(@this));
}
