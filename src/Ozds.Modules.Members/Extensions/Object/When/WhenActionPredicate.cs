namespace Ozds.Modules.Members;

public static partial class ObjectExtensions
{
  public static T? When<T>(this T? @this, Predicate<T> predicate, Action @do)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return default;
    }
    @do();
    return @this;
  }

  public static T When<T>(
      this T? @this, Predicate<T> predicate, Action @do, T @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default;
    }
    @do();
    return @this;
  }

  public static T When<T>(
      this T? @this, Predicate<T> predicate, Action @do, Func<T> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default();
    }
    @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Predicate<T> predicate, Action @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(this T? @this,
      Predicate<T> predicate, Action @do, Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    @do();
    return @this;
  }

  public static async Task<T?> When<T>(
      this T? @this, Predicate<T> predicate, Func<Task> @do)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return default;
    }
    await @do();
    return @this;
  }

  public static async Task<T> When<T>(
      this T? @this, Predicate<T> predicate, Func<Task> @do, T @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default;
    }
    await @do();
    return @this;
  }

  public static async Task<T> When<T>(
      this T? @this, Predicate<T> predicate, Func<Task> @do, Func<T> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default();
    }
    await @do();
    return @this;
  }

  public static async Task<T> When<T>(this T? @this, Predicate<T> predicate,
      Func<Task> @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    await @do();
    return @this;
  }

  public static async ValueTask<T?> When<T>(
      this T? @this, Predicate<T> predicate, Func<ValueTask> @do)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return default;
    }
    await @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Predicate<T> predicate, Func<ValueTask> @do, T @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default;
    }
    await @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(this T? @this,
      Predicate<T> predicate, Func<ValueTask> @do, Func<T> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default();
    }
    await @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(this T? @this,
      Predicate<T> predicate, Func<ValueTask> @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    await @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(this T? @this,
      Predicate<T> predicate, Func<ValueTask> @do, Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    await @do();
    return @this;
  }

  public static T? When<T>(this T? @this, Predicate<T> predicate, Action<T> @do)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return default;
    }
    @do(@this);
    return @this;
  }

  public static T When<T>(
      this T? @this, Predicate<T> predicate, Action<T> @do, T @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default;
    }
    @do(@this);
    return @this;
  }

  public static T When<T>(
      this T? @this, Predicate<T> predicate, Action<T> @do, Func<T> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default();
    }
    @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(this T? @this,
      Predicate<T> predicate, Action<T> @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(this T? @this,
      Predicate<T> predicate, Action<T> @do, Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    @do(@this);
    return @this;
  }

  public static async Task<T?> When<T>(
      this T? @this, Predicate<T> predicate, Func<T, Task> @do)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return default;
    }
    await @do(@this);
    return @this;
  }

  public static async Task<T> When<T>(
      this T? @this, Predicate<T> predicate, Func<T, Task> @do, T @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default;
    }
    await @do(@this);
    return @this;
  }

  public static async Task<T> When<T>(this T? @this, Predicate<T> predicate,
      Func<T, Task> @do, Func<T> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default();
    }
    await @do(@this);
    return @this;
  }

  public static async Task<T> When<T>(this T? @this, Predicate<T> predicate,
      Func<T, Task> @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    await @do(@this);
    return @this;
  }

  public static async ValueTask<T?> When<T>(
      this T? @this, Predicate<T> predicate, Func<T, ValueTask> @do)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return default;
    }
    await @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Predicate<T> predicate, Func<T, ValueTask> @do, T @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default;
    }
    await @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(this T? @this,
      Predicate<T> predicate, Func<T, ValueTask> @do, Func<T> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return @default();
    }
    await @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(this T? @this,
      Predicate<T> predicate, Func<T, ValueTask> @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    await @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(this T? @this,
      Predicate<T> predicate, Func<T, ValueTask> @do,
      Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy() || !predicate(@this))
    {
      return await @default();
    }
    await @do(@this);
    return @this;
  }
}
