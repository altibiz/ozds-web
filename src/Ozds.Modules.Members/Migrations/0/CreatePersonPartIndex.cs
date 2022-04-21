using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreatePersonPartIndex
{
  public static ISchemaBuilder CreatePersonPartMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<PersonPartIndex>(
      table => table
        .Column<string>("ContentItemId", column => column.WithLength(50))
        .Column<bool>("Published")
        .Column<string>("Oib", column => column.WithLength(50))
        .Column<string>("LegalName", column => column.WithLength(255))
        .Column<bool>("Legal"));

  public static ISchemaBuilder CreatePersonPartMapIndex(
      this ISchemaBuilder schema) =>
    schema.AlterIndexTable<PersonPartIndex>(
      table => table
        .CreateIndex(
          "IDX_PersonPartIndex_DocumentId",
          "DocumentId",
          "Oib",
          "ContentItemId"));
}
