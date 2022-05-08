using System.Text.RegularExpressions;

namespace Ozds.Util;

public static partial class Strings
{
  public static string RegexReplace(this string str, Regex pattern,
      string replacement) => pattern.Replace(str, replacement);

  public static string RegexReplace(this string str, string pattern,
      string replacement) => Regex.Replace(str, pattern, replacement);
}
