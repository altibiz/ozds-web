using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut WhenNullable<TIn, TOut>(
      this TIn @this,
      Func<TIn, TOut> selector) =>
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<TOut> WhenNullableTask<TIn, TOut>(
      this TIn @this,
      Func<TIn, Task<TOut>> selector) =>
    selector(@this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<TOut> WhenNullableValueTask<TIn, TOut>(
      this TIn @this,
      Func<TIn, ValueTask<TOut>> selector) =>
    selector(@this);
}
