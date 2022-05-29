using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool NotEmpty<T>(
      [NotNullWhen(false)] this Nullable<T> @this) where T : struct =>
    !@this.Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool NotEmpty<T>(
      [NotNullWhen(false)] this T? @this) =>
    !@this.Empty();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<bool> NotEmptyTask<T>(
      [NotNullWhen(false)] this Task<T> @this) =>
    !await @this.EmptyTask();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<bool> NotEmptyValueTask<T>(
      [NotNullWhen(false)] this ValueTask<T> @this) =>
    !await @this.EmptyValueTask();
}
