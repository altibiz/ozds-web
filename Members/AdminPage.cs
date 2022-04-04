﻿using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Title.Models;

namespace Members;

public static class AdminMigrations {
  public static void AdminPage(
      this IContentDefinitionManager contentDefinitionManager) =>
      contentDefinitionManager.AlterTypeDefinition("AdminPage",
          type => type.DisplayedAs("Admin Page")
                      .Creatable()
                      .Listable()
                      .Draftable()
                      .Versionable()
                      .Securable()
                      .WithPart("AdminPage", part => part.WithPosition("1"))
                      .WithPart("LiquidPart", part => part.WithPosition("2"))
                      .WithPart("TitlePart",
                          part => part.WithPosition("0").WithSettings(
                              new TitlePartSettings {
                                Options = TitlePartOptions.EditableRequired,
                              })));
}
