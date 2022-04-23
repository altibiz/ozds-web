using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Finally
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Action @finally) =>
    (TIn @in) =>
    {
      try
      {
        return selector(@in);
      }
      finally
      {
        @finally();
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<Task> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        return selector(@in);
      }
      finally
      {
        await @finally();
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<ValueTask> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        return selector(@in);
      }
      finally
      {
        await @finally();
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Action<TIn> @finally) =>
    (TIn @in) =>
    {
      try
      {
        return selector(@in);
      }
      finally
      {
        @finally(@in);
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, Task> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        return selector(@in);
      }
      finally
      {
        await @finally(@in);
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, ValueTask> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        return selector(@in);
      }
      finally
      {
        await @finally(@in);
      }
    };
}
