using YesSql.Sql;

namespace Ozds.Modules.Ozds.M0;

public static class CreatePersonIndex
{
  public static ISchemaBuilder CreatePersonMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<PersonIndex>(
      table => table
        .Column<string>("ContentItemId", column => column.WithLength(30))
        .Column<string>("Name", column => column.WithLength(250))
        .Column<string>("Oib", column => column.WithLength(30))
        .Column<string?>("SiteContentItemId", column => column.WithLength(30)));

  public static ISchemaBuilder CreatePersonMapIndex(
      this ISchemaBuilder schema) =>
    schema.AlterIndexTable<PersonIndex>(
      table => table
        .CreateIndex(
          "IDX_PersonIndex_DocumentId",
          "DocumentId",
          "Oib",
          "ContentItemId"));
}
