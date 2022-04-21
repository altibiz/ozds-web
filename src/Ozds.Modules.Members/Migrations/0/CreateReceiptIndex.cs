using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static partial class CreateReceiptIndex
{
  public static ISchemaBuilder CreateReceiptMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<ReceiptIndex>(
      table => table
        .Column<string>("OfficialId", column => column.WithLength(50))
        .Column<string>("ReceiptDocumentId", column => column.WithLength(50))
        .Column<string>("Partner", column => column.WithLength(255)));
}
