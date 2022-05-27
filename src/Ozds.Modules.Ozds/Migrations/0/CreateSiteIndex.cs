using YesSql.Sql;

namespace Ozds.Modules.Ozds.M0;

public static class CreateSiteIndex
{
  public static ISchemaBuilder CreateSiteMapTable(
      this ISchemaBuilder schema) =>
    schema.CreateMapIndexTable<SiteIndex>(
      table => table
        .Column<string>(
          "ContentItemId",
          column => column
            .WithLength(30))

        .Column<string>(
          "SourceTermId",
          column => column
            .WithLength(30))

        .Column<string>(
          "DeviceId",
          column => column
            .WithLength(30))

        .Column<string>(
          "StatusTermId",
          column => column
            .WithLength(30))

        .Column<string>(
          "OperatorName",
          column => column
            .WithLength(250))

        .Column<string>(
          "OperatorOib",
          column => column
            .WithLength(30))

        .Column<string>(
          "CenterContentItemId",
          column => column
            .WithLength(30))

        .Column<string>(
          "CenterUserId",
          column => column
            .WithLength(30))

        .Column<string>(
          "CenterOwnerName",
          column => column
            .WithLength(250))

        .Column<string>(
          "CenterOwnerOib",
          column => column
            .WithLength(30))

        .Column<string>(
          "OwnerContentItemId",
          column => column
            .WithLength(30))

        .Column<string>(
          "OwnerUserId",
          column => column
            .WithLength(30))

        .Column<string>(
          "OwnerName",
          column => column
            .WithLength(250))

        .Column<string>(
          "OwnerOib",
          column => column
            .WithLength(30)));
}
