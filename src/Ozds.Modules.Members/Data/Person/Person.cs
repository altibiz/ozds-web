using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using Newtonsoft.Json;

namespace Ozds.Modules.Members;

public class Person : ContentPart
{
  public TextField Name { get; set; } = new();
  public TextField Oib { get; set; } = new();
  public TextField Address { get; set; } = new();
  public TextField City { get; set; } = new();
  public TextField PostalCode { get; set; } = new();
  public TextField Contact { get; set; } = new();

  [JsonIgnore]
  public Lazy<PersonData> Data { get; }

  public Person()
  {
    Data = new Lazy<PersonData>(
      () =>
        new PersonData
        {
          Title = this.Name.Text,
          Oib = this.Oib.Text,
          Address = this.Address.Text,
          City = this.City.Text,
          PostalCode = this.PostalCode.Text,
          Contact = this.Contact.Text
        });
  }
}

public readonly record struct PersonData
(string Title,
 string Oib,
 string Address,
 string City,
 string PostalCode,
 string Contact);
