using YesSql.Sql;
using Members.Base;
using Members.Persons;

namespace Members.M0;

public static class AlterPersonPartIndexClass
{
  public static void AlterPersonPartIndex(this ISchemaBuilder schema)
  {
    schema.AlterIndexTable<PersonPartIndex>(table =>
    {
      table.CreateIndex("IDX_PersonPartIndex_DocumentId", "DocumentId", "Oib",
          "ContentItemId");
    });

    // FIX: SQLite Error 1: 'near ".": syntax error'.
    // schema.ExecuteSql(
    //     "UPDATE ppi SET ppi.Published=cii.Published FROM PersonPartIndex ppi
    //     INNER JOIN " + "ContentItemIndex cii ON
    //     ppi.DocumentId=cii.DocumentId");
  }
}
