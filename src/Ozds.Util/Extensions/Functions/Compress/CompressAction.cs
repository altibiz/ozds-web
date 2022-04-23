using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Functions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<ValueTuple> CompressAction<TOut>(
      this Action @this) => (ValueTuple args) => @this();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<ValueTuple<TIn1>>
  CompressAction<TIn1>(this Action<TIn1> @this) => (
      ValueTuple<TIn1> args) => @this(args.Item1);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<ValueTuple<TIn1, TIn2>>
  CompressAction<TIn1, TIn2>(this Action<TIn1, TIn2> @this) => (
      ValueTuple<TIn1, TIn2> args) => @this(args.Item1, args.Item2);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<ValueTuple<TIn1, TIn2, TIn3>>
  CompressAction<TIn1, TIn2, TIn3>(this Action<TIn1, TIn2, TIn3> @this) => (
      ValueTuple<TIn1, TIn2, TIn3> args) => @this(args.Item1, args.Item2,
      args.Item3);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<ValueTuple<TIn1, TIn2, TIn3, TIn4>>
  CompressAction<TIn1, TIn2, TIn3, TIn4>(
      this Action<TIn1, TIn2, TIn3, TIn4> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5>>
  CompressAction<TIn1, TIn2, TIn3, TIn4, TIn5>(
      this Action<TIn1, TIn2, TIn3, TIn4, TIn5> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4, args.Item5);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>>
  CompressAction<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(
      this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> args) => @this(args.Item1,
          args.Item2, args.Item3, args.Item4, args.Item5, args.Item6);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>>
  CompressAction<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(
      this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4, args.Item5,
          args.Item6, args.Item7);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>>
  CompressAction<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>(
      this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest> @this) where TRest : struct =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4, args.Item5,
          args.Item6, args.Item7, args.Rest);
}
