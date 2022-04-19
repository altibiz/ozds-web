namespace Ozds.Util;

public static partial class TaskExtensions
{
  public static async Task Then(
      this Task @this,
      Action @do)
  {
    await @this;
    @do();
  }

  public static async ValueTask Then(
      this ValueTask @this,
      Action @do)
  {
    await @this;
    @do();
  }

  public static async Task Then(
      this Task @this,
      Func<Task> @do)
  {
    await @this;
    await @do();
  }

  public static async ValueTask Then(
      this ValueTask @this,
      Func<Task> @do)
  {
    await @this;
    await @do();
  }

  public static async Task Then(
      this Task @this,
      Func<ValueTask> @do)
  {
    await @this;
    await @do();
  }

  public static async ValueTask Then(
      this ValueTask @this,
      Func<ValueTask> @do)
  {
    await @this;
    await @do();
  }

  public static async Task<TOut> Then<TOut>(
      this Task @this,
      Func<TOut> @do)
  {
    await @this;
    return @do();
  }

  public static async ValueTask<TOut> Then<TOut>(
      this ValueTask @this,
      Func<TOut> @do)
  {
    await @this;
    return @do();
  }

  public static async Task<TOut> Then<TOut>(
      this Task @this,
      Func<ValueTask<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  public static async ValueTask<TOut> Then<TOut>(
      this ValueTask @this,
      Func<ValueTask<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  public static async Task<TOut> Then<TOut>(
      this Task @this,
      Func<Task<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  public static async ValueTask<TOut> Then<TOut>(
      this ValueTask @this,
      Func<Task<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  public static async Task Then<TIn>(
      this Task<TIn> @this,
      Action<TIn> selector) =>
    selector(await @this);

  public static async ValueTask Then<TIn>(
      this ValueTask<TIn> @this,
      Action<TIn> selector) =>
    selector(await @this);

  public static async Task Then<TIn>(
      this Task<TIn> @this,
      Func<TIn, Task> selector) =>
    await selector(await @this);

  public static async ValueTask Then<TIn>(
      this ValueTask<TIn> @this,
      Func<TIn, Task> selector) =>
    await selector(await @this);

  public static async Task Then<TIn>(
      this Task<TIn> @this,
      Func<TIn, ValueTask> selector) =>
    await selector(await @this);

  public static async ValueTask Then<TIn>(
      this ValueTask<TIn> @this,
      Func<TIn, ValueTask> selector) =>
    await selector(await @this);

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
