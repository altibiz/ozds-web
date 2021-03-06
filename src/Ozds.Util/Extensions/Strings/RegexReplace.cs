using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Ozds.Extensions;

public static partial class Strings
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string RegexReplace(
      this string str,
      Regex pattern,
      string replacement) =>
    pattern.Replace(str, replacement);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string RegexReplace(
      this string str,
      string pattern,
      string replacement) =>
    str.RegexReplace(new Regex(pattern), replacement);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string RegexReplace(
      this string str,
      string pattern,
      string replacement,
      RegexOptions options) =>
    str.RegexReplace(new Regex(pattern, options), replacement);
}
