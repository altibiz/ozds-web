using OrchardCore.ContentManagement;
using YesSql.Indexes;
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
      .Map(contentItem => contentItem.AsReal<PersonPart>()
        .WhenNonNullable(person =>
          new PersonPartIndex
          {
            ContentItemId = contentItem.ContentItemId,
            Published = contentItem.Published,
            Oib = person.Oib.Text,
            LegalName = person.LegalName,
            Legal = person.Legal,
          })
        // NOTE: this is okay because YesSql expects null values
        .NonNullable());
}
