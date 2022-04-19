namespace Ozds.Modules.Members;

public static partial class DisposableExtensions
{
  public static void Using<TDisposable>(
      this TDisposable @this,
      Action<TDisposable> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      @do(@this);
    }
  }

  public static Task Using<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, Task> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }

  public static ValueTask Using<TDisposable>(
      this TDisposable @this,
      Func<TDisposable, ValueTask> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }

  public static TOut Using<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, TOut> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }

  public static Task<TOut> Using<TDisposable, TOut>(
      this TDisposable @this,
      Func<TDisposable, Task<TOut>> @do) where TDisposable : IDisposable
  {
    using (@this)
    {
      return @do(@this);
    }
  }

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
