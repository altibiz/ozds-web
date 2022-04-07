using YesSql.Sql;
using System;
using Members.Payments;

namespace Members.M0;

public static class CreatePaymentByDayIndexClass
{
  public static void CreatePaymentByDayIndex(this ISchemaBuilder schema) =>
      schema.CreateReduceIndexTable<PaymentByDayIndex>(
          table => table.Column<DateTime>(nameof(PaymentByDayIndex.Date))
                       .Column<decimal>(nameof(PaymentByDayIndex.PayIn))
                       .Column<int>(nameof(PaymentByDayIndex.CountIn))
                       .Column<decimal>(nameof(PaymentByDayIndex.PayOut))
                       .Column<int>(nameof(PaymentByDayIndex.CountOut)));
}
