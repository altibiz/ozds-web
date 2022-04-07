using YesSql.Sql;
using Members.Persons;

namespace Members.M0;

public static class CreatePersonPartIndexClass {
  public static void CreatePersonPartIndex(this ISchemaBuilder schema) =>
      schema.CreateMapIndexTable<PersonPartIndex>(
          table => table.Column<string>("Oib", col => col.WithLength(50))
                       .Column<string>("ContentItemId", c => c.WithLength(50))
                       .Column<string>("LegalName", c => c.WithLength(255))
                       .Column<string>("PersonType", c => c.WithLength(50))
                       .Column<decimal?>("Revenue2019")
                       .Column<decimal?>("Employees")
                       .Column<decimal?>("Associates")
                       .Column<bool>("Published"));
}
