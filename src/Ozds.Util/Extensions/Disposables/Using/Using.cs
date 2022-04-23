using System.Runtime.CompilerServices;

namespace Ozds.Util;

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
  public static Task UsingTask<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, Task> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask UsingValueTask<TDisposable>(
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
  public static Task<TOut> UsingTask<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, Task<TOut>> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<TOut> UsingValueTask<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, ValueTask<TOut>> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }
}
