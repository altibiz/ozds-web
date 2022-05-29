using OrchardCore.ContentManagement;
using YesSql.Indexes;
using OrchardCore.Data;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class PersonIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public string Oib { get; init; } = default!;
  public string Name { get; init; } = default!;
}

public class PersonIndexProvider :
  IndexProvider<ContentItem>,
  IScopedIndexProvider
{
  public override void Describe(DescribeContext<ContentItem> context) =>
    context
      .For<PersonIndex>()
      .Map(item =>
        ((item.AsContent<CenterType>(),
          item.AsContent<ConsumerType>()) switch
        {
          (CenterType center, null) =>
            FromPerson(
                item,
                center.CenterOwner.Value),
          (null, ConsumerType consumer) =>
            FromPerson(
              item,
              consumer.Person.Value),
          _ => null
        })
        // NOTE: this is okay because YesSql expects null values
        .NonNullable());

  private PersonIndex FromPerson(
      ContentItem item,
      Person person) =>
    new PersonIndex
    {
      ContentItemId = item.ContentItemId,
      Oib = person.Oib.Text,
      Name = person.Name.Text,
    };
}
