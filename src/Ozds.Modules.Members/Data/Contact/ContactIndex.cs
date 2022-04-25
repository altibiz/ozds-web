using OrchardCore.ContentManagement;
using YesSql.Indexes;
using OrchardCore.Data;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ContactIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
}

public class ContactIndexProvider : IndexProvider<ContentItem>,
                                       IScopedIndexProvider
{
  public override void Describe(DescribeContext<ContentItem> context) =>
    context
      .For<ContactIndex>()
      .Map(contentItem => contentItem.As<Contact>()
        .WhenNonNullable(person =>
          new ContactIndex
          {
            ContentItemId = contentItem.ContentItemId,
          })
        // NOTE: this is okay because YesSql expects null values
        .NonNullable());
}
