using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreateMemberIndex
{
  public static ISchemaBuilder CreateMemberMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<MemberIndex>(
      table => table
        .Column<string>("ContentItemId", column => column.WithLength(50))
        .Column<string>("UserId", column => column.WithLength(50)));
}
