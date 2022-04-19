namespace Ozds.Modules.Members;

public static partial class ObjectExtensions
{
  public static bool Falsy<T>(this T? @this) => !@this.Truthy();
}
