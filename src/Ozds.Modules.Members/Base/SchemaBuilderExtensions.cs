﻿using Dapper;
using YesSql;
using YesSql.Sql;
using YesSql.Sql.Schema;

namespace Ozds.Modules.Members.Base;

public static class SchemaBuilderExtensions
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
      .When(sql => schemaBuilder.Connection
        .Execute(sql.FirstOrDefault(), null, schemaBuilder.Transaction));
}
