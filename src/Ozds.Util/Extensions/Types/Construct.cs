using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Types
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object? Construct(
      this Type @this,
      params object?[]? args) =>
    Activator.CreateInstance(@this, args);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? Construct<T>(
      params object?[]? args) where T : class =>
    Activator.CreateInstance(typeof(T), args).As<T>();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? Construct<T>(
      this Type @this,
      params object?[]? args) where T : class =>
    Activator.CreateInstance(@this, args).As<T>();
}
