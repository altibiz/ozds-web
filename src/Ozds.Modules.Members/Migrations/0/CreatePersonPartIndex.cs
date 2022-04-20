using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreatePersonPartIndex
{
  public static void CreatePersonPartMapTable(this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<PersonPartIndex>(
      table => table
        .Column<bool>("Published")
        .Column<string>("ContentItemId", c => c.WithLength(50))
        .Column<string>("Oib", col => col.WithLength(50))
        .Column<string>("LegalName", c => c.WithLength(255))
        .Column<bool>("Legal"));


  public static void CreatePersonPartMapIndex(this ISchemaBuilder schema) =>
    schema.AlterIndexTable<PersonPartIndex>(
      table => table
        .CreateIndex(
          "IDX_PersonPartIndex_DocumentId",
          "DocumentId",
          "Oib",
          "ContentItemId"));
}
