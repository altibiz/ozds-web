using System.Runtime.CompilerServices;

namespace Ozds.Extensions.Disposable;

public static partial class Disposables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Using<TDisposable>(
      this TDisposable @this,
      Action<TDisposable> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask UsingAwait<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, ValueTask> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      await @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut Using<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, TOut> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> UsingAwait<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, ValueTask<TOut>> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return await @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task UsingAsync<TDisposable>(
      this TDisposable @this,
      Action<TDisposable> @do)
        where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task UsingAwaitAsync<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, ValueTask> @do)
        where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      await @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> UsingAsync<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, TOut> @do)
        where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      return @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> UsingAwaitAsync<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, ValueTask<TOut>> @do)
        where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      return await @do(@this);
    }
  }
}
