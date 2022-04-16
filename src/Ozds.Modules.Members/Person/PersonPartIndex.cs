using OrchardCore.ContentManagement;
using YesSql.Indexes;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement.Metadata;
using Ozds.Modules.Members.Utils;
using OrchardCore.Data;

namespace Ozds.Modules.Members.Persons
{
  public class PersonPartIndex : MapIndex
  {
    public string ContentItemId { get; init; } = default!;
    public string Oib { get; init; } = default!;
    public string LegalName { get; init; } = default!;
    public string PersonType { get; init; } = default!;
    public bool Published { get; init; } = default!;
  }

  public class PersonPartIndexProvider : IndexProvider<ContentItem>,
                                         IScopedIndexProvider
  {
    private IServiceProvider Services;
    private IContentDefinitionManager Content;

    public PersonPartIndexProvider(
        IServiceProvider services, IContentDefinitionManager content)
    {
      Services = services;
      Content = content;
    }

    public override void Describe(DescribeContext<ContentItem> context)
    {
      context.For<PersonPartIndex>().Map(contentItem =>
      {
        var person = contentItem.AsReal<PersonPart>();
        if (person is null)
        {
          return null!;
        }

        Content ??= Services.GetRequiredService<IContentDefinitionManager>();
        var typeDef = Content.GetSettings<PersonPartSettings>(person);
        return new PersonPartIndex
        {
          ContentItemId = contentItem.ContentItemId,
          Oib = person.Oib.Text,
          LegalName = person.LegalName,
          PersonType = typeDef.Type?.ToString(),
          Published = contentItem.Published,
        };
      });
    }
  }
}
