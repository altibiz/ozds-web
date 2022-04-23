using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Functions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action ExpandAction(
      this Action<ValueTuple> @this) => () => @this(new());

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1>
  ExpandAction<TIn1>(this Action<ValueTuple<TIn1>> @this) => (
      TIn1 arg1) => @this(new(arg1));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2>
  ExpandAction<TIn1, TIn2>(this Action<ValueTuple<TIn1, TIn2>> @this) => (
      TIn1 arg1, TIn2 arg2) => @this((arg1, arg2));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3>
  ExpandAction<TIn1, TIn2, TIn3>(this Action<ValueTuple<TIn1, TIn2, TIn3>> @this) => (
      TIn1 arg1, TIn2 arg2, TIn3 arg3) => @this((arg1, arg2, arg3));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3, TIn4>
  ExpandAction<TIn1, TIn2, TIn3, TIn4>(
      this Action<ValueTuple<TIn1, TIn2, TIn3, TIn4>> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4) => @this((arg1, arg2, arg3, arg4));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5>
  ExpandAction<TIn1, TIn2, TIn3, TIn4, TIn5>(
      this Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5>> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5) => @this((arg1, arg2, arg3, arg4, arg5));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>
  ExpandAction<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(
      this Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6) => @this((arg1, arg2, arg3, arg4, arg5, arg6));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>
  ExpandAction<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(
      this Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7) => @this((arg1, arg2, arg3, arg4, arg5, arg6, arg7));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>
  ExpandAction<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>(
      this Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>> @this) where TRest : struct =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TRest argRest) =>
      @this(new ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, argRest));
}
