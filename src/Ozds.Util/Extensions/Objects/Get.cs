using System.Runtime.CompilerServices;
using System.Reflection;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? Get<T>(
      this object? @this,
      string field) where T : class =>
    @this
      ?.GetType()
      .GetField(field)
      ?.GetValue(@this) as T;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? Get<T>(
      this object? @this,
      string field,
      BindingFlags binding) where T : class =>
    @this
      ?.GetType()
      .GetField(field, binding)
      ?.GetValue(@this) as T;
}
