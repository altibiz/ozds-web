using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using OrchardCore.Flows.Models;

namespace Ozds.Modules.Ozds;

public class SecondarySiteType : ContentTypeBase
{
  public Lazy<TitlePart> Title { get; init; } = default!;
  public Lazy<SecondarySite> SecondarySite { get; init; } = default!;
  public Lazy<Site> Site { get; init; } = default!;
  public Lazy<BagPart> Catalogue { get; init; } = default!;

  private SecondarySiteType(ContentItem item) : base(item) { }
}

public class SecondarySite : ContentPart
{
}
