using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Strings
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ConsoleLog(this string str)
  {
    Console.WriteLine(str);
    return str;
  }
}
