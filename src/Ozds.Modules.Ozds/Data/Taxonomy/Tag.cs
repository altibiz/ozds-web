using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using OrchardCore.Autoroute.Models;

namespace Ozds.Modules.Ozds;

public class TagType : ContentTypeBase
{
  public Lazy<TitlePart> Title { get; init; } = default!;
  public Lazy<Tag> Tag { get; init; } = default!;
  public Lazy<AutoroutePart> Autoroute { get; init; } = default!;

  private TagType(ContentItem item) : base(item) { }
}

public class Tag : ContentPart
{
}
