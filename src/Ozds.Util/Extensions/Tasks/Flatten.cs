using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task Flatten(
      this Task<Task> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task Flatten(
      this Task<ValueTask> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task Flatten(
      this ValueTask<Task> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task Flatten(
      this ValueTask<ValueTask> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> Flatten<T>(
      this Task<Task<T>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> Flatten<T>(
      this Task<ValueTask<T>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> Flatten<T>(
      this ValueTask<Task<T>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> Flatten<T>(
      this ValueTask<ValueTask<T>> @this) =>
    await await @this;
}
