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
    public override void Describe(DescribeContext<ContentItem> context) =>
      context
        .For<PersonPartIndex>()
        .Map(contentItem => contentItem
          .AsReal<PersonPart>()
          .When(person =>
          {
            Content ??= Services.GetRequiredService<IContentDefinitionManager>();
            var typeDefinition = Content.GetSettings<PersonPartSettings>(person);

            return new PersonPartIndex
            {
              ContentItemId = contentItem.ContentItemId,
              Oib = person.Oib.Text,
              LegalName = person.LegalName,
              PersonType =
                typeDefinition.When(
                  typeDefinition => typeDefinition.Type.ToString(),
                  PersonType.Natural.ToString()),
              Published = contentItem.Published,
            };
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
}
