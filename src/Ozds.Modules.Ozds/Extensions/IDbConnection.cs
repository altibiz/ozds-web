using Dapper;
using System.Data;
using YesSql;
using YesSql.Sql;

namespace Ozds.Modules.Ozds;

public static class IDbConnectionExtensions
{
  public static void ClearReduceIndexTable(this IDbConnection connection,
      Type indexType, IConfiguration configuration, string collection = "")
  {
    var indexTable =
        configuration.TableNameConvention.GetIndexTable(indexType, collection);
    var documentTable =
        configuration.TableNameConvention.GetDocumentTable(collection);

    var bridgeTableName = indexTable + "_" + documentTable;
    connection.Execute(
        $"DELETE FROM {configuration.SqlDialect.QuoteForTableName(configuration.TablePrefix + bridgeTableName)}");
    connection.Execute(
        $"DELETE FROM {configuration.SqlDialect.QuoteForTableName(configuration.TablePrefix + indexTable)}");
  }

  public static void ClearMapIndexTable(this IDbConnection connection,
      Type indexType, IConfiguration configuration, string collection = "")
  {
    var indexTable =
        configuration.TableNameConvention.GetIndexTable(indexType, collection);
    connection.Execute(
        $"DELETE FROM {configuration.SqlDialect.QuoteForTableName(configuration.TablePrefix + indexTable)}");
  }

  public async static Task<IEnumerable<Document>> GetContentItems(
      this IDbConnection conn, string contentItemType,
      IConfiguration configuration, string collection = "")
  {
    var sqlBuilder =
        new SqlBuilder(configuration.TablePrefix, configuration.SqlDialect);
    sqlBuilder.Select();
    sqlBuilder.Selector("dd", "*");
    sqlBuilder.Table(
        configuration.TableNameConvention.GetDocumentTable(collection), "dd");
    sqlBuilder.WhereAnd(
        " dd.Type='OrchardCore.ContentManagement.ContentItem, OrchardCore.ContentManagement.Abstractions' ");
    if (!string.IsNullOrEmpty(contentItemType))
    {
      sqlBuilder.InnerJoin(configuration.TablePrefix + "ContentItemIndex",
          "cix", "DocumentId", "dd", "Id", "cix");
      sqlBuilder.WhereAnd("ContentType='" + contentItemType + "'");
    }
    return await conn.QueryAsync<Document>(sqlBuilder.ToSqlString());
  }
}
