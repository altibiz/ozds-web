﻿using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class PartServiceHandler<TPart, TService> : ContentPartHandler<TPart>
    where TPart : ContentPart, new()
    where TService : IPartService<TPart>
{
  public override Task ValidatingAsync(
      ValidateContentContext context,
      TPart part) =>
    AsyncEnumerable.ForEachAsync(Service
      .ValidateAsync(part)
, item => context.Fail(item));

  public override Task InitializingAsync(
      InitializingContentContext context,
      TPart instance) =>
    Service
      .InitializingAsync(instance)
      .Then(() => context.ContentItem.Apply(instance));

  public override Task PublishedAsync(
      PublishContentContext context,
      TPart instance) =>
    Service.PublishedAsync(instance, context);

  public override Task UpdatedAsync(
      UpdateContentContext context,
      TPart instance) =>
    Service
      .UpdatedAsync<TPart>(context, instance)
      .Then(() => instance.ContentItem.Apply(instance));

  public PartServiceHandler(TService service)
  {
    Service = service;
  }

  private TService Service { get; }
}
