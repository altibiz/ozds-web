namespace Ozds.Util;

public static partial class Finally
{
  public static Action<TIn> Try<TIn>(Action<TIn> action, Action @finally) => (
      TIn @in) =>
  {
    try
    {
      action(@in);
    }
    finally
    {
      @finally();
    }
  };

  public static Func<TIn, ValueTask> Try<TIn>(
      Action<TIn> action, Func<Task> @finally) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        finally
        {
          await @finally();
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Action<TIn> action, Func<ValueTask> @finally) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        finally
        {
          await @finally();
        }
      };

  public static Action<TIn> Try<TIn>(
      Action<TIn> action, Action<TIn> @finally) => (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        finally
        {
          @finally(@in);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Action<TIn> action, Func<TIn, Task> @finally) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        finally
        {
          await @finally(@in);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Action<TIn> action, Func<TIn, ValueTask> @finally) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        finally
        {
          await @finally(@in);
        }
      };
}
