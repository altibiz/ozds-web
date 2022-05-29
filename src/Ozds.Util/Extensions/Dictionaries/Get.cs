using System.Runtime.CompilerServices;
using System.Collections;

namespace Ozds.Extensions;

public static partial class Dictionaries
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Get<T>(
      this IDictionary @this,
      string name) where T : class =>
    (@this[name] as T) ??
    throw new KeyNotFoundException($"{name} not found");
}
