using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Block(this Task @this) =>
    @this.Wait();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Block<T>(this Task<T> @this) =>
    @this.Result;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Block(this ValueTask @this) =>
    @this.ToTask().Block();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Block<T>(this ValueTask<T> @this) =>
    @this.ToTask().Block();
}
