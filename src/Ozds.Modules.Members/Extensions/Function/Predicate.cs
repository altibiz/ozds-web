namespace Ozds.Modules.Members;

public static partial class Func
{
  public static Predicate<T> Predicate<T>(this Func<T, bool> @this) =>
    (T arg) => @this(arg);
}
