namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static bool Falsy<T>(this T? @this) => !@this.Truthy();
}
