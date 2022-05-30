using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut To<TIn, TOut>(
      this TIn @this,
      Func<TIn, TOut> selector) =>
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> To<TIn, TOut>(
      this TIn @this,
      Func<TIn, Task<TOut>> selector) =>
    await selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> To<TIn, TOut>(
      this TIn @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    await selector(@this);
}
