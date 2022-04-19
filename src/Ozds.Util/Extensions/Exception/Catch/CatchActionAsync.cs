namespace Ozds.Util;

public static partial class Catch<TException>
    where TException : Exception
{
  public static Func<TIn, Task> Try<TIn>(Func<TIn, Task> action) => async (TIn @in) =>
  {
    try
    {
      await action(@in);
    }
    catch (TException)
    {
    }
  };

  public static Func<TIn, Task> Try<TIn>(Func<TIn, Task> action, Action @catch) => async (
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

  public static Func<TIn, Task> Try<TIn>(
      Func<TIn, Task> action, Func<Task> @catch) => async (TIn @in) =>
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

  public static Func<TIn, Task> Try<TIn>(
      Func<TIn, Task> action, Func<ValueTask> @catch) => async (TIn @in) =>
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

  public static Func<TIn, Task> Try<TIn>(
      Func<TIn, Task> action, Action<TIn> @catch) => async (TIn @in) =>
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

  public static Func<TIn, Task> Try<TIn>(
      Func<TIn, Task> action, Func<TIn, Task> @catch) => async (TIn @in) =>
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

  public static Func<TIn, Task> Try<TIn>(Func<TIn, Task> action,
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

  public static Func<TIn, Task> Try<TIn>(
      Func<TIn, Task> action, Action<TException> @catch) => async (TIn @in) =>
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

  public static Func<TIn, Task> Try<TIn>(Func<TIn, Task> action,
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

  public static Func<TIn, Task> Try<TIn>(Func<TIn, Task> action,
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

  public static Func<TIn, Task> Try<TIn>(Func<TIn, Task> action,
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

  public static Func<TIn, Task> Try<TIn>(Func<TIn, Task> action,
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

  public static Func<TIn, Task> Try<TIn>(Func<TIn, Task> action,
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
