using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task After(
      this Task @this,
      Action @do)
  {
    await @this;
    @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask After(
      this ValueTask @this,
      Action @do)
  {
    await @this;
    @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task AfterTask(
      this Task @this,
      Func<Task> @do)
  {
    await @this;
    await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask AfterTask(
      this ValueTask @this,
      Func<Task> @do)
  {
    await @this;
    await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task AfterValueTask(
      this Task @this,
      Func<ValueTask> @do)
  {
    await @this;
    await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask AfterValueTask(
      this ValueTask @this,
      Func<ValueTask> @do)
  {
    await @this;
    await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> After<TOut>(
      this Task @this,
      Func<TOut> @do)
  {
    await @this;
    return @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> After<TOut>(
      this ValueTask @this,
      Func<TOut> @do)
  {
    await @this;
    return @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> AfterTask<TOut>(
      this Task @this,
      Func<Task<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> AfterTask<TOut>(
      this ValueTask @this,
      Func<Task<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> AfterValueTask<TOut>(
      this Task @this,
      Func<ValueTask<TOut>> @do)
  {
    await @this;
    return await @do();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> AfterValueTask<TOut>(
      this ValueTask @this,
      Func<ValueTask<TOut>> @do)
  {
    await @this;
    return await @do();
  }
}
