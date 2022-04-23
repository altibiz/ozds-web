using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Finally
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> TryValueTask<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Func<TOut> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        return await selector(@in);
      }
      finally
      {
        @finally();
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> TryValueTask<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Func<Task> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        return await selector(@in);
      }
      finally
      {
        await @finally();
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> TryValueTask<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Func<ValueTask> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        return await selector(@in);
      }
      finally
      {
        await @finally();
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> TryValueTask<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Action<TIn> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        return await selector(@in);
      }
      finally
      {
        @finally(@in);
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> TryValueTask<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Func<TIn, Task> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        return await selector(@in);
      }
      finally
      {
        await @finally(@in);
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> TryValueTask<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Func<TIn, ValueTask> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        return await selector(@in);
      }
      finally
      {
        await @finally(@in);
      }
    };
}
