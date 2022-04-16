using System.Reflection;

namespace Ozds.Modules.Members;

public static class ObjectExtensions
{
  public static TOut Select<TIn, TOut>(
      this TIn @this,
      Func<TIn, TOut> selector) =>
    selector(@this);

  public static void Select<TIn>(
      this TIn @this,
      Action<TIn> selector) =>
    selector(@this);

  public static Task<TOut> Select<TIn, TOut>(
      this TIn @this,
      Func<TIn, Task<TOut>> selector) =>
    selector(@this);

  public static Task Select<TIn>(
      this TIn @this,
      Func<TIn, Task> selector) =>
    selector(@this);

  public static TOut? SelectOrDefault<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut?> selector) where TIn : class =>
    @this == default ? default :
    selector(@this) ?? default;

  public static TOut SelectOrDefault<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut?> selector,
      TOut @default) where TIn : class =>
    @this == default ? @default :
    selector(@this) ?? @default;

  public static TOut SelectOrDefault<TIn, TOut>(
      this TIn? @this,
      Func<TIn, TOut?> selector,
      Func<TOut> @default) where TIn : class =>
    @this == default ? @default() :
    selector(@this) ?? @default();

  public static async Task<TOut?> SelectOrDefault<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut?>> selector) where TIn : class =>
    @this == default ? default :
    await selector(@this) ?? default;

  public static async Task<TOut> SelectOrDefault<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut?>> selector,
      TOut @default) where TIn : class =>
    @this == default ? @default :
    await selector(@this) ?? @default;

  public static async Task<TOut> SelectOrDefault<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut?>> selector,
      Func<TOut> @default) where TIn : class =>
    @this == default ? @default() :
    await selector(@this) ?? @default();

  public static async Task<TOut> SelectOrDefault<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<TOut?>> selector,
      Func<Task<TOut>> @default) where TIn : class =>
    @this == default ? await @default() :
    await selector(@this) ?? await @default();

  public static TOut? AsOrDefault<TOut>(
      this object? @this) where TOut : class =>
    @this as TOut ?? default;

  public static TOut AsOrDefault<TOut>(
      this object? @this,
      TOut @default) where TOut : class =>
    @this as TOut ?? @default;

  public static TOut AsOrDefault<TOut>(
      this object? @this,
      Func<TOut> @default) where TOut : class =>
    @this as TOut ?? @default();

  public static async Task<TOut> AsOrDefault<TOut>(
      this object? @this,
      Func<Task<TOut>> @default) where TOut : class =>
    @this as TOut ?? await @default();

  public static TOut? GetFieldOrDefault<TOut>(
      this object? @this,
      string field) where TOut : class =>
    @this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .AsOrDefault<TOut>() ?? default;

  public static TOut GetFieldOrDefault<TOut>(
      this object? @this,
      string field,
      TOut @default) where TOut : class =>
    @this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .AsOrDefault<TOut>(@default) ?? @default;

  public static TOut GetFieldOrDefault<TOut>(
      this object? @this,
      string field,
      Func<TOut> @default) where TOut : class =>
    @this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .AsOrDefault<TOut>(@default) ?? @default();

  public static async Task<TOut> GetFieldOrDefault<TOut>(
      this object? @this,
      string field,
      Func<Task<TOut>> @default) where TOut : class =>
    await (@this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this)
      .AsOrDefault<TOut>(@default) ?? @default());

  public static TOut? GetFieldOrDefault<TOut>(
      this object? @this,
      string field,
      BindingFlags binding) where TOut : class =>
    @this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .AsOrDefault<TOut>() ?? default;

  public static TOut GetFieldOrDefault<TOut>(
      this object? @this,
      string field,
      BindingFlags binding,
      Func<TOut> @default) where TOut : class =>
    @this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .AsOrDefault<TOut>(@default) ?? @default();

  public static async Task<TOut> GetFieldOrDefault<TOut>(
      this object? @this,
      string field,
      BindingFlags binding,
      Func<Task<TOut>> @default) where TOut : class =>
    await (@this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this)
      .AsOrDefault<TOut>(@default) ?? @default());

  public static bool FalseOrPredicate<TIn>(
      this TIn? @this,
      Predicate<TIn> predicate) where TIn : class =>
    @this == default ? false : predicate(@this);
}
