using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  // TODO: merge with To
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut Var<TIn, TOut>(
      this TIn @this,
      Func<TIn, TOut> selector) =>
    selector(@this);
}
