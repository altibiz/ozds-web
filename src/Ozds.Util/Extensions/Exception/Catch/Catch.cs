namespace Ozds.Util;

public static partial class Catch<TException>
    where TException : Exception
{
  public static Func<TIn, TOut?> Try<TIn, TOut>(Func<TIn, TOut> selector) => (
      TIn @in) =>
  {
    try
    {
      return selector(@in);
    }
    catch (TException)
    {
      return default;
    }
  };

  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector, TOut @catch) => (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException)
        {
          return @catch;
        }
      };

  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector, Func<TOut> @catch) => (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException)
        {
          return @catch();
        }
      };

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<Task<TOut>> @catch) => async (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException)
        {
          return await @catch();
        }
      };

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<ValueTask<TOut>> @catch) => async (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException)
        {
          return await @catch();
        }
      };

  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector, Func<TIn, TOut> @catch) => (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException)
        {
          return @catch(@in);
        }
      };

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, Task<TOut>> @catch) => async (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException)
        {
          return await @catch(@in);
        }
      };

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, ValueTask<TOut>> @catch) => async (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException)
        {
          return await @catch(@in);
        }
      };

  public static Func<TIn, TOut> Try<TIn, TOut>(Func<TIn, TOut> selector,
      Func<TException, TOut> @catch) => (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException exception)
        {
          return @catch(exception);
        }
      };

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TException, Task<TOut>> @catch) => async (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException exception)
        {
          return await @catch(exception);
        }
      };

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TException, ValueTask<TOut>> @catch) => async (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException exception)
        {
          return await @catch(exception);
        }
      };

  public static Func<TIn, TOut> Try<TIn, TOut>(Func<TIn, TOut> selector,
      Func<TIn, TException, TOut> @catch) => (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException exception)
        {
          return @catch(@in, exception);
        }
      };

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, TException, Task<TOut>> @catch) => async (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException exception)
        {
          return await @catch(@in, exception);
        }
      };

  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, TException, ValueTask<TOut>> @catch) => async (TIn @in) =>
      {
        try
        {
          return selector(@in);
        }
        catch (TException exception)
        {
          return await @catch(@in, exception);
        }
      };
}
