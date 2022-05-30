using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? As<T>(
      this object? @this) where T : class =>
    @this as T ?? default;
}
