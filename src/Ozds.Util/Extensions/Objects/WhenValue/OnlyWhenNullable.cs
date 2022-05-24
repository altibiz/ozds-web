using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T OnlyWhenNullable<T>(
      this T? @this,
      T @default) where T : struct =>
    !@this.Truthy() ? @default :
    @this.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T OnlyWhenNullable<T>(
      this T? @this,
      Func<T> @default) where T : struct =>
    !@this.Truthy() ? @default() :
    @this.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> OnlyWhenNullable<T>(
      this T? @this,
      Func<Task<T>> @default) where T : struct =>
    !@this.Truthy() ? await @default() :
    @this.Value;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> OnlyWhenNullable<T>(
      this T? @this,
      Func<ValueTask<T>> @default) where T : struct =>
    !@this.Truthy() ? await @default() :
    @this.Value;
}
