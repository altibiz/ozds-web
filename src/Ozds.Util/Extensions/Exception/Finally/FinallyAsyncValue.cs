namespace Ozds.Util;

public static partial class Finally
{
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Func<TOut> @finally) => async (TIn @in) =>
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

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Func<Task> @finally) => async (TIn @in) =>
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

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Func<ValueTask> @finally) => async (TIn @in) =>
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

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Action<TIn> @finally) => async (TIn @in) =>
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

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Func<TIn, Task> @finally) => async (TIn @in) =>
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

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, ValueTask<TOut>> selector,
      Func<TIn, ValueTask> @finally) => async (TIn @in) =>
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
