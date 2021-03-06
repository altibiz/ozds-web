namespace Ozds.Modules.Ozds.M0;

public static class IndexMigrations
{
  public static ISchemaBuilder CreateIndexMapTables(
      this ISchemaBuilder schema) =>
    schema
      .CreatePersonMapTable()
      .CreateReceiptMapTable()
      .CreateSiteMapTable();

  public static ISchemaBuilder CreateIndexMapIndices(
      this ISchemaBuilder schema) =>
    schema
      .CreatePersonMapIndex();
}
