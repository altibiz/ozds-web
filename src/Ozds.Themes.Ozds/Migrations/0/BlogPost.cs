using OrchardCore.Autoroute.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Indexing;
using OrchardCore.Media.Settings;
using OrchardCore.Taxonomies.Settings;

namespace Ozds.Themes.Ozds.M0;

public static partial class AlterBlogPost {
  public static void AlterBlogPostType(this IContentDefinitionManager
          content) => content.AlterTypeDefinition("BlogPost",
      type =>
          type.RemovePart("MarkdownBodyPart")
              .DisplayedAs("Blog Post")
              .Draftable()
              .Versionable()
              .WithPart("TitlePart", part => part.WithPosition("0"))
              .WithPart("AutoroutePart",
                  part => part.WithPosition("2").WithSettings(
                      new AutoroutePartSettings {
                        AllowCustomPath = true,
                        Pattern =
                            "{{ Model.ContentItem | container | display_text | slugify }}/{{ Model.ContentItem | display_text | slugify }}",
                        ShowHomepageOption = false,
                      }))
              .WithPart("BlogPost", part => part.WithPosition("3"))
              .WithPart("HtmlBodyPart",
                  part => part.WithPosition("1").WithEditor("Wysiwyg")));

  public static void AlterBlogPostPart(this IContentDefinitionManager content) {
    content.AlterPartDefinition(
        "BlogPost", part => part.RemoveField("Category").RemoveField("Tags"));

    content.AlterPartDefinition("BlogPost",
        part =>
            part.WithField("Subtitle", field => field.OfType("TextField")
                                                    .WithDisplayName("Subtitle")
                                                    .WithPosition("0"))
                .WithField("Image",
                    field =>
                        field.OfType("MediaField")
                            .WithDisplayName("Banner Image")
                            .WithPosition("1")
                            .WithSettings(
                                new ContentIndexSettings { Included = false,
                                  Stored = false, Analyzed = false })
                            .WithSettings(new MediaFieldSettings {
                              Multiple = false,
                              AllowAnchors = true,
                            }))
                .WithField("Tags",
                    field => field.OfType("TaxonomyField")
                                 .WithDisplayName("Tags")
                                 .WithEditor("Tags")
                                 .WithDisplayMode("Tags")
                                 .WithPosition("2")
                                 .WithSettings(new TaxonomyFieldSettings {
                                   TaxonomyContentItemId =
                                       "45j76cwwz4f4v4hx5zqxfpzvwq",
                                 }))
                .WithField("Category",
                    field => field.OfType("TaxonomyField")
                                 .WithDisplayName("Category")
                                 .WithPosition("3")
                                 .WithSettings(new TaxonomyFieldSettings {
                                   TaxonomyContentItemId =
                                       "4dgj6ce33vdsbxqz8hw4c4c24d",
                                   Unique = true,
                                   LeavesOnly = true,
                                 })));
  }
}
