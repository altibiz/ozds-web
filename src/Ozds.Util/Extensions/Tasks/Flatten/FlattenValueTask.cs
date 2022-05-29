using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask FlattenValueTask(
      this Task<Task> @this) =>
    await await @this;

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
  public static async ValueTask<T> FlattenValueTask<T>(
      this Task<Task<T>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> FlattenValueTask<T>(
      this Task<ValueTask<T>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> FlattenValueTask<T>(
      this ValueTask<Task<T>> @this) =>
    await await @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> FlattenValueTask<T>(
      this ValueTask<ValueTask<T>> @this) =>
    await await @this;
}
