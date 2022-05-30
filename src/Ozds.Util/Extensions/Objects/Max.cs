using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Max<T>(
      T lhs,
      T? rhs) where T : IComparable<T> =>
    rhs is null ? lhs
    : lhs.CompareTo(rhs) < 0 ? rhs
    : lhs;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Max<T>(
      T head,
      params T?[] @rest) where T : IComparable<T> =>
    @rest.Aggregate(head, (min, next) => Max(min, next));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Max<T>(
      T lhs,
      T? rhs) where T : struct, IComparable<T> =>
    rhs is null ? lhs
    : lhs.CompareTo(rhs.Value) < 0 ? rhs.Value
    : lhs;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Max<T>(
      T head,
      params T?[] @rest) where T : struct, IComparable<T> =>
    @rest.Aggregate(head, (min, next) => Max(min, next));
}
