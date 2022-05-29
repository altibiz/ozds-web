using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TIn WithNullable<TIn>(
      this TIn @this,
      Action<TIn> action)
  {
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<TIn> WithNullable<TIn>(
      this TIn @this,
      Func<TIn, Task> action)
  {
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<TIn> WithNullable<TIn>(
      this TIn @this,
      Func<TIn, ValueTask> action)
  {
    await action(@this);
    return @this;
  }
}
