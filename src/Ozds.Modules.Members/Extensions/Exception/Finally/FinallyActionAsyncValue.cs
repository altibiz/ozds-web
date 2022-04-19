namespace Ozds.Modules.Members;

public static partial class Finally
{
  public static Func<TIn, ValueTask> Try<TIn>(
      Func<TIn, ValueTask> action, Action @finally) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        finally
        {
          @finally();
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Func<TIn, ValueTask> action, Func<Task> @finally) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        finally
        {
          await @finally();
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action,
      Func<ValueTask> @finally) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        finally
        {
          await @finally();
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Func<TIn, ValueTask> action, Action<TIn> @finally) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        finally
        {
          @finally(@in);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action,
      Func<TIn, Task> @finally) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        finally
        {
          await @finally(@in);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action,
      Func<TIn, ValueTask> @finally) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        finally
        {
          await @finally(@in);
        }
      };
}
