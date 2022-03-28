using System;
using System.Globalization;

namespace Elasticsearch {
public static class DateTimeExtensions {
  public static string ToISOString(this DateTime dateTime) => dateTime.ToString(
      "o", CultureInfo.InvariantCulture);
}
}
