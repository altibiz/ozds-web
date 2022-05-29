using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TValue> ForEach<TValue>(
      this IEnumerable<TValue> @this,
      Action<TValue> @do)
  {
    foreach (var value in @this)
    {
      @do(value);
      yield return value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> ForEachAwait<TValue>(
      this IEnumerable<TValue> @this,
      Func<TValue, ValueTask> @do)
  {
    foreach (var value in @this)
    {
      await @do(value);
      yield return value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> ForEachAsync<TValue>(
      this IAsyncEnumerable<TValue> @this,
      Action<TValue> @do)
  {
    await foreach (var value in @this)
    {
      @do(value);
      yield return value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async IAsyncEnumerable<TValue> ForEachAwaitAsync<TValue>(
      this IAsyncEnumerable<TValue> @this,
      Func<TValue, ValueTask> @do)
  {
    await foreach (var value in @this)
    {
      await @do(value);
      yield return value;
    }
  }
}
