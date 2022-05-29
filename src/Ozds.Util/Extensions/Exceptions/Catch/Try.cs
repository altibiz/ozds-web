using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Catch<TException>
    where TException : Exception
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, TOut?> Try<TIn, TOut>(
      Func<TIn, TOut> selector) =>
    (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      TOut @catch) =>
    (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TOut> @catch) =>
    (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<Task<TOut>> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<ValueTask<TOut>> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, TOut> @catch) =>
    (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, Task<TOut>> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, ValueTask<TOut>> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TException, TOut> @catch) =>
    (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TException, Task<TOut>> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TException, ValueTask<TOut>> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, TOut> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, TException, TOut> @catch) =>
    (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, TException, Task<TOut>> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask<TOut>> Try<TIn, TOut>(
      Func<TIn, TOut> selector,
      Func<TIn, TException, ValueTask<TOut>> @catch) =>
    async (TIn @in) =>
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
