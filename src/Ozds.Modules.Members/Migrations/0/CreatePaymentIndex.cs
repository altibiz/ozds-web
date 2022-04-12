using System;
using YesSql.Sql;
using Ozds.Modules.Members.Payments;

namespace Ozds.Modules.Members.M0;

public static partial class CreatePaymentIndexClass {
  public static void CreatePaymentIndex(this ISchemaBuilder schema) =>
      schema.CreateMapIndexTable<PaymentIndex>(
          table => table.Column<DateTime>(nameof(PaymentIndex.Date))
                       .Column<decimal?>(nameof(PaymentIndex.Amount))
                       .Column<string>(nameof(PaymentIndex.ContentItemId),
                           c => c.WithLength(50))
                       .Column<string>(nameof(PaymentIndex.PersonContentItemId),
                           c => c.WithLength(50))
                       .Column<string>(nameof(PaymentIndex.PayerName),
                           c => c.WithLength(255))
                       .Column<string>(nameof(PaymentIndex.Address),
                           c => c.WithLength(255)));
}
