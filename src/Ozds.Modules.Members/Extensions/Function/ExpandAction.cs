namespace Ozds.Modules.Members;

public static partial class FunctionExtensions
{
  public static Action Expand(
      this Action<ValueTuple> @this) => () => @this(new());

  public static Action<TIn1>
  Expand<TIn1>(this Action<ValueTuple<TIn1>> @this) => (
      TIn1 arg1) => @this(new(arg1));

  public static Action<TIn1, TIn2>
  Expand<TIn1, TIn2>(this Action<ValueTuple<TIn1, TIn2>> @this) => (
      TIn1 arg1, TIn2 arg2) => @this((arg1, arg2));

  public static Action<TIn1, TIn2, TIn3>
  Expand<TIn1, TIn2, TIn3>(this Action<ValueTuple<TIn1, TIn2, TIn3>> @this) => (
      TIn1 arg1, TIn2 arg2, TIn3 arg3) => @this((arg1, arg2, arg3));

  public static Action<TIn1, TIn2, TIn3, TIn4>
  Expand<TIn1, TIn2, TIn3, TIn4>(
      this Action<ValueTuple<TIn1, TIn2, TIn3, TIn4>> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4) => @this((arg1, arg2, arg3, arg4));

  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5>
  Expand<TIn1, TIn2, TIn3, TIn4, TIn5>(
      this Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5>> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5) => @this((arg1, arg2, arg3, arg4, arg5));

  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>
  Expand<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(
      this Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6) => @this((arg1, arg2, arg3, arg4, arg5, arg6));

  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>
  Expand<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(
      this Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>> @this) =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7) => @this((arg1, arg2, arg3, arg4, arg5, arg6, arg7));

  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>
  Expand<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>(
      this Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>> @this) where TRest : struct =>
      (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TRest argRest) =>
      @this(new ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, argRest));
}
