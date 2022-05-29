using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? WhenWith<T>(
      this T? @this,
      Predicate<T> predicate,
      Action<T> action)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @this;
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WhenWith<T>(
      this T? @this,
      Predicate<T> predicate,
      Action<T> action,
      T @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default;
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WhenWith<T>(
      this T? @this,
      Predicate<T> predicate,
      Action<T> action,
      Func<T> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default();
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> WhenWith<T>(
      this T? @this,
      Predicate<T> predicate,
      Action<T> action,
      Func<Task<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> WhenWith<T>(
      this T? @this,
      Predicate<T> predicate,
      Action<T> action,
      Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> WhenWithTask<T>(
      this T? @this,
      Predicate<T> predicate,
      Func<T, Task> action)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @this;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenWithTask<T>(
      this T? @this,
      Predicate<T> predicate,
      Func<T, Task> action,
      T @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenWithTask<T>(
      this T? @this,
      Predicate<T> predicate,
      Func<T, Task> action,
      Func<T> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenWithTask<T>(
      this T? @this,
      Predicate<T> predicate,
      Func<T, Task> action,
      Func<Task<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WhenWithTask<T>(
      this T? @this,
      Predicate<T> predicate,
      Func<T, Task> action,
      Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T?> WhenWithValueTask<T>(
      this T? @this,
      Predicate<T> predicate,
      Func<T, ValueTask> action)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @this;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> WhenWithValueTask<T>(
      this T? @this,
      Predicate<T> predicate,
      Func<T, ValueTask> action,
      T @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> WhenWithValueTask<T>(
      this T? @this,
      Predicate<T> predicate,
      Func<T, ValueTask> action,
      Func<T> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> WhenWithValueTask<T>(
      this T? @this,
      Predicate<T> predicate,
      Func<T, ValueTask> action,
      Func<Task<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> WhenWithValueTask<T>(
      this T? @this,
      Predicate<T> predicate,
      Func<T, ValueTask> action,
      Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    await action(@this);
    return @this;
  }
}
