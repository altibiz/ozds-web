namespace Ozds.Modules.Members;

public static partial class TaskExtensions
{
  public static async Task Flatten(
      this Task<Task> @this) => await await @this;

  public static async Task Flatten(
      this Task<ValueTask> @this) => await await @this;

  public static async Task Flatten(
      this ValueTask<Task> @this) => await await @this;

  public static async Task Flatten(
      this ValueTask<ValueTask> @this) => await await @this;

  public static async Task<TOut> Flatten<TOut>(
      this Task<Task<TOut>> @this) => await await @this;

  public static async Task<TOut> Flatten<TOut>(
      this Task<ValueTask<TOut>> @this) => await await @this;

  public static async Task<TOut> Flatten<TOut>(
      this ValueTask<Task<TOut>> @this) => await await @this;

  public static async Task<TOut> Flatten<TOut>(
      this ValueTask<ValueTask<TOut>> @this) => await await @this;

  public static async ValueTask FlattenToValue(
      this Task<Task> @this) => await await @this;

  public static async ValueTask FlattenToValue(
      this Task<ValueTask> @this) => await await @this;

  public static async ValueTask FlattenToValue(
      this ValueTask<Task> @this) => await await @this;

  public static async ValueTask FlattenToValue(
      this ValueTask<ValueTask> @this) => await await @this;

  public static async ValueTask<TOut> FlattenToValue<TOut>(
      this Task<Task<TOut>> @this) => await await @this;

  public static async ValueTask<TOut> FlattenToValue<TOut>(
      this Task<ValueTask<TOut>> @this) => await await @this;

  public static async ValueTask<TOut> FlattenToValue<TOut>(
      this ValueTask<Task<TOut>> @this) => await await @this;

  public static async ValueTask<TOut> FlattenToValue<TOut>(
      this ValueTask<ValueTask<TOut>> @this) => await await @this;
}
