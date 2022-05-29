using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Tasks
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void BlockTask(this Task @this) =>
    @this.Wait();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T BlockTask<T>(this Task<T> @this) =>
    @this.Result;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void BlockValueTask(this ValueTask @this) =>
    @this.ToTask().BlockTask();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T BlockValueTask<T>(this ValueTask<T> @this) =>
    @this.ToTask().BlockTask();
}
