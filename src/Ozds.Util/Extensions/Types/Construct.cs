using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Types
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object? Construct(
      this Type? @this,
      params object?[]? args) =>
    @this == default ? default :
    Activator.CreateInstance(@this, args);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object ConstructDefault(
      this Type? @this,
      object @default,
      params object?[]? args) =>
    @this == default ? @default :
    Activator.CreateInstance(@this, args) ?? @default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object ConstructDefault(
      this Type? @this,
      Func<object> @default,
      params object?[]? args) =>
    @this == default ? @default() :
    Activator.CreateInstance(@this, args) ?? @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? Construct<T>(
      params object?[]? args) where T : class =>
    Activator.CreateInstance(typeof(T), args).As<T>();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? Construct<T>(
      this Type? @this,
      params object?[]? args) where T : class =>
    @this == default ? default :
    Activator.CreateInstance(@this, args).As<T>();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T ConstructDefault<T>(
      this Type? @this,
      T @default,
      params object?[]? args) where T : class =>
    @this == default ? @default :
    Activator.CreateInstance(@this, args).As<T>(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T ConstructDefault<T>(
      this Type? @this,
      Func<T> @default,
      params object?[]? args) where T : class =>
    @this == default ? @default() :
    Activator.CreateInstance(@this, args).As<T>(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<T> ConstructDefaultTask<T>(
      this Type? @this,
      Func<Task<T>> @default,
      params object?[]? args) where T : class =>
    @this == default ? @default().ToValueTask() :
    Activator.CreateInstance(@this, args).AsTask<T>(@default);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<T> ConstructDefaultValueTask<T>(
      this Type? @this,
      Func<ValueTask<T>> @default,
      params object?[]? args) where T : class =>
    @this == default ? @default() :
    Activator.CreateInstance(@this, args).AsValueTask<T>(@default);
}
