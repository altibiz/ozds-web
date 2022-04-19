namespace Ozds.Modules.Members;

public static class TypeExtensions
{
  public static T? Construct<T>(
      params object?[]? args) where T : class =>
    Activator.CreateInstance(typeof(T), args).As<T>();

  public static T? Construct<T>(
      this Type? @this,
      params object?[]? args) where T : class =>
    @this == default ? default :
    Activator.CreateInstance(@this, args).As<T>();

  public static T Construct<T>(
      this Type? @this,
      T @default,
      params object?[]? args) where T : class =>
    @this == default ? @default :
    Activator.CreateInstance(@this, args).As<T>(@default);

  public static T Construct<T>(
      this Type? @this,
      Func<T> @default,
      params object?[]? args) where T : class =>
    @this == default ? @default() :
    Activator.CreateInstance(@this, args).As<T>(@default);

  public static ValueTask<T> Construct<T>(
      this Type? @this,
      Func<Task<T>> @default,
      params object?[]? args) where T : class =>
    @this == default ? @default().ToValueTask() :
    Activator.CreateInstance(@this, args).As<T>(@default);

  public static ValueTask<T> Construct<T>(
      this Type? @this,
      Func<ValueTask<T>> @default,
      params object?[]? args) where T : class =>
    @this == default ? @default() :
    Activator.CreateInstance(@this, args).As<T>(@default);

  public static bool IsAssignableTo<T>(this Type @this) =>
    @this.IsAssignableTo(typeof(T));

  public static bool IsAssignableFrom<T>(this Type @this) =>
    @this.IsAssignableFrom(typeof(T));
}
