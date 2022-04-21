namespace Ozds.Util;

public static class StringExtensions
{
  public static bool Empty(this string? @this) =>
    String.IsNullOrWhiteSpace(@this);
}
