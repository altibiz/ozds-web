using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Finally
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
  public static Func<TIn, ValueTask> TryWithValueTask<TIn>(
      Func<TIn, ValueTask> action,
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
