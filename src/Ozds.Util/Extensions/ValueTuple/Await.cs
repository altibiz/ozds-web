using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class ValueTuples
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<(T1, T2)>
  Await<T1, T2>(
      this (
        Task<T1>,
        Task<T2>) @this) =>
    (await @this.Item1,
     await @this.Item2);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<(T1, T2, T3)>
  Await<T1, T2, T3>(
      this (
        Task<T1>,
        Task<T2>,
        Task<T3>) @this) =>
    (await @this.Item1,
     await @this.Item2,
     await @this.Item3);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<(T1, T2, T3, T4)>
  Await<T1, T2, T3, T4>(
      this (
        Task<T1>,
        Task<T2>,
        Task<T3>,
        Task<T4>) @this) =>
    (await @this.Item1,
     await @this.Item2,
     await @this.Item3,
     await @this.Item4);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<(T1, T2, T3, T4, T5)>
  Await<T1, T2, T3, T4, T5>(
      this (
        Task<T1>,
        Task<T2>,
        Task<T3>,
        Task<T4>,
        Task<T5>) @this) =>
    (await @this.Item1,
     await @this.Item2,
     await @this.Item3,
     await @this.Item4,
     await @this.Item5);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<(T1, T2, T3, T4, T5, T6)>
    Await<T1, T2, T3, T4, T5, T6>(
      this (
        Task<T1>,
        Task<T2>,
        Task<T3>,
        Task<T4>,
        Task<T5>,
        Task<T6>) @this) =>
    (await @this.Item1,
     await @this.Item2,
     await @this.Item3,
     await @this.Item4,
     await @this.Item5,
     await @this.Item6);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<(T1, T2, T3, T4, T5, T6, T7)>
      Await<T1, T2, T3, T4, T5, T6, T7>(
      this (
        Task<T1>,
        Task<T2>,
        Task<T3>,
        Task<T4>,
        Task<T5>,
        Task<T6>,
        Task<T7>) @this) =>
    (await @this.Item1,
     await @this.Item2,
     await @this.Item3,
     await @this.Item4,
     await @this.Item5,
     await @this.Item6,
     await @this.Item7);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8)>
        Await<T1, T2, T3, T4, T5, T6, T7, T8>(
      this (
        Task<T1>,
        Task<T2>,
        Task<T3>,
        Task<T4>,
        Task<T5>,
        Task<T6>,
        Task<T7>,
        Task<T8>) @this) =>
    (await @this.Item1,
     await @this.Item2,
     await @this.Item3,
     await @this.Item4,
     await @this.Item5,
     await @this.Item6,
     await @this.Item7,
     await @this.Item8);
}
