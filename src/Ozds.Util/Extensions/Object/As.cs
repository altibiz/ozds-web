namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static TOut? As<TOut>(
      this object? @this) where TOut : class =>
    @this as TOut ?? default;

  public static TOut As<TOut>(
      this object? @this,
      TOut @default) where TOut : class =>
    @this as TOut ?? @default;

  public static TOut As<TOut>(
      this object? @this,
      Func<TOut> @default) where TOut : class =>
    @this as TOut ?? @default();

  public static async ValueTask<TOut> As<TOut>(
      this object? @this,
      Func<Task<TOut>> @default) where TOut : class =>
    @this as TOut ?? await @default();

  public static async ValueTask<TOut> As<TOut>(
      this object? @this,
      Func<ValueTask<TOut>> @default) where TOut : class =>
    @this as TOut ?? await @default();
}
