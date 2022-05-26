using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Ozds.Util;

public static partial class Strings
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string RegexRemove(this string str, Regex pattern) =>
    str.RegexReplace(pattern, "");

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string RegexRemove(this string str, string pattern) =>
    str.RegexReplace(pattern, "");
}
