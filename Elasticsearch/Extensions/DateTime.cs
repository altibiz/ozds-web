using System;
using System.Globalization;

namespace Elasticsearch
{
  public static class DateTimeExtensions
  {
    public static string ToUtcIsoString(
        this DateTime dateTime) => dateTime.ToUniversalTime().ToString("o",
        CultureInfo.InvariantCulture);
  }
}
