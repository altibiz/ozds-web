using System.Text.Json;

namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static string ToJson<T>(this T? @this) =>
    JsonSerializer.Serialize(@this);
}
