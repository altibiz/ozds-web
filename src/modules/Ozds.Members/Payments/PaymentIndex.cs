using OrchardCore.ContentManagement;
using YesSql.Indexes;
using System;
using System.Linq;
using Ozds.Members.Utils;

namespace Ozds.Members.Payments {
  public class PaymentIndex : MapIndex {
    public string ContentItemId { get; set; }

    public string PersonContentItemId { get; set; }

    public decimal? Amount { get; set; }

    public string PayerName { get; set; }

    public DateTime? Date { get; set; }

    public string Address { get; set; }

    public bool IsPayout { get; set; }

    public bool Published { get; set; }

    public string TransactionRef { get; set; }
  }

  public class PaymentIndexProvider : IndexProvider<ContentItem> {
    public override void Describe(DescribeContext<ContentItem> context) {
      context.For<PaymentIndex>().Map(contentItem => {
        var pp = contentItem.AsReal<Payment>();
        if (pp == null)
          return null;
        return new PaymentIndex {
          ContentItemId = contentItem.ContentItemId,
          Amount = pp.Amount.Value,
          Date = pp.Date.Value,
          PersonContentItemId = pp.Person.ContentItemIds?.FirstOrDefault(),
          PayerName = pp.PayerName.Text,
          Address = pp.Address?.Text?.Length > 255
                        ? pp.Address?.Text?.Substring(0, 255)
                        : pp.Address?.Text,
          IsPayout = pp.IsPayout?.Value ?? false,
          Published = contentItem.Published,
#pragma warning disable 0618
          TransactionRef = pp.TransactionRef ?? pp.PaymentRef?.Text,
#pragma warning restore 0618
        };
      });
    }
  }
}
