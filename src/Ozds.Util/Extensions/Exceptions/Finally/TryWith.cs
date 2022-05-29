using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Finally
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn> TryWith<TIn>(
      Action<TIn> action,
      Action @finally) =>
    (TIn @in) =>
    {
      try
      {
        action(@in);
      }
      finally
      {
        @finally();
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<Task> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        action(@in);
      }
      finally
      {
        await @finally();
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<ValueTask> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        action(@in);
      }
      finally
      {
        await @finally();
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn> TryWith<TIn>(
      Action<TIn> action,
      Action<TIn> @finally) =>
    (TIn @in) =>
    {
      try
      {
        action(@in);
      }
      finally
      {
        @finally(@in);
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<TIn, Task> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        action(@in);
      }
      finally
      {
        await @finally(@in);
      }
    };

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn, ValueTask> TryWith<TIn>(
      Action<TIn> action,
      Func<TIn, ValueTask> @finally) =>
    async (TIn @in) =>
    {
      try
      {
        action(@in);
      }
      finally
      {
        await @finally(@in);
      }
    };
}
