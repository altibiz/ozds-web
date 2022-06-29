using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Strings
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int? TryParseInt(
      this string @this) =>
    int.TryParse(@this, out var @int) ? @int
    : null;
}
