using System.Reflection;

namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static TOut? Get<TOut>(
      this object? @this,
      PropertyInfo field) where TOut : class =>
    field
      .GetValue(@this)
      .As<TOut>() ?? default;

  public static TOut Get<TOut>(
      this object? @this,
      PropertyInfo field,
      TOut @default) where TOut : class =>
    field
      .GetValue(@this)
      .As<TOut>(@default);

  public static TOut Get<TOut>(
      this object? @this,
      PropertyInfo field,
      Func<TOut> @default) where TOut : class =>
    field
      .GetValue(@this)
      .As<TOut>(@default);

  public static ValueTask<TOut> Get<TOut>(
      this object? @this,
      PropertyInfo field,
      Func<Task<TOut>> @default) where TOut : class =>
    field
      .GetValue(@this)
      .As<TOut>(@default);

  public static ValueTask<TOut> Get<TOut>(
      this object? @this,
      PropertyInfo field,
      Func<ValueTask<TOut>> @default) where TOut : class =>
    field
      .GetValue(@this)
      .As<TOut>(@default);
}
