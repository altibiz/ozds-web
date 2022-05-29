using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Strings
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ConsoleLog(this string str)
  {
    Console.WriteLine(str);
    return str;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ConsoleTitledLog(this string str, string title)
  {
    Console.WriteLine(title);
    Console.WriteLine(str);
    return str;
  }
}
