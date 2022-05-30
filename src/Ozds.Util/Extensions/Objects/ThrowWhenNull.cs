using System.Runtime.CompilerServices;
namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T ThrowWhenNull<T>(
      this T? @this) =>
    @this is null ? throw new NullReferenceException()
    : @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThrowWhenNull<T>(
      this Task<T?> @this) =>
    await @this.Then(@this =>
      @this is null ? throw new NullReferenceException()
      : @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThrowWhenNull<T>(
      this ValueTask<T?> @this) =>
    await @this.Then(@this =>
      @this is null ? throw new NullReferenceException()
      : @this);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T ThrowWhenNull<T>(
      this T? @this) where T : struct =>
    @this is null ? throw new NullReferenceException()
    : @this.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThrowWhenNull<T>(
      this Task<T?> @this) where T : struct =>
    await @this.Then(@this =>
      @this is null ? throw new NullReferenceException()
      : @this.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> ThrowWhenNull<T>(
      this ValueTask<T?> @this) where T : struct =>
    await @this.Then(@this =>
      @this is null ? throw new NullReferenceException()
      : @this.Value);

}
