using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreateContractIndex
{
  public static ISchemaBuilder CreateContractMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<ContractIndex>(
      table => table
        .Column<string>("ContentItemId", column => column.WithLength(50))
        .Column<string>("CenterId", column => column.WithLength(50))
        .Column<string>("MemberId", column => column.WithLength(50)));
}
