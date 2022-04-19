namespace Ozds.Util;

public static partial class ObjectExtensions
{
  public static async Task<TOut?> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, TOut> selector) =>
    !@this.Truthy() ? default :
    !await predicate(@this) ? default :
    selector(@this);

  public static async Task<TOut> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, TOut> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    !await predicate(@this) ? @default :
    selector(@this);

  public static async Task<TOut> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, TOut> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    !await predicate(@this) ? @default() :
    selector(@this);

  public static async Task<TOut?> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, Task<TOut>> selector) =>
    !@this.Truthy() ? default :
    !await predicate(@this) ? default :
    await selector(@this);

  public static async Task<TOut> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, Task<TOut>> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    !await predicate(@this) ? @default :
    await selector(@this);

  public static async Task<TOut> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, Task<TOut>> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    !await predicate(@this) ? @default() :
    await selector(@this);

  public static async Task<TOut> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, Task<TOut>> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !await predicate(@this) ? await @default() :
    await selector(@this);

  public static async Task<TOut> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, Task<TOut>> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !await predicate(@this) ? await @default() :
    await selector(@this);

  public static async ValueTask<TOut?> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, ValueTask<TOut>> selector) =>
    !@this.Truthy() ? default :
    !await predicate(@this) ? default :
    await selector(@this);

  public static async ValueTask<TOut> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, ValueTask<TOut>> selector,
      TOut @default) =>
    !@this.Truthy() ? @default :
    !await predicate(@this) ? @default :
    await selector(@this);

  public static async ValueTask<TOut> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, ValueTask<TOut>> selector,
      Func<TOut> @default) =>
    !@this.Truthy() ? @default() :
    !await predicate(@this) ? @default() :
    await selector(@this);

  public static async ValueTask<TOut> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, ValueTask<TOut>> selector,
      Func<Task<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !await predicate(@this) ? await @default() :
    await selector(@this);

  public static async ValueTask<TOut> FinallyWhen<TIn, TOut>(
      this TIn? @this,
      Func<TIn, Task<bool>> predicate,
      Func<TIn, ValueTask<TOut>> selector,
      Func<ValueTask<TOut>> @default) =>
    !@this.Truthy() ? await @default() :
    !await predicate(@this) ? await @default() :
    await selector(@this);
}
