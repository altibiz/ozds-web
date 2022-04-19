namespace Ozds.Modules.Members;

public static partial class Catch<TException>
    where TException : Exception
{
  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action) => async (TIn @in) =>
  {
    try
    {
      await action(@in);
    }
    catch (TException)
    {
    }
  };

  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action, Action @catch) => async (
      TIn @in) =>
  {
    try
    {
      await action(@in);
    }
    catch (TException)
    {
      @catch();
    }
  };

  public static Func<TIn, ValueTask> Try<TIn>(
      Func<TIn, ValueTask> action, Func<Task> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException)
        {
          await @catch();
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Func<TIn, ValueTask> action, Func<ValueTask> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException)
        {
          await @catch();
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Func<TIn, ValueTask> action, Action<TIn> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException)
        {
          @catch(@in);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Func<TIn, ValueTask> action, Func<TIn, Task> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException)
        {
          await @catch(@in);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action,
      Func<TIn, ValueTask> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException)
        {
          await @catch(@in);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Func<TIn, ValueTask> action, Action<TException> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException exception)
        {
          @catch(exception);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action,
      Func<TException, Task> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException exception)
        {
          await @catch(exception);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action,
      Func<TException, ValueTask> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException exception)
        {
          await @catch(exception);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action,
      Action<TIn, TException> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException exception)
        {
          @catch(@in, exception);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action,
      Func<TIn, TException, Task> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException exception)
        {
          await @catch(@in, exception);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Func<TIn, ValueTask> action,
      Func<TIn, TException, ValueTask> @catch) => async (TIn @in) =>
      {
        try
        {
          await action(@in);
        }
        catch (TException exception)
        {
          await @catch(@in, exception);
        }
      };
}
