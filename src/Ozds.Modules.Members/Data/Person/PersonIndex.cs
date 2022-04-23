using OrchardCore.ContentManagement;
using YesSql.Indexes;
using OrchardCore.Data;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class PersonIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public bool Published { get; init; } = default!;
  public string Oib { get; init; } = default!;
  public string LegalName { get; init; } = default!;
  public bool Legal { get; init; } = default!;
}

public class PersonIndexProvider : IndexProvider<ContentItem>,
                                       IScopedIndexProvider
{
  public override void Describe(DescribeContext<ContentItem> context) =>
    context
      .For<PersonIndex>()
      .Map(contentItem => contentItem.AsReal<Person>()
        .WhenNonNullable(person =>
          new PersonIndex
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
