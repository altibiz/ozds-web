using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Catch<TException>
    where TException : Exception
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn> TryWith<TIn>(
      Action<TIn> action) =>
    (TIn @in) =>
    {
      try
      {
        action(@in);
      }
      catch (TException)
      {
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn> TryWith<TIn>(
      Action<TIn> action,
      Action @catch) =>
    (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<Task> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<ValueTask> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn> TryWith<TIn>(
      Action<TIn> action,
      Action<TIn> @catch) =>
    (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<TIn, Task> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<TIn, ValueTask> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn> TryWith<TIn>(
      Action<TIn> action,
      Action<TException> @catch) =>
    (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<TException, Task> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<TException, ValueTask> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn> TryWith<TIn>(
      Action<TIn> action,
      Action<TIn, TException> @catch) =>
    (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<TIn, TException, Task> @catch) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<TIn, TException, ValueTask> @catch) =>
    async (TIn @in) =>
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
