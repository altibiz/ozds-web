using OrchardCore.ContentManagement;
using YesSql.Indexes;
using System;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement.Metadata;
using Ozds.Users.Utils;
using OrchardCore.Data;
using Ozds.Users.Core;

namespace Ozds.Users.Persons
{
  public class PersonPartIndex : MapIndex
  {
    public string ContentItemId { get; set; }
    public string Oib { get; set; }
    public string LegalName { get; set; }
    public decimal? Revenue2019 { get; set; }
    public string PersonType { get; set; }
    public decimal? Employees { get; set; }
    public decimal? Associates { get; set; }
    public bool Published { get; set; }
  }

  public class PersonPartIndexProvider : IndexProvider<ContentItem>,
                                         IScopedIndexProvider
  {
    private IServiceProvider _serviceProvider;
    private IContentDefinitionManager contentDefinitionManager;

    public PersonPartIndexProvider(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public override void Describe(DescribeContext<ContentItem> context)
    {
      context.For<PersonPartIndex>().Map(contentItem =>
      {
        var pp = contentItem.AsReal<PersonPart>();
        if (pp == null)
          return null;
        // Lazy initialization because of ISession cyclic dependency
        contentDefinitionManager ??=
            _serviceProvider.GetRequiredService<IContentDefinitionManager>();
        var typeDef =
            contentDefinitionManager.GetSettings<PersonPartSettings>(pp);
        var res = new PersonPartIndex
        {
          ContentItemId = contentItem.ContentItemId,
          Oib = pp.Oib.Text,
          LegalName = pp.LegalName,
          PersonType = typeDef.Type?.ToString(),
          Published = contentItem.Published,
        };

        var company = contentItem.AsReal<Company>();

        if (company != null)
        {
          res.Revenue2019 = company.Revenue2019?.Value;
          res.Employees = company.EmployeeNumber?.Value;
          res.Associates = company.PermanentAssociates?.Value;
        }

        return res;
      });
    }
  }
}
