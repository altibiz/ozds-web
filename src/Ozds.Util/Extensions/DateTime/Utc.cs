using System.Runtime.CompilerServices;
using System.Globalization;

namespace Ozds.Elasticsearch;

public static class DateTimeExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ToUtcIsoString(
      this DateTime @this) =>
    @this
      .ToUniversalTime()
      .ToString("o", CultureInfo.InvariantCulture);
}
