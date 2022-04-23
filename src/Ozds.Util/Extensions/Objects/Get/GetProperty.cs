using System.Runtime.CompilerServices;
using System.Reflection;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? GetProperty<T>(
      this object? @this,
      PropertyInfo field) where T : class =>
    field
      .GetValue(@this)
      .As<T>() ?? default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetProperty<T>(
      this object? @this,
      PropertyInfo field,
      T @default) where T : class =>
    field
      .GetValue(@this)
      .As<T>(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetProperty<T>(
      this object? @this,
      PropertyInfo field,
      Func<T> @default) where T : class =>
    field
      .GetValue(@this)
      .As<T>(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<T> GetProperty<T>(
      this object? @this,
      PropertyInfo field,
      Func<Task<T>> @default) where T : class =>
    field
      .GetValue(@this)
      .AsTask<T>(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<T> GetProperty<T>(
      this object? @this,
      PropertyInfo field,
      Func<ValueTask<T>> @default) where T : class =>
    field
      .GetValue(@this)
      .AsValueTask<T>(@default);
}
