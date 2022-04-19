namespace Ozds.Modules.Members;

public static partial class IEnumerableExtensions
{
  public static IEnumerable<TValue> ForEach<TValue>(
      this IEnumerable<TValue> @this, Action<TValue> @do)
  {
    foreach (var value in @this)
    {
      @do(value);
      yield return value;
    }
  }

  public static async IAsyncEnumerable<TValue> ForEach<TValue>(
      this IEnumerable<TValue> @this, Func<TValue, Task> @do)
  {
    foreach (var value in @this)
    {
      await @do(value);
      yield return value;
    }
  }

  public static async IAsyncEnumerable<TValue> ForEach<TValue>(
      this IEnumerable<TValue> @this, Func<TValue, ValueTask> @do)
  {
    foreach (var value in @this)
    {
      await @do(value);
      yield return value;
    }
  }

  public static async IAsyncEnumerable<TValue> ForEach<TValue>(
      this IAsyncEnumerable<TValue> @this, Action<TValue> @do)
  {
    await foreach (var value in @this)
    {
      @do(value);
      yield return value;
    }
  }

  public static async IAsyncEnumerable<TValue> ForEach<TValue>(
      this IAsyncEnumerable<TValue> @this, Func<TValue, Task> @do)
  {
    await foreach (var value in @this)
    {
      await @do(value);
      yield return value;
    }
  }

  public static async IAsyncEnumerable<TValue> ForEach<TValue>(
      this IAsyncEnumerable<TValue> @this, Func<TValue, ValueTask> @do)
  {
    await foreach (var value in @this)
    {
      await @do(value);
      yield return value;
    }
  }

  public static async Task<IEnumerable<TValue>> ForEachAwait<TValue>(
      this IEnumerable<TValue> @this, Func<TValue, Task> @do)
  {
    foreach (var value in @this)
    {
      await @do(value);
    }

    return @this;
  }

  public static async Task<IEnumerable<TValue>> ForEachAwait<TValue>(
      this IEnumerable<TValue> @this, Func<TValue, ValueTask> @do)
  {
    foreach (var value in @this)
    {
      await @do(value);
    }

    return @this;
  }

  public static async Task<IEnumerable<TValue>> ForEachAwait<TValue>(
      this IAsyncEnumerable<TValue> @this, Action<TValue> @do)
  {
    await foreach (var value in @this)
    {
      @do(value);
    }

    return await @this.Await();
  }

  public static async Task<IEnumerable<TValue>> ForEachAwait<TValue>(
      this IAsyncEnumerable<TValue> @this, Func<TValue, Task> @do)
  {
    await foreach (var value in @this)
    {
      await @do(value);
    }

    return await @this.Await();
  }

  public static async Task<IEnumerable<TValue>> ForEachAwait<TValue>(
      this IAsyncEnumerable<TValue> @this, Func<TValue, ValueTask> @do)
  {
    await foreach (var value in @this)
    {
      await @do(value);
    }

    return await @this.Await();
  }
}
