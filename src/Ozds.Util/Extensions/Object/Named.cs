namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static TIn Named<TIn>(
      this TIn @this, Action<TIn> selector)
  {
    selector(@this);
    return @this;
  }

  public static async Task<TIn> Named<TIn>(
      this TIn @this, Func<TIn, Task> selector)
  {
    await selector(@this);
    return @this;
  }

  public static async ValueTask<TIn> Named<TIn>(
      this TIn @this, Func<TIn, ValueTask> selector)
  {
    await selector(@this);
    return @this;
  }

  public static TOut Named<TIn, TOut>(
      this TIn @this, Func<TIn, TOut> selector) => selector(@this);

  public static Task<TOut> Named<TIn, TOut>(
      this TIn @this, Func<TIn, Task<TOut>> selector) => selector(@this);

  public static ValueTask<TOut> Named<TIn, TOut>(
      this TIn @this, Func<TIn, ValueTask<TOut>> selector) => selector(@this);
}
