using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;
using OrchardCore.Taxonomies.Fields;

namespace Ozds.Users.Core;

public class Offer : ContentPart
{
  public TextField ShortDescription { get; set; }
  public TextField Description { get; set; }
  public HtmlField LongDescription { get; set; }
  public TextField PersonName { get; set; }
  public TextField ContactPerson { get; set; }
  public TextField Email { get; set; }
  public TextField Address { get; set; }
  public TextField Phone { get; set; }
  public LinkField Web { get; set; }
  public LinkField Instagram { get; set; }
  public LinkField Facebook { get; set; }
  public LinkField LinkedIn { get; set; }
  public TaxonomyField Category { get; set; }
  public TextField YoutubeVideoId { get; set; }
  public MediaField FeaturedImage { get; set; }
  public ContentPickerField Company { get; set; }
}
