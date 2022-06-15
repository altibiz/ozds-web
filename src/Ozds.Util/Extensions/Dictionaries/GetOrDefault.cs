using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Dictionaries
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut? GetOrDefault<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key)
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      return default;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TOut? GetOrDefaultValue<TIn, TOut>(
      this IDictionary<TIn, TOut> @this,
      TIn key) where TOut : struct
  {
    try
    {
      return @this[key];
    }
    catch (KeyNotFoundException)
    {
      return default;
    }
  }
}
