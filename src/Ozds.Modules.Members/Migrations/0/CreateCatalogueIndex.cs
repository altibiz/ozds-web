using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreateCatalogueIndex
{
  public static ISchemaBuilder CreateCatalogueMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<CatalogueIndex>(
      table => table
        .Column<string>("CatalogueId", column => column.WithLength(50)));
}
