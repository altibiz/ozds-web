using OrchardCore.ContentManagement;
using YesSql.Indexes;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class PersonPartIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public bool Published { get; init; } = default!;
  public string Oib { get; init; } = default!;
  public string LegalName { get; init; } = default!;
  public bool Legal { get; init; } = default!;
}

public class PersonPartIndexProvider : IndexProvider<ContentItem>,
                                       IScopedIndexProvider
{
  public override void Describe(DescribeContext<ContentItem> context) =>
    context
      .For<PersonPartIndex>()
      .Map(contentItem => contentItem
        .AsReal<PersonPart>()
        .When(person =>
          new PersonPartIndex
          {
            ContentItemId = contentItem.ContentItemId,
            Published = contentItem.Published,
            Oib = person.Oib.Text,
            LegalName = person.LegalName,
            Legal = person.Legal,
          })
        // NOTE: this is mandatory for Yessql
        .NonNullable());

  public PersonPartIndexProvider(
      IServiceProvider services,
      IContentDefinitionManager content)
  {
    Services = services;
    Content = content;
  }

  private IServiceProvider Services;
  private IContentDefinitionManager Content;
}
