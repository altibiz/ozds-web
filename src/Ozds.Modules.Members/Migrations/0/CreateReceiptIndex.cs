using YesSql.Sql;

namespace Ozds.Modules.Members.M0;

public static class CreateReceiptIndex
{
  public static ISchemaBuilder CreateReceiptMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<ReceiptIndex>(
      table => table
        .Column<string>("ContentItemId", column => column.WithLength(30))
        .Column<string>("SiteContentItemId", column => column.WithLength(30))
        .Column<string>("TariffModelTermId", column => column.WithLength(30))
        .Column<string>("ConsumerName", column => column.WithLength(250))
        .Column<string>("ConsumerOib", column => column.WithLength(30))
        .Column<string>("CenterOwnerName", column => column.WithLength(250))
        .Column<string>("CenterOwnerOib", column => column.WithLength(30)));
}
