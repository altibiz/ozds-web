namespace Ozds.Modules.Members;

public static partial class IEnumerableExtensions
{
  public static async Task<IEnumerable<T>> Await<T>(
      this IAsyncEnumerable<T> @this)
  {
    var result = new List<T>();
    await foreach (var current in @this)
    {
      result.Add(current);
    }
    return result;
  }

  public static async Task<IEnumerable<T>> Await<T>(
      this IEnumerable<Task<T>> @this)
  {
    var result = new List<T>();
    foreach (var current in @this)
    {
      result.Add(await current);
    }
    return result;
  }

  public static async ValueTask<IEnumerable<T>> Await<T>(
      this IEnumerable<ValueTask<T>> @this)
  {
    var result = new List<T>();
    foreach (var current in @this)
    {
      result.Add(await current);
    }
    return result;
  }
}
