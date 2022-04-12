using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Ozds.Users.Payments
{
  public class BankStatPart : ContentPart
  {
    public string StatementJson { get; set; }
    public DateField Date { get; set; }
  }
}