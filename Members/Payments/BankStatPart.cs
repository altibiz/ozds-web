using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Members.Payments {
  public class BankStatPart : ContentPart {
    public string StatementJson { get; set; }
    public DateField Date { get; set; }
  }
}