using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask FlattenValueTask(
      this Task<ValueTask> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask FlattenValueTask(
      this ValueTask<Task> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask FlattenValueTask(
      this ValueTask<ValueTask> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> FlattenValueTask<TOut>(
      this Task<Task<TOut>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> FlattenValueTask<TOut>(
      this Task<ValueTask<TOut>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> FlattenValueTask<TOut>(
      this ValueTask<Task<TOut>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TOut> FlattenValueTask<TOut>(
      this ValueTask<ValueTask<TOut>> @this) =>
    await await @this;
}
