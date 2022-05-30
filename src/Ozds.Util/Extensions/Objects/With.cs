using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T With<T>(
      this T @this,
      Action<T> action)
  {
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithAsync<T>(
      this T @this,
      Func<T, Task> action)
  {
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithAsync<T>(
      this T @this,
      Func<T, ValueTask> action)
  {
    await action(@this);
    return @this;
  }
}
