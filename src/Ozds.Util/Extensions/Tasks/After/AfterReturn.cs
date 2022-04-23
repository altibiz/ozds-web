using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> AfterReturn<TOut>(
      this Task @this,
      Func<TOut> @do)
  {
    await @this;
    return @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> AfterReturn<TOut>(
      this ValueTask @this,
      Func<TOut> @do)
  {
    await @this;
    return @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> AfterReturnTask<TOut>(
      this Task @this,
      Func<Task<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> AfterReturnTask<TOut>(
      this ValueTask @this,
      Func<Task<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> AfterReturnValueTask<TOut>(
      this Task @this,
      Func<ValueTask<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> AfterReturnValueTask<TOut>(
      this ValueTask @this,
      Func<ValueTask<TOut>> @do)
  {
    await @this;
    return await @do();
  }
}
