using Dapper;
using YesSql;
using YesSql.Sql;
using YesSql.Sql.Schema;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public static class ISchemaBuilderExtensions
{
  public static void ExecuteSql(
      this ISchemaBuilder schemaBuilder,
      string sql) =>
    schemaBuilder
      .Get<ICommandInterpreter>(
        "_commandInterpreter",
        System.Reflection.BindingFlags.NonPublic |
        System.Reflection.BindingFlags.Instance)
      ?.CreateSql(new SqlStatementCommand(sql))
      .WhenNonNull(sql => schemaBuilder.Connection
        .Execute(sql.FirstOrDefault(), null, schemaBuilder.Transaction));
}
