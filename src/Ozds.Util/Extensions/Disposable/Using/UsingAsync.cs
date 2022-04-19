namespace Ozds.Util;

public static partial class DisposableExtensions
{
  public static async Task UsingAsync<TDisposable>(
      this TDisposable @this,
      Action<TDisposable> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      @do(@this);
    }
  }

  public static async Task UsingAsync<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, Task> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      await @do(@this);
    }
  }

  public static async Task UsingAsync<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, ValueTask> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      await @do(@this);
    }
  }

  public static async Task<TOut> UsingAsync<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, TOut> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      return @do(@this);
    }
  }

  public static async Task<TOut> UsingAsync<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, Task<TOut>> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      return await @do(@this);
    }
  }

  public static async Task<TOut> UsingAsync<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, ValueTask<TOut>> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      return await @do(@this);
    }
  }
}
