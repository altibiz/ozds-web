using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Users.Core
{
  public class Company : ContentPart
  {
    public TextField AuthorizedRep { get; set; }
    public TaxonomyField RepRole { get; set; }
    public NumericField Revenue2019 { get; set; }
    public NumericField EmployeeNumber { get; set; }
    public TaxonomyField OrganisationType { get; set; }
    public TaxonomyField Activity { get; set; }
    public NumericField PermanentAssociates { get; set; }
    public MediaField Logo { get; set; }
  }
}
