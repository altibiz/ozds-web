using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Lambda
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1>
  Create<T1>(
    Func<T1> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2>
  Create<T1, T2>(
    Func<T1, T2> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3>
  Create<T1, T2, T3>(
    Func<T1, T2, T3> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4>
  Create<T1, T2, T3, T4>(
    Func<T1, T2, T3, T4> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, T5>
  Create<T1, T2, T3, T4, T5>(
    Func<T1, T2, T3, T4, T5> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, T5, T6>
  Create<T1, T2, T3, T4, T5, T6>(
    Func<T1, T2, T3, T4, T5, T6> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, T5, T6, T7>
  Create<T1, T2, T3, T4, T5, T6, T7>(
    Func<T1, T2, T3, T4, T5, T6, T7> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, T5, T6, T7, T8>
  Create<T1, T2, T3, T4, T5, T6, T7, T8>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8> lambda) => lambda;
}
