using System.Runtime.CompilerServices;
using System.Reflection;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? Get<T>(
      this object? @this,
      string field) where T : class =>
    @this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .As<T>() ?? default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Get<T>(
      this object? @this,
      string field,
      T @default) where T : class =>
    @this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .As<T>(@default) ?? @default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Get<T>(
      this object? @this,
      string field,
      Func<T> @default) where T : class =>
    @this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .As<T>(@default) ?? @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> Get<T>(
      this object? @this,
      string field,
      Func<Task<T>> @default) where T : class =>
    await (@this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .AsTask<T>(@default) ?? @default().ToValueTask());

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> Get<T>(
      this object? @this,
      string field,
      Func<ValueTask<T>> @default) where T : class =>
    await (@this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .AsValueTask<T>(@default) ?? @default());

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? Get<T>(
      this object? @this,
      string field,
      BindingFlags binding) where T : class =>
    @this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .As<T>() ?? default;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Get<T>(
      this object? @this,
      string field,
      BindingFlags binding,
      Func<T> @default) where T : class =>
    @this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .As<T>(@default) ?? @default();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> Get<T>(
      this object? @this,
      string field,
      BindingFlags binding,
      Func<Task<T>> @default) where T : class =>
    await (@this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .AsTask<T>(@default) ?? @default().ToValueTask());

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> Get<T>(
      this object? @this,
      string field,
      BindingFlags binding,
      Func<ValueTask<T>> @default) where T : class =>
    await (@this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .AsValueTask<T>(@default) ?? @default());
}
