using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreateCenterIndex
{
  public static ISchemaBuilder CreateCenterMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<CenterIndex>(
      table => table
        .Column<string>("UserId", column => column.WithLength(50)));
}
