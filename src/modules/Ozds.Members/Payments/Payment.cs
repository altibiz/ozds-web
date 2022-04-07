using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using System;

namespace Ozds.Members.Payments
{
  public class Payment : ContentPart
  {
    public NumericField Amount { get; set; }
    public TextField PayerName { get; set; }
    public TextField Address { get; set; }
    public TextField ReferenceNr { get; set; }
    public DateField Date { get; set; }
    public ContentPickerField Person { get; set; }

    public TextField Description { get; set; }
    public string BankContentItemId { get; set; }

    public string TransactionRef { get; set; }

    [Obsolete(
        "Use TransactionRef, this is for initial imports and legacy projections")]
    public TextField PaymentRef
    {
      get; set;
    }

    public BooleanField IsPayout { get; set; }
  }
}
