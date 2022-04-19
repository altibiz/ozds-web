namespace Ozds.Modules.Members;

public static partial class ObjectExtensions
{
  public static bool Predicate<TIn>(
      this TIn? @this,
      Predicate<TIn> predicate) =>
    !@this.Truthy() ? false : predicate(@this);
}
