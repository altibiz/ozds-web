using System.Runtime.CompilerServices;
using System.Reflection;

namespace Ozds.Extensions;

public static partial class Functions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object? TryInvoke(
      this MethodBase method,
      object? @this,
      params object?[]? args)
  {
    try
    {
      return method.Invoke(@this, args);
    }
    catch
    {
      return default;
    }
  }
}
