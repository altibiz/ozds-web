namespace Ozds.Util;

public static partial class TaskExtensions
{
  public static T Wait<T>(this Task<T> @this)
  {
    @this.Wait();
    return @this.Result;
  }
}
