using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Truthy<T>(
      [NotNullWhen(true)] this T? @this) where T : struct =>
    !@this.Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Truthy<T>(
      [NotNullWhen(true)] this T? @this) =>
    !@this.Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Truthy(
      [NotNullWhen(true)] this bool @this) =>
    @this;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<bool> TruthyTask<T>(
      [NotNullWhen(true)] this Task<T> @this) =>
    (await @this).Truthy();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<bool> TruthyValueTask<T>(
      [NotNullWhen(true)] this ValueTask<T> @this) =>
    (await @this).Truthy();
}
