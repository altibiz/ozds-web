namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static void Named<TIn>(
      this TIn @this, Action<TIn> selector) => selector(@this);

  public static Task Named<TIn>(
      this TIn @this, Func<TIn, Task> selector) => selector(@this);

  public static ValueTask Named<TIn>(
      this TIn @this, Func<TIn, ValueTask> selector) => selector(@this);

  public static TOut Named<TIn, TOut>(
      this TIn @this, Func<TIn, TOut> selector) => selector(@this);

  public static Task<TOut> Named<TIn, TOut>(
      this TIn @this, Func<TIn, Task<TOut>> selector) => selector(@this);

  public static ValueTask<TOut> Named<TIn, TOut>(
      this TIn @this, Func<TIn, ValueTask<TOut>> selector) => selector(@this);
}
