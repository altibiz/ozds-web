using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Contents.Services;
using YesSql;
using YesSql.Filters.Query;
using YesSql.Services;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class PersonPartAdminListFilterProvider
    : IContentsAdminListFilterProvider
{
  public void Build(QueryEngineBuilder<ContentItem> builder) =>
    builder
      .WithNamedTerm(
        "oib",
        builder => builder.OneCondition(
          (val, query) =>
            val
              .When(val =>
                query
                  .With<PersonPartIndex>(
                    i => i.Oib == val))
              .Return(query)))
      .WithDefaultTerm(
        "text",
        builder => builder.ManyCondition(
          (value, query, context) =>
            Task
              .Run<IQuery<ContentItem>?>(() =>
                context
                  .As<ContentQueryContext>()
                  .When(content => content.ServiceProvider
                    .GetRequiredService<IHttpContextAccessor>()
                    .HttpContext
                    .When(httpContext => httpContext
                      .Request
                      .RouteValues
                      .GetOrDefault("contentTypeId")
                      .When(selectedContentType =>
                        selectedContentType.ToString() == "Member" ||
                        selectedContentType.ToString() == "Company",
                        _ => query
                          .With<PersonPartIndex>(
                            person =>
                              person.Oib == value ||
                              person.LegalName.Contains(value))
                          .As<IQuery<ContentItem>>(),
                        () => query
                          .With<ContentItemIndex>(
                            item => item.DisplayText.Contains(value))
                          .As<IQuery<ContentItem>>()))))
              .ToValueTask(),
          (value, query, context) =>
            Task
              .Run<IQuery<ContentItem>?>(() =>
                query
                  .With<ContentItemIndex>(
                    index => index.DisplayText.IsNotIn<ContentItemIndex>(
                        index => index.DisplayText,
                        index => index.DisplayText.Contains(value)))
                  .As<IQuery<ContentItem>>())
              .ToValueTask()));
}
