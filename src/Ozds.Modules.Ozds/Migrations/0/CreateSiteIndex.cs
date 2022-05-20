using YesSql.Sql;

namespace Ozds.Modules.Ozds.M0;

public static class CreateSiteIndex
{
  public static ISchemaBuilder CreateSiteMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<SiteIndex>(
      table => table
        .Column<string>("ContentItemId", column => column.WithLength(30))
        .Column<string>("SourceTermId", column => column.WithLength(30))
        .Column<string>("DeviceId", column => column.WithLength(30))
        .Column<string>("StatusTermId", column => column.WithLength(30)));
}
