using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Disposables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task UsingAsync<TDisposable>(
      this TDisposable @this,
      Action<TDisposable> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task UsingAsyncTask<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, Task> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      await @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task UsingAsyncValueTask<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, ValueTask> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      await @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> UsingAsync<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, TOut> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      return @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> UsingAsyncTask<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, Task<TOut>> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      return await @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> UsingAsyncValueTask<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, ValueTask<TOut>> @do) where TDisposable : IAsyncDisposable
  {
    await using (@this)
    {
      return await @do(@this);
    }
  }
}
