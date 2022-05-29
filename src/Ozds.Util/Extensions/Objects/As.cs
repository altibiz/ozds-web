using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? As<T>(
      this object? @this) where T : class =>
    @this as T ?? default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T As<T>(
      this object? @this,
      T @default) where T : class =>
    @this as T ?? @default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T As<T>(
      this object? @this,
      Func<T> @default) where T : class =>
    @this as T ?? @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> AsTask<T>(
      this object? @this,
      Func<Task<T>> @default) where T : class =>
    @this as T ?? await @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> AsValueTask<T>(
      this object? @this,
      Func<ValueTask<T>> @default) where T : class =>
    @this as T ?? await @default();
}
