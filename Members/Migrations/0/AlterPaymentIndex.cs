using YesSql.Sql;
using Members.Base;
using Members.Payments;

namespace Members.M0;

public static partial class AlterPaymentIndexClass
{
  public static void AlterPaymentIndex(this ISchemaBuilder SchemaBuilder) =>
      SchemaBuilder
          .AlterIndexTable<PaymentIndex>(table =>
          {
            table.AddColumn<bool>("Published");
            table.AddColumn<string>("TransactionRef", c => c.WithLength(50));
            table.AddColumn<bool>("IsPayout");
          })
          .ExecuteSql(
              "UPDATE PaymentIndex SET Published=(SELECT Published FROM ContentItemIndex WHERE PaymentIndex.DocumentId=ContentItemIndex.DocumentId)");
}
