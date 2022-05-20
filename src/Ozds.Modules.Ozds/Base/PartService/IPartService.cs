using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Admin;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Handlers;
using System.ComponentModel.DataAnnotations;

namespace Ozds.Modules.Ozds;

public interface IPartService<T>
{
  Action<T>? GetEditModel(T part, BuildPartEditorContext context);

  Task InitializingAsync(T part);
  IAsyncEnumerable<ValidationResult> ValidateAsync(T part);
  Task PublishedAsync(T instance, PublishContentContext context);
  Task UpdatedAsync<TPart>(UpdateContentContext context, T instance);
}

public static class PartServiceExtensions
{
  public static void UsePartService<TPart, TService>(
      this IServiceCollection services)
      where TPart : ContentPart, new()
      where TService : class, IPartService<TPart> =>
    services
      .AddScoped<TService, TService>()
      .AddContentPart<TPart>()
      .AddHandler<PartServiceHandler<TPart, TService>>()
      .UseDisplayDriver<PartServiceDisplayDriver<TPart, TService>>();
}

public abstract class PartService<T> : IPartService<T>
{
  public bool IsAdmin => AdminAttribute.IsApplied(HttpContext.HttpContext);

  public virtual Action<T> GetEditModel(
      T part,
      BuildPartEditorContext context) =>
    (part) => { };

  public virtual Task InitializingAsync(T part) => Task.CompletedTask;

  public virtual Task PublishedAsync(
      T instance,
      PublishContentContext context) =>
    Task.CompletedTask;

  public virtual IAsyncEnumerable<ValidationResult> ValidateAsync(T part) =>
    Validate(part).ToAsyncEnumerable();

  public virtual IEnumerable<ValidationResult> Validate(T part) =>
    Array.Empty<ValidationResult>();

  public virtual Task UpdatedAsync<TPart>(
      UpdateContentContext context,
      T instance) =>
    Task.CompletedTask;

  public PartService(IHttpContextAccessor httpContext)
  {
    HttpContext = httpContext;
  }

  private IHttpContextAccessor HttpContext { get; }
}
