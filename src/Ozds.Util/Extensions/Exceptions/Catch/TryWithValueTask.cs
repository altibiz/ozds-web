using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Catch<TException>
    where TException : Exception
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action) =>
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
