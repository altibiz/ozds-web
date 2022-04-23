namespace Ozds.Util;

public static partial class Objects
{
  public static TOut Return<TIn, TOut>(
      this TIn? @this,
      TOut result) =>
    result;

  public static TOut Return<TIn, TOut>(
      this TIn? @this,
      Func<TOut> result) =>
    result();

  public static Task<TOut> ReturnTask<TIn, TOut>(
      this TIn? @this,
      Func<Task<TOut>> result) =>
    result();

  public static ValueTask<TOut> ReturnValueTask<TIn, TOut>(
      this TIn? @this,
      Func<ValueTask<TOut>> result) =>
    result();
}
