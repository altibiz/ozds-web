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
}
