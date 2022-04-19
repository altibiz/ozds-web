namespace Ozds.Modules.Members;

public static partial class TaskExtensions
{
  public static Task<TOut> ToTask<TOut>(
      this TOut @this) =>
    Task.FromResult(@this);

  public static async Task<TOut> ToTask<TOut>(
      this ValueTask<TOut> @this) =>
    await @this;

  public static async Task ToTask(
      this ValueTask @this) =>
    await @this;

  public static ValueTask<TOut> ToValueTask<TOut>(
      this TOut @this) =>
    ValueTask.FromResult(@this);

  public static async ValueTask<TOut> ToValueTask<TOut>(
      this Task<TOut> @this) =>
    await @this;

  public static async ValueTask ToValueTask(
      this Task @this) =>
    await @this;

  public static Task<T?> Nullable<T>(
      this Task<T> @this) =>
    @this.Then(@this => @this.Nullable());

  public static ValueTask<T?> Nullable<T>(
      this ValueTask<T> @this) =>
    @this.Then(@this => @this.Nullable());
}
