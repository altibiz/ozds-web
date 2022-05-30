using System.Runtime.CompilerServices;

namespace Ozds.Extensions;

public static partial class Objects
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? WithNonNull<T>(
      this T? @this,
      Action<T> action)
  {
    if (@this is null)
    {
      return @this;
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WithNonNull<T>(
      this T? @this,
      Action<T> action,
      T @default)
  {
    if (@this is null)
    {
      return @default;
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WithNonNull<T>(
      this T? @this,
      Action<T> action,
      Func<T> @default)
  {
    if (@this is null)
    {
      return @default();
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNull<T>(
      this T? @this,
      Action<T> action,
      Func<Task<T>> @default)
  {
    if (@this is null)
    {
      return await @default();
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNull<T>(
      this T? @this,
      Action<T> action,
      Func<ValueTask<T>> @default)
  {
    if (@this is null)
    {
      return await @default();
    }
    action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> WithNonNullAsync<T>(
      this T? @this,
      Func<T, Task> action)
  {
    if (@this is null)
    {
      return @this;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, Task> action,
      T @default)
  {
    if (@this is null)
    {
      return @default;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, Task> action,
      Func<T> @default)
  {
    if (@this is null)
    {
      return @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, Task> action,
      Func<Task<T>> @default)
  {
    if (@this is null)
    {
      return await @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> WithNonNullAsync<T>(
      this T? @this,
      Func<T, ValueTask> action)
  {
    if (@this is null)
    {
      return @this;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, ValueTask> action,
      T @default)
  {
    if (@this is null)
    {
      return @default;
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, ValueTask> action,
      Func<T> @default)
  {
    if (@this is null)
    {
      return @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, ValueTask> action,
      Func<Task<T>> @default)
  {
    if (@this is null)
    {
      return await @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, ValueTask> action,
      Func<ValueTask<T>> @default)
  {
    if (@this is null)
    {
      return await @default();
    }
    await action(@this);
    return @this;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? WithNonNull<T>(
      this T? @this,
      Action<T> action) where T : struct
  {
    if (@this is null)
    {
      return @this;
    }
    action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WithNonNull<T>(
      this T? @this,
      Action<T> action,
      T @default) where T : struct
  {
    if (@this is null)
    {
      return @default;
    }
    action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T WithNonNull<T>(
      this T? @this,
      Action<T> action,
      Func<T> @default) where T : struct
  {
    if (@this is null)
    {
      return @default();
    }
    action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNull<T>(
      this T? @this,
      Action<T> action,
      Func<Task<T>> @default) where T : struct
  {
    if (@this is null)
    {
      return await @default();
    }
    action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNull<T>(
      this T? @this,
      Action<T> action,
      Func<ValueTask<T>> @default) where T : struct
  {
    if (@this is null)
    {
      return await @default();
    }
    action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> WithNonNullAsync<T>(
      this T? @this,
      Func<T, Task> action) where T : struct
  {
    if (@this is null)
    {
      return null;
    }
    await action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, Task> action,
      T @default) where T : struct
  {
    if (@this is null)
    {
      return @default;
    }
    await action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, Task> action,
      Func<T> @default) where T : struct
  {
    if (@this is null)
    {
      return @default();
    }
    await action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, Task> action,
      Func<Task<T>> @default) where T : struct
  {
    if (@this is null)
    {
      return await @default();
    }
    await action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T?> WithNonNullAsync<T>(
      this T? @this,
      Func<T, ValueTask> action) where T : struct
  {
    if (@this is null)
    {
      return null;
    }
    await action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, ValueTask> action,
      T @default) where T : struct
  {
    if (@this is null)
    {
      return @default;
    }
    await action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, ValueTask> action,
      Func<T> @default) where T : struct
  {
    if (@this is null)
    {
      return @default();
    }
    await action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, ValueTask> action,
      Func<Task<T>> @default) where T : struct
  {
    if (@this is null)
    {
      return await @default();
    }
    await action(@this.Value);
    return @this.Value;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static async Task<T> WithNonNullAsync<T>(
      this T? @this,
      Func<T, ValueTask> action,
      Func<ValueTask<T>> @default) where T : struct
  {
    if (@this is null)
    {
      return await @default();
    }
    await action(@this.Value);
    return @this.Value;
  }
}
