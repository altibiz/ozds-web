namespace Ozds.Modules.Members;

public static partial class Finally
{
  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector, Action @finally) => (TIn @in) =>
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

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector, Func<Task> @finally) => async (TIn @in) =>
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

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<ValueTask> @finally) => async (TIn @in) =>
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

  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector, Action<TIn> @finally) => (TIn @in) =>
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

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, Task> @finally) => async (TIn @in) =>
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

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, ValueTask> @finally) => async (TIn @in) =>
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
