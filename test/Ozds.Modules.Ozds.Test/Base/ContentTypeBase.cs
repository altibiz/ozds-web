using Xunit;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;
using OrchardCore.Lists.Models;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds.Test;

public class ContentTypeBaseConstructs
{
  [Fact]
  public void Title() =>
    new ContentItem()
      .With(item =>
        {
          item.ContentType = "Title";
          item.Weld(
            nameof(TitleType.TitlePart),
            new TitlePart
            {
              Title = "MyTitle"
            });

        })
      .AsContent<TitleType>()
      .AssertNotNull()
      .With(type =>
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
      .With(item =>
        {
          item.ContentType = "TwoTitles";
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
      .With(type =>
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
      .With(item =>
        {
          item.ContentType = "Contained";
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
      .With(type =>
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
              .With(part =>
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

  [Fact]
  public void Derived() =>
    new ContentItem()
      .With(item =>
        {
          item.ContentType = "Title";
          item.Weld(
            nameof(TitleType.TitlePart),
            new TitlePart
            {
              Title = "MyTitle"
            });
          item
            .Construct<DerivedType>()
            .AssertNotNull()
            .With(type =>
              {
                type
                  .TitlePart
                    .AssertNotNull()
                  .Value
                  .Title
                    .AssertNotNull()
                    .AssertEquals("MyTitle");
              });
        });

  private class DerivedType : ContentTypeBase<DerivedType>
  {
    public Lazy<TitlePart> TitlePart { get; init; } = default!;

    public DerivedType(ContentItem contentItem) : base(contentItem) { }
  }

  // TODO: CRTP on Tags
  // [Fact]
  // public void Tag() =>
  //   new ContentItem()
  //     .With(item =>
  //       {
  //         item.ContentType = "Tag";
  //         item.Weld(
  //           nameof(TitleType.TitlePart),
  //           new TitlePart
  //           {
  //             Title = "MyTitle"
  //           });
  //         item
  //           .Construct<TagType>()
  //           .AssertNotNull()
  //           .With(type =>
  //             {
  //               type
  //                 .Title
  //                   .AssertNotNull()
  //                 .Value
  //                 .Title
  //                   .AssertNotNull()
  //                   .AssertEquals("MyTitle");
  //             });
  //       });
  //
  // [Fact]
  // public void TariffTag() =>
  //   new ContentItem()
  //     .With(item =>
  //       {
  //         item.ContentType = "TariffTag";
  //         item.Weld(
  //           nameof(TitleType.TitlePart),
  //           new TitlePart
  //           {
  //             Title = "MyTitle"
  //           });
  //         item
  //           .Construct<TariffTagType>()
  //           .AssertNotNull()
  //           .With(type =>
  //             {
  //               type
  //                 .Title
  //                   .AssertNotNull()
  //                 .Value
  //                 .Title
  //                   .AssertNotNull()
  //                   .AssertEquals("MyTitle");
  //             });
  //       });
}
