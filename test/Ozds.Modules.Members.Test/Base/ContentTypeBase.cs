using Xunit;
using OrchardCore.Title.Models;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members.Test;

public class ContentTypeBaseConstructs
{
  [Fact]
  public void Title() =>
    new ContentItem()
      .WithNullable(item =>
        {
          item.Weld<TitlePart>();
          item.Alter<TitlePart>(titlePart => titlePart.Title = "MyTitle");
        })
      .AsContent<TitleType>()
      .AssertNotNull()
      .WithNullable(type =>
        {
          type
            .TitlePart
              .AssertNotNull()
            .Value
            .Title
              .AssertNotNull()
              .AssertEquals("MyTitle");
        });

  private class TitleType : ContentTypeBase
  {
    public Lazy<TitlePart> TitlePart { get; init; } = default!;

    private TitleType(ContentItem contentItem) : base(contentItem) { }
  }

  [Fact]
  public void TwoTitles() =>
    new ContentItem()
      .WithNullable(item =>
        {
          item.Weld(
            "FirstTitle",
            new TitlePart
            {
              Title = "MyFirstTitle"
            });

          item.Weld(
            "SecondTitle",
            new TitlePart
            {
              Title = "MySecondTitle"
            });
        })
      .AsContent<TwoTitlesType>()
      .AssertNotNull()
      .WithNullable(type =>
        {
          type
            .FirstTitle
              .AssertNotNull()
            .Value
            .Title
              .AssertNotNull()
              .AssertEquals("MyFirstTitle");

          type
            .SecondTitle
              .AssertNotNull()
            .Value
            .Title
              .AssertNotNull()
              .AssertEquals("MySecondTitle");
        });

  private class TwoTitlesType : ContentTypeBase
  {
    public Lazy<TitlePart> FirstTitle { get; init; } = default!;
    public Lazy<TitlePart> SecondTitle { get; init; } = default!;

    private TwoTitlesType(ContentItem contentItem) : base(contentItem) { }
  }
}
