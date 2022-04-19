namespace Ozds.Modules.Members;

public static partial class ObjectExtensions
{
  public static T? Nullable<T>(this T @this) => @this;

  public static T NonNullable<T>(this T? @this) => @this!;
}
