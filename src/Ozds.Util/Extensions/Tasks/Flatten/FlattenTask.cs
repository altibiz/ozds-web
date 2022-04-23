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
  public static async Task<T> FlattenTask<T>(
      this Task<Task<T>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> FlattenTask<T>(
      this Task<ValueTask<T>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> FlattenTask<T>(
      this ValueTask<Task<T>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> FlattenTask<T>(
      this ValueTask<ValueTask<T>> @this) =>
    await await @this;
}
