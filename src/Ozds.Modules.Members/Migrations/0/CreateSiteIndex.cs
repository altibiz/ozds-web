using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreateSiteIndex
{
  public static ISchemaBuilder CreateSiteMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<SiteIndex>(
      table => table
        .Column<string>("SiteId", column => column.WithLength(50))
        .Column<string>("Source", column => column.WithLength(50))
        .Column<string>("DeviceId", column => column.WithLength(50))
        .Column<decimal>("Coefficient")
        .Column<string>("Phase", column => column.WithLength(10))
        .Column<bool>("Active")
        .Column<bool>("Primary"));
}
