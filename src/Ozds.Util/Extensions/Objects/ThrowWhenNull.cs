using System.Runtime.CompilerServices;
namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T ThrowWhenNull<T>(
      this T? @this) =>
    @this is null ? throw new NullReferenceException()
    : @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<T> ThrowWhenNull<T>(
      this Task<T?> @this) =>
    @this.Then(@this =>
      @this is null ? throw new NullReferenceException()
      : @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<T> ThrowWhenNull<T>(
      this ValueTask<T?> @this) =>
    @this.Then(@this =>
      @this is null ? throw new NullReferenceException()
      : @this);
}
