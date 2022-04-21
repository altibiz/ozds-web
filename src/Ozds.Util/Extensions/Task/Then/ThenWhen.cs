namespace Ozds.Util;

public static partial class TaskExtensions
{
  public static async Task<TOut?> ThenWhen<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, TOut?> selector) =>
    (await @this).When(@this => selector(@this));

  public static async ValueTask<TOut?> ThenWhen<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, TOut?> selector) =>
    (await @this).When(@this => selector(@this));

  public static async Task<TOut?> ThenWhen<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, Task<TOut?>> selector) =>
    await (await @this).When(@this => selector(@this));

  public static async Task<TOut?> ThenWhen<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, Task<TOut?>> selector) =>
    await (await @this).When(@this => selector(@this));

  public static async Task<TOut?> ThenWhen<TIn, TOut>(
      this Task<TIn?> @this,
      Func<TIn, ValueTask<TOut?>> selector) =>
    await (await @this).When(@this => selector(@this));

  public static async ValueTask<TOut?> ThenWhen<TIn, TOut>(
      this ValueTask<TIn?> @this,
      Func<TIn, ValueTask<TOut?>> selector) =>
    await (await @this).When(@this => selector(@this));
}
