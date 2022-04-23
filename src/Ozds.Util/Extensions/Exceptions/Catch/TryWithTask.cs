using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Catch<TException>
    where TException : Exception
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(Func<TIn, Task> action) =>
    async (TIn @in) =>
    {
      try
      {
        await action(@in);
      }
      catch (TException)
      {
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(Func<TIn, Task> action,
      Action @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(
      Func<TIn, Task> action,
      Func<Task> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(
      Func<TIn, Task> action,
      Func<ValueTask> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(
      Func<TIn, Task> action,
      Action<TIn> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(
      Func<TIn, Task> action,
      Func<TIn, Task> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(Func<TIn, Task> action,
      Func<TIn, ValueTask> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(
      Func<TIn, Task> action,
      Action<TException> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(Func<TIn, Task> action,
      Func<TException, Task> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(Func<TIn, Task> action,
      Func<TException, ValueTask> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(Func<TIn, Task> action,
      Action<TIn, TException> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(Func<TIn, Task> action,
      Func<TIn, TException, Task> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWithTask<TIn>(Func<TIn, Task> action,
      Func<TIn, TException, ValueTask> @catch) =>
    async (TIn @in) =>
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
