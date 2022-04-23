using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task FlattenTask(
      this Task<Task> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task FlattenTask(
      this Task<ValueTask> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task FlattenTask(
      this ValueTask<Task> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task FlattenTask(
      this ValueTask<ValueTask> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> FlattenTask<TOut>(
      this Task<Task<TOut>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> FlattenTask<TOut>(
      this Task<ValueTask<TOut>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> FlattenTask<TOut>(
      this ValueTask<Task<TOut>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> FlattenTask<TOut>(
      this ValueTask<ValueTask<TOut>> @this) =>
    await await @this;
}
