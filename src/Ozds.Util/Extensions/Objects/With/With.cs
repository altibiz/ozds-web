using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? With<T>(
      this T? @this,
      Action<T> action)
  {
    if (!@this.Truthy())
    {
      return @this;
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T With<T>(
      this T? @this,
      Action<T> action,
      T @default)
  {
    if (!@this.Truthy())
    {
      return @default;
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T With<T>(
      this T? @this,
      Action<T> action,
      Func<T> @default)
  {
    if (!@this.Truthy())
    {
      return @default();
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> With<T>(
      this T? @this,
      Action<T> action,
      Func<Task<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> With<T>(
      this T? @this,
      Action<T> action,
      Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> WithTask<T>(
      this T? @this,
      Func<T, Task> action)
  {
    if (!@this.Truthy())
    {
      return @this;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithTask<T>(
      this T? @this,
      Func<T, Task> action,
      T @default)
  {
    if (!@this.Truthy())
    {
      return @default;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithTask<T>(
      this T? @this,
      Func<T, Task> action,
      Func<T> @default)
  {
    if (!@this.Truthy())
    {
      return @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithTask<T>(
      this T? @this,
      Func<T, Task> action,
      Func<Task<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T?> WithValueTask<T>(
      this T? @this,
      Func<T, ValueTask> action)
  {
    if (!@this.Truthy())
    {
      return @this;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> WithValueTask<T>(
      this T? @this,
      Func<T, ValueTask> action,
      T @default)
  {
    if (!@this.Truthy())
    {
      return @default;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> WithValueTask<T>(
      this T? @this,
      Func<T, ValueTask> action,
      Func<T> @default)
  {
    if (!@this.Truthy())
    {
      return @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> WithValueTask<T>(
      this T? @this,
      Func<T, ValueTask> action,
      Func<Task<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async ValueTask<T> WithValueTask<T>(
      this T? @this,
      Func<T, ValueTask> action,
      Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    await action(@this);
    return @this;
  }
}
