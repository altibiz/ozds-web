using System;
using System.Globalization;

namespace Ozds.Elasticsearch {
  public static class DateTimeExtensions {
    public static string ToUtcIsoString(
        this DateTime dateTime) => dateTime.ToUniversalTime().ToString("o",
        CultureInfo.InvariantCulture);
  }
}
