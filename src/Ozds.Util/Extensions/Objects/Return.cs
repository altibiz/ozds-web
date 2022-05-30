using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Return<TIn>(
      this TIn? @this)
  { }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut Return<TIn, TOut>(
      this TIn? @this,
      TOut result) =>
    result;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut Return<TIn, TOut>(
      this TIn? @this,
      Func<TOut> result) =>
    result();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TOut> ReturnAsync<TIn, TOut>(
      this TIn? @this,
      Func<ValueTask<TOut>> result) =>
    await result();
}
