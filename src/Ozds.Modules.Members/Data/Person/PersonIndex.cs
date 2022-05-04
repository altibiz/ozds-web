using OrchardCore.ContentManagement;
using YesSql.Indexes;
using OrchardCore.Data;
using Ozds.Util;

namespace Ozds.Modules.Members;

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
        ((item.Get<Person>("CenterOwner"),
          item.Get<Person>("Person"))
         switch
        {
          (Person person, null) =>
           FromPerson(item, person),
          (null, Person person) =>
           FromPerson(item, person),
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
      Name = person.Name.Text
    };
}
