using Xunit;
using OrchardCore.Title.Models;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members.Test;

public class ContentTypeBaseConstructs
{
  [Fact]
  public void Constructs() =>
    new ContentItem()
      .WithNullable(
        item =>
        {
          item.Weld<TitlePart>();
          item.Alter<TitlePart>(titlePart => titlePart.Title = "MyTitle");
        })
      .AsContent<Tag>()
      .AssertNotNull()
      .WithNullable(tag =>
          tag.Title.AssertNotNull()
          .Value.Title.AssertEquals("MyTitle"));

  private class Tag : ContentTypeBase
  {
    public Lazy<TitlePart> Title { get; init; } = default!;

    private Tag(ContentItem contentItem) : base(contentItem) { }
  }
}
