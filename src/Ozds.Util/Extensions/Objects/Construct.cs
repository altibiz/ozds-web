namespace Ozds.Util;

public static partial class Objects
{
  public static TOut? Construct<TOut>(
      this object? @this) where TOut : class =>
    Types.Construct<TOut>(@this);
}
