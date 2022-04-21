namespace Ozds.Util;

public static partial class TaskExtensions
{
  public static async Task<TOut> Then<TIn, TOut>(
      this Task<TIn> @this,
      Func<TIn, TOut> selector) =>
    selector(await @this);

  public static async ValueTask<TOut> Then<TIn, TOut>(
      this ValueTask<TIn> @this,
      Func<TIn, TOut> selector) =>
    selector(await @this);

  public static async Task<TOut> Then<TIn, TOut>(
      this Task<TIn> @this,
      Func<TIn, Task<TOut>> selector) =>
    await selector(await @this);

  public static async Task<TOut> Then<TIn, TOut>(
      this ValueTask<TIn> @this,
      Func<TIn, Task<TOut>> selector) =>
    await selector(await @this);

  public static async Task<TOut> Then<TIn, TOut>(
      this Task<TIn> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await selector(await @this);

  public static async ValueTask<TOut> Then<TIn, TOut>(
      this ValueTask<TIn> @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await selector(await @this);
}
