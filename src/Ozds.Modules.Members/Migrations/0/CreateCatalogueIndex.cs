using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreateCatalogueIndex
{
  public static ISchemaBuilder CreateCatalogueMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<CatalogueIndex>(
      table => table
        .Column<string>("ContentItemId", column => column.WithLength(50)));
}
