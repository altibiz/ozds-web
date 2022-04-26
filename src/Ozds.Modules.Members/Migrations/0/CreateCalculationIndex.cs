using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreateCalculationIndex
{
  public static ISchemaBuilder CreateCalculationMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<CalculationIndex>(
      table => table
        .Column<string>("ContentItemId", column => column.WithLength(50))
        .Column<string>("OfficialContentItemId", column => column.WithLength(50))
        .Column<string>("SiteContentItemId", column => column.WithLength(50))
        .Column<string>("DeviceId", column => column.WithLength(50)));
}
