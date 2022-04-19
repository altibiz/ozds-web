namespace Ozds.Modules.Members;

public static partial class Catch<TException>
    where TException : Exception
{
  public static Action<TIn> Try<TIn>(Action<TIn> action) => (TIn @in) =>
  {
    try
    {
      action(@in);
    }
    catch (TException)
    {
    }
  };

  public static Action<TIn> Try<TIn>(Action<TIn> action, Action @catch) => (
      TIn @in) =>
  {
    try
    {
      action(@in);
    }
    catch (TException)
    {
      @catch();
    }
  };

  public static Func<TIn, ValueTask> Try<TIn>(
      Action<TIn> action, Func<Task> @catch) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException)
        {
          await @catch();
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Action<TIn> action, Func<ValueTask> @catch) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException)
        {
          await @catch();
        }
      };

  public static Action<TIn> Try<TIn>(
      Action<TIn> action, Action<TIn> @catch) => (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException)
        {
          @catch(@in);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(
      Action<TIn> action, Func<TIn, Task> @catch) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException)
        {
          await @catch(@in);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Action<TIn> action,
      Func<TIn, ValueTask> @catch) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException)
        {
          await @catch(@in);
        }
      };

  public static Action<TIn> Try<TIn>(
      Action<TIn> action, Action<TException> @catch) => (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException exception)
        {
          @catch(exception);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Action<TIn> action,
      Func<TException, Task> @catch) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException exception)
        {
          await @catch(exception);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Action<TIn> action,
      Func<TException, ValueTask> @catch) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException exception)
        {
          await @catch(exception);
        }
      };

  public static Action<TIn> Try<TIn>(Action<TIn> action,
      Action<TIn, TException> @catch) => (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException exception)
        {
          @catch(@in, exception);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Action<TIn> action,
      Func<TIn, TException, Task> @catch) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException exception)
        {
          await @catch(@in, exception);
        }
      };

  public static Func<TIn, ValueTask> Try<TIn>(Action<TIn> action,
      Func<TIn, TException, ValueTask> @catch) => async (TIn @in) =>
      {
        try
        {
          action(@in);
        }
        catch (TException exception)
        {
          await @catch(@in, exception);
        }
      };
}
