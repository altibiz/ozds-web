using Dapper;
using YesSql;
using YesSql.Sql;
using YesSql.Sql.Schema;

namespace Ozds.Modules.Members.Base;

public static class SchemaBuilderExtensions
{
  public static void ExecuteSql(this ISchemaBuilder schemaBuilder,
      string sql) => schemaBuilder
                         .GetFieldOrDefault<ICommandInterpreter>(
                             "_commandInterpreter",
                             System.Reflection.BindingFlags.NonPublic |
                                 System.Reflection.BindingFlags.Instance)
                         ?.CreateSql(new SqlStatementCommand(sql))
                         .SelectOrDefault(
                             rawSql => schemaBuilder.Connection.Execute(
                                 rawSql.FirstOrDefault(), null,
                                 schemaBuilder.Transaction));
}
