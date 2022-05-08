using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;

namespace Ozds.Modules.Members;

public class ConsumerType : ContentTypeBase
{
  public Lazy<TitlePart> Title { get; init; } = default!;
  public Lazy<Consumer> Consumer { get; init; } = default!;
  public Lazy<Person> Person { get; init; } = default!;

  private ConsumerType(ContentItem item) : base(item) { }
}

public class Consumer : ContentPart
{
  public ContentPickerField SecondarySites { get; set; } = new();
}
