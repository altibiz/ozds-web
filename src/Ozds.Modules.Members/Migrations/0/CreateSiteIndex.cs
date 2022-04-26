using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreateSiteIndex
{
  public static ISchemaBuilder CreateSiteMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<SiteIndex>(
      table => table
        .Column<string>("ContentItemId", column => column.WithLength(50))
        .Column<string>("SourceTermId", column => column.WithLength(50))
        .Column<string>("DeviceId", column => column.WithLength(50))
        .Column<decimal>("Coefficient")
        .Column<string>("PhaseTermId", column => column.WithLength(10))
        .Column<bool>("Active")
        .Column<bool>("Primary"));
}
