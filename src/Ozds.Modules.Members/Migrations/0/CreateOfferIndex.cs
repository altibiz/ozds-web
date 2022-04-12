using YesSql.Sql;
using Ozds.Modules.Members.Indexes;

namespace Ozds.Modules.Members.M0;

public static partial class CreateOfferIndexClass
{
  public static void CreateOfferIndex(this ISchemaBuilder SchemaBuilder)
  {
    SchemaBuilder.CreateMapIndexTable<OfferIndex>(
        table => table
                     .Column<string>(nameof(OfferIndex.ContentItemId),
                         c => c.WithLength(50))
                     .Column<string>(nameof(OfferIndex.CompanyContentItemId),
                         c => c.WithLength(50))
                     .Column<string>(
                         nameof(OfferIndex.Title), c => c.WithLength(225))
                     .Column<string>(
                         nameof(OfferIndex.Owner), c => c.WithLength(225))
                     .Column<bool>(nameof(OfferIndex.Published))
                     .Column<bool>(nameof(OfferIndex.Latest)));
  }
}
