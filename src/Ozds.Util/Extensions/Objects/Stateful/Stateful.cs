using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Object
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TOut>
  Stateful<TState, TIn1, TOut>(
      this TState state,
      Func<TState, TIn1, TOut> stateless) =>
    (TIn1 arg1) =>
      stateless(state, arg1);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TOut>
  Stateful<TState, TIn1, TIn2, TOut>(
      this TState state,
      Func<TState, TIn1, TIn2, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2) =>
      stateless(state, arg1, arg2);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TOut>
  Stateful<TState, TIn1, TIn2, TIn3, TOut>(
      this TState state,
      Func<TState, TIn1, TIn2, TIn3, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3) =>
      stateless(state, arg1, arg2, arg3);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TIn4, TOut>
  Stateful<TState, TIn1, TIn2, TIn3, TIn4, TOut>(
      this TState state,
      Func<TState, TIn1, TIn2, TIn3, TIn4, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4) =>
      stateless(state, arg1, arg2, arg3, arg4);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>
  Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(
      this TState state,
      Func<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5) =>
      stateless(state, arg1, arg2, arg3, arg4, arg5);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>
  Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(
      this TState state,
      Func<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6) =>
      stateless(state, arg1, arg2, arg3, arg4, arg5, arg6);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>
  Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(
      this TState state,
      Func<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7) =>
      stateless(state, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>
  Stateful<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>(
      this TState state,
      Func<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TIn8 arg8) =>
      stateless(state, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
}
