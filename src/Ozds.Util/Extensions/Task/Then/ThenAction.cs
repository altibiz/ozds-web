namespace Ozds.Util;

public static partial class TaskExtensions
{
  public static async Task Then<TIn>(
      this Task<TIn?> @this,
      Action<TIn> selector) =>
    (await @this).When(@this => selector(@this));

  public static async ValueTask Then<TIn>(
      this ValueTask<TIn?> @this,
      Action<TIn> selector) =>
    (await @this).When(@this => selector(@this));

  public static async Task Then<TIn>(
      this Task<TIn?> @this,
      Func<TIn, Task> selector) =>
    await (await @this).When(@this => selector(@this));

  public static async ValueTask Then<TIn>(
      this ValueTask<TIn?> @this,
      Func<TIn, Task> selector) =>
    await (await @this).When(@this => selector(@this));

  public static async Task Then<TIn>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask> selector) =>
    await (await @this).When(@this => selector(@this));

  public static async ValueTask Then<TIn>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask> selector) =>
    await (await @this).When(@this => selector(@this));
}
