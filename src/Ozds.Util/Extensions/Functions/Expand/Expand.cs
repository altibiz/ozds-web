using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Functions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TOut> Expand<TOut>(
      this Func<ValueTuple, TOut> @this) => () => @this(new());

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TOut>
  Expand<TIn1, TOut>(this Func<ValueTuple<TIn1>, TOut> @this) => (
      TIn1 arg1) => @this(new(arg1));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TOut>
  Expand<TIn1, TIn2, TOut>(this Func<ValueTuple<TIn1, TIn2>, TOut> @this) => (
      TIn1 arg1, TIn2 arg2) => @this((arg1, arg2));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TOut>
  Expand<TIn1, TIn2, TIn3, TOut>(this Func<ValueTuple<TIn1, TIn2, TIn3>, TOut> @this) => (
      TIn1 arg1, TIn2 arg2, TIn3 arg3) => @this((arg1, arg2, arg3));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TIn4, TOut>
  Expand<TIn1, TIn2, TIn3, TIn4, TOut>(
      this Func<ValueTuple<TIn1, TIn2, TIn3, TIn4>, TOut> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4) => @this((arg1, arg2, arg3, arg4));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>
  Expand<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(
      this Func<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5>, TOut> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5) => @this((arg1, arg2, arg3, arg4, arg5));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>
  Expand<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(
      this Func<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>, TOut> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6) => @this((arg1, arg2, arg3, arg4, arg5, arg6));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>
  Expand<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(
      this Func<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>, TOut> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7) => @this((arg1, arg2, arg3, arg4, arg5, arg6, arg7));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest, TOut>
  Expand<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest, TOut>(
      this Func<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>, TOut> @this) where TRest : struct =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TRest argRest) =>
      @this(new ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, argRest));
}
