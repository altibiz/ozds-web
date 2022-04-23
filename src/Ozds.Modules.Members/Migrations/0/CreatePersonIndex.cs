using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreatePersonIndex
{
  public static ISchemaBuilder CreatePersonMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<PersonIndex>(
      table => table
        .Column<string>("ContentItemId", column => column.WithLength(50))
        .Column<bool>("Published")
        .Column<string>("Oib", column => column.WithLength(50))
        .Column<string>("LegalName", column => column.WithLength(255))
        .Column<bool>("Legal"));

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
