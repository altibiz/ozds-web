using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace Ozds.Util;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Falsy<T>(
      [NotNullWhen(false)] this Nullable<T> @this) where T : struct =>
    !@this.Truthy();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Falsy<T>(
      [NotNullWhen(false)] this T? @this) =>
    !@this.Truthy();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<bool> FalsyTask<T>(
      [NotNullWhen(false)] this Task<T> @this) =>
    !await @this.TruthyTask();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<bool> FalsyValueTask<T>(
      [NotNullWhen(false)] this ValueTask<T> @this) =>
    !await @this.TruthyValueTask();
}
