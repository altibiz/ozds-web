namespace Ozds.Modules.Members;

public static partial class ObjectExtensions
{
  public static T? When<T>(this T? @this, Action @do)
  {
    if (!@this.Truthy())
    {
      return default;
    }
    @do();
    return @this;
  }

  public static T When<T>(this T? @this, Action @do, T @default)
  {
    if (!@this.Truthy())
    {
      return @default;
    }
    @do();
    return @this;
  }

  public static T When<T>(this T? @this, Action @do, Func<T> @default)
  {
    if (!@this.Truthy())
    {
      return @default();
    }
    @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Action @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Action @do, Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    @do();
    return @this;
  }

  public static async Task<T?> When<T>(this T? @this, Func<Task> @do)
  {
    if (!@this.Truthy())
    {
      return default;
    }
    await @do();
    return @this;
  }

  public static async Task<T> When<T>(this T? @this, Func<Task> @do, T @default)
  {
    if (!@this.Truthy())
    {
      return @default;
    }
    await @do();
    return @this;
  }

  public static async Task<T> When<T>(
      this T? @this, Func<Task> @do, Func<T> @default)
  {
    if (!@this.Truthy())
    {
      return @default();
    }
    await @do();
    return @this;
  }

  public static async Task<T> When<T>(
      this T? @this, Func<Task> @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    await @do();
    return @this;
  }

  public static async ValueTask<T?> When<T>(this T? @this, Func<ValueTask> @do)
  {
    if (!@this.Truthy())
    {
      return default;
    }
    await @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Func<ValueTask> @do, T @default)
  {
    if (!@this.Truthy())
    {
      return @default;
    }
    await @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Func<ValueTask> @do, Func<T> @default)
  {
    if (!@this.Truthy())
    {
      return @default();
    }
    await @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Func<ValueTask> @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    await @do();
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Func<ValueTask> @do, Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    await @do();
    return @this;
  }

  public static T? When<T>(this T? @this, Action<T> @do)
  {
    if (!@this.Truthy())
    {
      return default;
    }
    @do(@this);
    return @this;
  }

  public static T When<T>(this T? @this, Action<T> @do, T @default)
  {
    if (!@this.Truthy())
    {
      return @default;
    }
    @do(@this);
    return @this;
  }

  public static T When<T>(this T? @this, Action<T> @do, Func<T> @default)
  {
    if (!@this.Truthy())
    {
      return @default();
    }
    @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Action<T> @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Action<T> @do, Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    @do(@this);
    return @this;
  }

  public static async Task<T?> When<T>(this T? @this, Func<T, Task> @do)
  {
    if (!@this.Truthy())
    {
      return default;
    }
    await @do(@this);
    return @this;
  }

  public static async Task<T> When<T>(
      this T? @this, Func<T, Task> @do, T @default)
  {
    if (!@this.Truthy())
    {
      return @default;
    }
    await @do(@this);
    return @this;
  }

  public static async Task<T> When<T>(
      this T? @this, Func<T, Task> @do, Func<T> @default)
  {
    if (!@this.Truthy())
    {
      return @default();
    }
    await @do(@this);
    return @this;
  }

  public static async Task<T> When<T>(
      this T? @this, Func<T, Task> @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    await @do(@this);
    return @this;
  }

  public static async ValueTask<T?> When<T>(
      this T? @this, Func<T, ValueTask> @do)
  {
    if (!@this.Truthy())
    {
      return default;
    }
    await @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Func<T, ValueTask> @do, T @default)
  {
    if (!@this.Truthy())
    {
      return @default;
    }
    await @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Func<T, ValueTask> @do, Func<T> @default)
  {
    if (!@this.Truthy())
    {
      return @default();
    }
    await @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Func<T, ValueTask> @do, Func<Task<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    await @do(@this);
    return @this;
  }

  public static async ValueTask<T> When<T>(
      this T? @this, Func<T, ValueTask> @do, Func<ValueTask<T>> @default)
  {
    if (!@this.Truthy())
    {
      return await @default();
    }
    await @do(@this);
    return @this;
  }
}
