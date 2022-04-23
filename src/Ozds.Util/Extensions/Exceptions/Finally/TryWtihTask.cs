using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Finally
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWtihTask<TIn>(
      Func<TIn, Task> action,
      Action @finally) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWtihTask<TIn>(
      Func<TIn, Task> action,
      Func<Task> @finally) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWtihTask<TIn>(
      Func<TIn, Task> action,
      Func<ValueTask> @finally) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWtihTask<TIn>(
      Func<TIn, Task> action,
      Action<TIn> @finally) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWtihTask<TIn>(
      Func<TIn, Task> action,
      Func<TIn, Task> @finally) =>
    async (TIn @in) =>
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

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, Task> TryWtihTask<TIn>(
      Func<TIn, Task> action,
      Func<TIn, ValueTask> @finally) =>
    async (TIn @in) =>
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
