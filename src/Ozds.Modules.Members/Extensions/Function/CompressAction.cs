namespace Ozds.Modules.Members;

public static partial class FunctionExtensions
{
  public static Action<ValueTuple> Compress<TOut>(
      this Action @this) => (ValueTuple args) => @this();

  public static Action<ValueTuple<TIn1>>
  Compress<TIn1>(this Action<TIn1> @this) => (
      ValueTuple<TIn1> args) => @this(args.Item1);

  public static Action<ValueTuple<TIn1, TIn2>>
  Compress<TIn1, TIn2>(this Action<TIn1, TIn2> @this) => (
      ValueTuple<TIn1, TIn2> args) => @this(args.Item1, args.Item2);

  public static Action<ValueTuple<TIn1, TIn2, TIn3>>
  Compress<TIn1, TIn2, TIn3>(this Action<TIn1, TIn2, TIn3> @this) => (
      ValueTuple<TIn1, TIn2, TIn3> args) => @this(args.Item1, args.Item2,
      args.Item3);

  public static Action<ValueTuple<TIn1, TIn2, TIn3, TIn4>>
  Compress<TIn1, TIn2, TIn3, TIn4>(
      this Action<TIn1, TIn2, TIn3, TIn4> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4);

  public static Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5>>
  Compress<TIn1, TIn2, TIn3, TIn4, TIn5>(
      this Action<TIn1, TIn2, TIn3, TIn4, TIn5> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4, args.Item5);

  public static Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>>
  Compress<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(
      this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> args) => @this(args.Item1,
          args.Item2, args.Item3, args.Item4, args.Item5, args.Item6);

  public static Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>>
  Compress<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(
      this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4, args.Item5,
          args.Item6, args.Item7);

  public static Action<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>>
  Compress<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>(
      this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest> @this) where TRest : struct =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4, args.Item5,
          args.Item6, args.Item7, args.Rest);
}
