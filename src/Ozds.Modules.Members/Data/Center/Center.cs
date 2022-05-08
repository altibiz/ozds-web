using OrchardCore.ContentManagement;
using OrchardCore.Flows.Models;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;

namespace Ozds.Modules.Members;

public class CenterType : ContentTypeBase
{
  public Lazy<TitlePart> Title { get; init; } = default!;
  public Lazy<Center> Center { get; init; } = default!;
  public Lazy<Person> CenterOwner { get; init; } = default!;
  public Lazy<Person> Operator { get; init; } = default!;
  public Lazy<BagPart> Catalogues { get; init; } = default!;
  public Lazy<ListPart> Consumers { get; init; } = default!;

  private CenterType(ContentItem item) : base(item) { }
}

public class Center : ContentPart
{
}
