using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Object
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1>
  StatefulAction<TState, TIn1>(
      this TState state,
      Action<TState, TIn1> stateless) =>
    (TIn1 arg1) =>
      stateless(state, arg1);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2>
  StatefulAction<TState, TIn1, TIn2>(
      this TState state,
      Action<TState, TIn1, TIn2> stateless) =>
    (TIn1 arg1, TIn2 arg2) =>
      stateless(state, arg1, arg2);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3>
  StatefulAction<TState, TIn1, TIn2, TIn3>(
      this TState state,
      Action<TState, TIn1, TIn2, TIn3> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3) =>
      stateless(state, arg1, arg2, arg3);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3, TIn4>
  StatefulAction<TState, TIn1, TIn2, TIn3, TIn4>(
      this TState state,
      Action<TState, TIn1, TIn2, TIn3, TIn4> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4) =>
      stateless(state, arg1, arg2, arg3, arg4);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5>
  StatefulAction<TState, TIn1, TIn2, TIn3, TIn4, TIn5>(
      this TState state,
      Action<TState, TIn1, TIn2, TIn3, TIn4, TIn5> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5) =>
      stateless(state, arg1, arg2, arg3, arg4, arg5);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>
  StatefulAction<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(
      this TState state,
      Action<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6) =>
      stateless(state, arg1, arg2, arg3, arg4, arg5, arg6);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>
  StatefulAction<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(
      this TState state,
      Action<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7) =>
      stateless(state, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8>
  StatefulAction<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8>(
      this TState state,
      Action<TState, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8> stateless) =>
    (TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TIn8 arg8) =>
    stateless(state, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
}
