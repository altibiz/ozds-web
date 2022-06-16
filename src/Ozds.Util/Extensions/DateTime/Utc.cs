using System.Runtime.CompilerServices;
using System.Globalization;

namespace Ozds.Extensions;

public static class DateTimeExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ToUtcIsoString(
      this DateTime @this) =>
    @this
      .ToUniversalTime()
      .ToString("o", CultureInfo.InvariantCulture);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime ToUtcIsoDateTime(
      this string @this) =>
    DateTime.Parse(@this).ToUniversalTime();
}
