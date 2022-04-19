namespace Ozds.Util;

public static partial class ActionExtensions
{
  public static Action<TIn1> Stateful<TState, TIn1>(
      this TState state, Action<TState, TIn1> stateless) =>
    (TIn1 arg1) => stateless(state, arg1);

  public static Action<TIn1, TIn2> Stateful<TState, TIn1, TIn2>(
      this TState state, Action<TState, TIn1, TIn2> stateless) =>
    (TIn1 arg1, TIn2 arg2) => stateless(state, arg1, arg2);

  public static Action<TIn1, TIn2, TIn3> Stateful<TState, TIn1, TIn2, TIn3>(
      this TState state, Action<TState, TIn1, TIn2, TIn3> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3) => stateless(state, arg1, arg2, arg3);

  public static Action<TIn1, TIn2, TIn3, TIn4> Stateful<TState, TIn1, TIn2, TIn3, TIn4>(
      this TState state, Action<TState, TIn1, TIn2, TIn3, TIn4> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4) => stateless(state, arg1, arg2, arg3, arg4);

  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5> Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5>(
      this TState state, Action<TState, TIn1, TIn2, TIn3, TIn4, TIn5> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5) => stateless(state, arg1, arg2, arg3, arg4, arg5);

  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(
      this TState state, Action<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6) => stateless(state, arg1, arg2, arg3, arg4, arg5, arg6);

  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(
      this TState state, Action<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7) => stateless(state, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8> Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8>(
      this TState state, Action<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TIn8 arg8) =>
    stateless(state, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
}

public static partial class FuncExtensions
{
  public static Func<TIn1, TOut> Stateful<TState, TIn1, TOut>(
      this TState state, Func<TState, TIn1, TOut> stateless) =>
    (TIn1 arg1) => stateless(state, arg1);

  public static Func<TIn1, TIn2, TOut> Stateful<TState, TIn1, TIn2, TOut>(
      this TState state, Func<TState, TIn1, TIn2, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2) => stateless(state, arg1, arg2);

  public static Func<TIn1, TIn2, TIn3, TOut> Stateful<TState, TIn1, TIn2, TIn3, TOut>(
      this TState state, Func<TState, TIn1, TIn2, TIn3, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3) => stateless(state, arg1, arg2, arg3);

  public static Func<TIn1, TIn2, TIn3, TIn4, TOut> Stateful<TState, TIn1, TIn2, TIn3, TIn4, TOut>(
      this TState state, Func<TState, TIn1, TIn2, TIn3, TIn4, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4) => stateless(state, arg1, arg2, arg3, arg4);

  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(
      this TState state, Func<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5) => stateless(state, arg1, arg2, arg3, arg4, arg5);

  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(
      this TState state, Func<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6) => stateless(state, arg1, arg2, arg3, arg4, arg5, arg6);

  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut> Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(
      this TState state, Func<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7) => stateless(state, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut> Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>(
      this TState state, Func<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TIn8 arg8) =>
    stateless(state, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
}
