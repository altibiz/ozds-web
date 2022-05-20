using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;
using OrchardCore.Title.Models;
using OrchardCore.Autoroute.Models;

namespace Ozds.Modules.Ozds;

public class TariffTagType : ContentTypeBase
{
  public Lazy<TitlePart> Title { get; init; } = default!;
  public Lazy<TariffTag> TariffTag { get; init; } = default!;
  public Lazy<AutoroutePart> Autoroute { get; init; } = default!;

  private TariffTagType(ContentItem item) : base(item) { }
}

public class TariffTag : ContentPart
{
  public TextField Name { get; init; } = default!;
  public TextField Abbreviation { get; init; } = default!;
  public TextField Unit { get; init; } = default!;
}
