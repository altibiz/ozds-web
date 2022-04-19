namespace Ozds.Util;

public static partial class Finally
{
  public static Func<TIn, Task<TOut>> Try<TIn, TOut>(
      Func<TIn, Task<TOut>> selector,
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

  public static Func<TIn, Task<TOut>> Try<TIn, TOut>(
      Func<TIn, Task<TOut>> selector,
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

  public static Func<TIn, Task<TOut>> Try<TIn, TOut>(
      Func<TIn, Task<TOut>> selector,
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

  public static Func<TIn, Task<TOut>> Try<TIn, TOut>(
      Func<TIn, Task<TOut>> selector,
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

  public static Func<TIn, Task<TOut>> Try<TIn, TOut>(
      Func<TIn, Task<TOut>> selector,
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

  public static Func<TIn, Task<TOut>> Try<TIn, TOut>(
      Func<TIn, Task<TOut>> selector,
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
