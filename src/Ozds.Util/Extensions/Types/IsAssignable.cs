using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Types
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsAssignableTo<T>(
      this Type @this) =>
    @this.IsAssignableTo(typeof(T));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsAssignableFrom<T>(
      this Type @this) =>
    @this.IsAssignableFrom(typeof(T));
}
