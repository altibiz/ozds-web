using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Min<T>(
      T lhs,
      T? rhs) where T : IComparable<T> =>
    rhs is null ? lhs
    : lhs.CompareTo(rhs) < 0 ? lhs
    : rhs;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Min<T>(
      T head,
      params T?[] @rest) where T : IComparable<T> =>
    @rest.Aggregate(head, (min, next) => Min(min, next));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Min<T>(
      T lhs,
      T? rhs) where T : struct, IComparable<T> =>
    !rhs.HasValue ? lhs
    : lhs.CompareTo(rhs.Value) < 0 ? lhs
    : rhs.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Min<T>(
      T head,
      params T?[] @rest) where T : struct, IComparable<T> =>
    @rest.Aggregate(head, (min, next) => Min(min, next));
}
