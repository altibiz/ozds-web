using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Functions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Id<T>(T value) => value;
}
