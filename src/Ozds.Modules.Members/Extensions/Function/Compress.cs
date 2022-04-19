namespace Ozds.Modules.Members;

public static partial class FunctionExtensions
{
  public static Func<ValueTuple, TOut> Compress<TOut>(
      this Func<TOut> @this) => (ValueTuple args) => @this();

  public static Func<ValueTuple<TIn1>, TOut>
  Compress<TIn1, TOut>(this Func<TIn1, TOut> @this) => (
      ValueTuple<TIn1> args) => @this(args.Item1);

  public static Func<ValueTuple<TIn1, TIn2>, TOut>
  Compress<TIn1, TIn2, TOut>(this Func<TIn1, TIn2, TOut> @this) => (
      ValueTuple<TIn1, TIn2> args) => @this(args.Item1, args.Item2);

  public static Func<ValueTuple<TIn1, TIn2, TIn3>, TOut>
  Compress<TIn1, TIn2, TIn3, TOut>(this Func<TIn1, TIn2, TIn3, TOut> @this) => (
      ValueTuple<TIn1, TIn2, TIn3> args) => @this(args.Item1, args.Item2,
      args.Item3);

  public static Func<ValueTuple<TIn1, TIn2, TIn3, TIn4>, TOut>
  Compress<TIn1, TIn2, TIn3, TIn4, TOut>(
      this Func<TIn1, TIn2, TIn3, TIn4, TOut> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4);

  public static Func<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5>, TOut>
  Compress<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(
      this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4, args.Item5);

  public static Func<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>, TOut>
  Compress<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(
      this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> args) => @this(args.Item1,
          args.Item2, args.Item3, args.Item4, args.Item5, args.Item6);

  public static Func<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>, TOut>
  Compress<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(
      this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut> @this) =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4, args.Item5,
          args.Item6, args.Item7);

  public static Func<ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest>, TOut>
  Compress<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest, TOut>(
      this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest, TOut> @this) where TRest : struct =>
      (ValueTuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TRest> args) => @this(
          args.Item1, args.Item2, args.Item3, args.Item4, args.Item5,
          args.Item6, args.Item7, args.Rest);
}
