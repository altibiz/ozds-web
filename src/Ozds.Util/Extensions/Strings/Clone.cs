using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Strings
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CloneString(this string str) =>
    (str.Clone() as string)!;
}
