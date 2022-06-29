using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class ValueTuples
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2)?
  FilterNull<T1, T2>(
      this (T1?, T2?) @this) =>
    @this.Item1 is null ||
    @this.Item2 is null
    ? null :
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3)?
  FilterNull<T1, T2, T3>(
      this (T1?, T2?, T3?) @this) =>
    @this.Item1 is null ||
    @this.Item2 is null ||
    @this.Item3 is null
    ? null :
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3, T4)?
  FilterNull<T1, T2, T3, T4>(
      this (T1?, T2?, T3?, T4?) @this) =>
    @this.Item1 is null ||
    @this.Item2 is null ||
    @this.Item3 is null ||
    @this.Item4 is null
    ? null :
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3, T4, T5)?
  FilterNull<T1, T2, T3, T4, T5>(
      this (T1?, T2?, T3?, T4?, T5?) @this) =>
    @this.Item1 is null ||
    @this.Item2 is null ||
    @this.Item3 is null ||
    @this.Item4 is null ||
    @this.Item5 is null
    ? null :
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3, T4, T5, T6)?
  FilterNull<T1, T2, T3, T4, T5, T6>(
      this (T1?, T2?, T3?, T4?, T5?, T6?) @this) =>
    @this.Item1 is null ||
    @this.Item2 is null ||
    @this.Item3 is null ||
    @this.Item4 is null ||
    @this.Item5 is null ||
    @this.Item6 is null
    ? null :
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3, T4, T5, T6, T7)?
  FilterNull<T1, T2, T3, T4, T5, T6, T7>(
      this (T1?, T2?, T3?, T4?, T5?, T6?, T7?) @this) =>
    @this.Item1 is null ||
    @this.Item2 is null ||
    @this.Item3 is null ||
    @this.Item4 is null ||
    @this.Item5 is null ||
    @this.Item6 is null ||
    @this.Item7 is null
    ? null :
    @this!;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3, T4, T5, T6, T7, TRest)?
  FilterNull<T1, T2, T3, T4, T5, T6, T7, TRest>(
      this (T1?, T2?, T3?, T4?, T5?, T6?, T7?, TRest) @this)
        where TRest : struct =>
    @this.Item1 is null ||
    @this.Item2 is null ||
    @this.Item3 is null ||
    @this.Item4 is null ||
    @this.Item5 is null ||
    @this.Item6 is null ||
    @this.Item7 is null
    ? null :
    @this!;
}
