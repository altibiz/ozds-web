namespace Ozds.Modules.Members;

public static class TaskExtensions
{
  public static async Task<TOut> Then<TIn, TOut>(
      this Task<TIn> @this, Func<TIn, TOut> selector) => selector(await @this);

  public static async Task<TOut> Then<TIn, TOut>(this ValueTask<TIn> @this,
      Func<TIn, TOut> selector) => selector(await @this);

  public static async Task<TOut> Then<TOut>(this Task @this, Func<TOut> @do)
  {
    await @this;
    return @do();
  }

  public static async ValueTask<TOut> Then<TOut>(
      this ValueTask @this, Func<TOut> @do)
  {
    await @this;
    return @do();
  }

  public static async Task<TOut> Flatten<TOut>(
      this Task<Task<TOut>> @this) => await await @this;

  public static async ValueTask<TOut> Flatten<TOut>(
      this ValueTask<ValueTask<TOut>> @this) => await await @this;

  public static Task<TOut> ToTask<TOut>(this TOut @this) => Task.FromResult(
      @this);

  public static ValueTask<TOut> ToValueTask<TOut>(
      this TOut @this) => ValueTask.FromResult(@this);
}
