using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;

namespace Ozds.Modules.Ozds;

public class ConsumerType : ContentTypeBase
{
  public Lazy<TitlePart> Title { get; init; } = default!;
  public Lazy<Consumer> Consumer { get; init; } = default!;
  public Lazy<Person> Person { get; init; } = default!;
  public Lazy<ListPart> SecondarySites { get; init; } = default!;

  private ConsumerType(ContentItem item) : base(item) { }
}

public class Consumer : ContentPart
{
  public UserPickerField User { get; init; } = new();
}
