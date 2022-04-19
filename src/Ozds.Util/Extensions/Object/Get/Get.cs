using System.Reflection;

namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static TOut? Get<TOut>(
      this object? @this,
      string field) where TOut : class =>
    @this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .As<TOut>() ?? default;

  public static TOut Get<TOut>(
      this object? @this,
      string field,
      TOut @default) where TOut : class =>
    @this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .As<TOut>(@default) ?? @default;

  public static TOut Get<TOut>(
      this object? @this,
      string field,
      Func<TOut> @default) where TOut : class =>
    @this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .As<TOut>(@default) ?? @default();

  public static async ValueTask<TOut> Get<TOut>(
      this object? @this,
      string field,
      Func<Task<TOut>> @default) where TOut : class =>
    await (@this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .As<TOut>(@default) ?? @default().ToValueTask());

  public static async ValueTask<TOut> Get<TOut>(
      this object? @this,
      string field,
      Func<ValueTask<TOut>> @default) where TOut : class =>
    await (@this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .As<TOut>(@default) ?? @default());

  public static TOut? Get<TOut>(
      this object? @this,
      string field,
      BindingFlags binding) where TOut : class =>
    @this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .As<TOut>() ?? default;

  public static TOut Get<TOut>(
      this object? @this,
      string field,
      BindingFlags binding,
      Func<TOut> @default) where TOut : class =>
    @this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .As<TOut>(@default) ?? @default();

  public static async ValueTask<TOut> Get<TOut>(
      this object? @this,
      string field,
      BindingFlags binding,
      Func<Task<TOut>> @default) where TOut : class =>
    await (@this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .As<TOut>(@default) ?? @default().ToValueTask());

  public static async ValueTask<TOut> Get<TOut>(
      this object? @this,
      string field,
      BindingFlags binding,
      Func<ValueTask<TOut>> @default) where TOut : class =>
    await (@this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .As<TOut>(@default) ?? @default());
}
