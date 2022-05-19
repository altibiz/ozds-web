using OrchardCore.ContentManagement.Metadata;

namespace Ozds.Themes.Ozds.M0;

public static class ContentMigrations
{
  public static IContentDefinitionManager AlterContent(
      this IContentDefinitionManager content)
  {
    content.AlterPagePart();
    content.AlterPageType();

    return content;
  }
}
