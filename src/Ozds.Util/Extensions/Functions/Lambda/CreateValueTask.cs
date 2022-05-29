using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Lambda
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<ValueTask<T1>>
  CreateValueTask<T1>(
    Func<ValueTask<T1>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, ValueTask<T2>>
  CreateValueTask<T1, T2>(
    Func<T1, ValueTask<T2>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, ValueTask<T3>>
  CreateValueTask<T1, T2, T3>(
    Func<T1, T2, ValueTask<T3>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, ValueTask<T4>>
  CreateValueTask<T1, T2, T3, T4>(
    Func<T1, T2, T3, ValueTask<T4>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, ValueTask<T5>>
  CreateValueTask<T1, T2, T3, T4, T5>(
    Func<T1, T2, T3, T4, ValueTask<T5>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, T5, ValueTask<T6>>
  CreateValueTask<T1, T2, T3, T4, T5, T6>(
    Func<T1, T2, T3, T4, T5, ValueTask<T6>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, T5, T6, ValueTask<T7>>
  CreateValueTask<T1, T2, T3, T4, T5, T6, T7>(
    Func<T1, T2, T3, T4, T5, T6, ValueTask<T7>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, T5, T6, T7, ValueTask<T8>>
  CreateValueTask<T1, T2, T3, T4, T5, T6, T7, T8>(
    Func<T1, T2, T3, T4, T5, T6, T7, ValueTask<T8>> lambda) => lambda;
}
