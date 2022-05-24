using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T OnlyWhenNullable<T>(
      this T? @this,
      T @default) =>
    !@this.Truthy() ? @default :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> OnlyWhenNullable<T>(
      this T? @this,
      Task<T> @default) =>
    !@this.Truthy() ? await @default :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> OnlyWhenNullable<T>(
      this T? @this,
      ValueTask<T> @default) =>
    !@this.Truthy() ? await @default :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T OnlyWhenNullable<T>(
      this T? @this,
      Func<T> @default) =>
    !@this.Truthy() ? @default() :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> OnlyWhenNullable<T>(
      this T? @this,
      Func<Task<T>> @default) =>
    !@this.Truthy() ? await @default() :
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> OnlyWhenNullable<T>(
      this T? @this,
      Func<ValueTask<T>> @default) =>
    !@this.Truthy() ? await @default() :
    @this;
}
