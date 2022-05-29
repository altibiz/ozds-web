using System.Runtime.CompilerServices;
namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T ThrowWhenNull<T>(
      this T? @this) where T : struct =>
    @this is null ? throw new NullReferenceException()
    : @this.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task<T> ThrowWhenNull<T>(
      this Task<T?> @this) where T : struct =>
    @this.Then(@this =>
      @this is null ? throw new NullReferenceException()
      : @this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ValueTask<T> ThrowWhenNull<T>(
      this ValueTask<T?> @this) where T : struct =>
    @this.Then(@this =>
      @this is null ? throw new NullReferenceException()
      : @this.Value);

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
