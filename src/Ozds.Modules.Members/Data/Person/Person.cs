using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Ozds.Modules.Members;

public class Person : ContentPart
{
  public TextField Name { get; set; } = new();
  public TextField Oib { get; set; } = new();
  public TextField Address { get; set; } = new();
  public TextField City { get; set; } = new();
  public TextField PostalCode { get; set; } = new();
  public TextField Contact { get; set; } = new();
}
