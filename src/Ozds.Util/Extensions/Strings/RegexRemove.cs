using System.Text.RegularExpressions;

namespace Ozds.Util;

public static partial class Strings
{
  public static string RegexRemove(this string str, Regex pattern) =>
    str.RegexReplace(pattern, "");

  public static string RegexRemove(this string str, string pattern) =>
    str.RegexReplace(pattern, "");
}
