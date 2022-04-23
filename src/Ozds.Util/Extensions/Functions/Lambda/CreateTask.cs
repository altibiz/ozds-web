using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Lambda
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<Task<T1>>
  CreateTask<T1>(
    Func<Task<T1>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, Task<T2>>
  CreateTask<T1, T2>(
    Func<T1, Task<T2>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, Task<T3>>
  CreateTask<T1, T2, T3>(
    Func<T1, T2, Task<T3>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, Task<T4>>
  CreateTask<T1, T2, T3, T4>(
    Func<T1, T2, T3, Task<T4>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, Task<T5>>
  CreateTask<T1, T2, T3, T4, T5>(
    Func<T1, T2, T3, T4, Task<T5>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, T5, Task<T6>>
  CreateTask<T1, T2, T3, T4, T5, T6>(
    Func<T1, T2, T3, T4, T5, Task<T6>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, T5, T6, Task<T7>>
  CreateTask<T1, T2, T3, T4, T5, T6, T7>(
    Func<T1, T2, T3, T4, T5, T6, Task<T7>> lambda) => lambda;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Func<T1, T2, T3, T4, T5, T6, T7, Task<T8>>
  CreateTask<T1, T2, T3, T4, T5, T6, T7, T8>(
    Func<T1, T2, T3, T4, T5, T6, T7, Task<T8>> lambda) => lambda;
}
