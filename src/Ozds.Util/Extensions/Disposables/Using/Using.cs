using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

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
  public static Task Using<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, Task> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask Using<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, ValueTask> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
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
  public static Task<TOut> Using<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, Task<TOut>> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<TOut> Using<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, ValueTask<TOut>> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }
}
