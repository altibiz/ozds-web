using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Functions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Predicate<T> Predicate<T>(this Func<T, bool> @this) =>
    (T arg) => @this(arg);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Predicate<T?> NullablePredicate<T>(this Func<T, bool> @this) =>
    (T? arg) => arg is null ? false : @this(arg);
}
