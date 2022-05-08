using Xunit;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;
using Ozds.Util;

namespace Ozds.Modules.Members.Test;

public class ContentTypeBaseConstructs
{
  [Fact]
  public void Title() =>
    new ContentItem()
      .WithNullable(item =>
        {
          item.Weld(
            nameof(TitleType.TitlePart),
            new TitlePart
            {
              Title = "MyTitle"
            });

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
            nameof(TwoTitlesType.FirstTitle),
            new TitlePart
            {
              Title = "MyFirstTitle"
            });

          item.Weld(
            nameof(TwoTitlesType.SecondTitle),
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

  [Fact]
  public void Contained() =>
    new ContentItem()
      .WithNullable(item =>
        {
          item.Weld(
            nameof(ContainedType.TitlePart),
            new TitlePart
            {
              Title = "MyTitle"
            });

          item.Weld(
            nameof(ContainedType.ContainedPart),
            new ContainedPart
            {
              ListContentItemId = "MyId",
              Order = 10
            });
        })
      .AsContent<ContainedType>()
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

          type
            .ContainedPart
              .AssertNotNull()
            .Value
              .AssertNotNull()
              .WithNullable(part =>
                {
                  part
                    .ListContentItemId
                      .AssertNotNull()
                      .AssertEquals("MyId");

                  part
                    .Order
                      .AssertNotNull()
                      .AssertEquals(10);
                });
        });

  private class ContainedType : ContentTypeBase
  {
    public Lazy<TitlePart> TitlePart { get; init; } = default!;

    private ContainedType(ContentItem contentItem) : base(contentItem) { }
  }
}
