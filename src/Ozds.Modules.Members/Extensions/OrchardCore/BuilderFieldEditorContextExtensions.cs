using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentManagement.Metadata.Settings;

namespace Ozds.Modules.Members;

public static class BuildFieldEditorContextExtensions
{
  public static ContentPartFieldDefinition? GetFieldDefinition(
      this BuildFieldEditorContext context, bool isAdminTheme) =>
      context.TypePartDefinition.Settings.Properties()
          .SelectFirst(property =>
              ImplementingTypes
                .GetOrDefault(property.Name)
                  .When(type => property.Value
                    .ToObject(type)
                    .As<IFieldEditorSettings>()))
          .When(partSettings =>
          {
            var oldSettings = context.PartFieldDefinition
              .GetSettings<ContentPartFieldSettings>();

            var newEditor = partSettings
              .GetFieldDisplayMode(
                  context.PartFieldDefinition.Name,
                  oldSettings.Editor,
                  context,
                  isAdminTheme);
            if (!newEditor.IsVisible)
            {
              return null;
            }

            var newDisplayName = partSettings
              .GetFieldLabel(
                  context.PartFieldDefinition.Name,
                  oldSettings.DisplayName,
                  isAdminTheme);
            if (oldSettings.Editor == newEditor &&
                oldSettings.DisplayName == newDisplayName)
            {
              return context.PartFieldDefinition;
            }

            var newDefinition = new ContentPartFieldDefinition(
                context.PartFieldDefinition.FieldDefinition,
                context.PartFieldDefinition.Name,
                context.PartFieldDefinition.Settings)
            {
              PartDefinition = context.PartFieldDefinition.PartDefinition
            };
            var newSettings = newDefinition
              .GetSettings<ContentPartFieldSettings>();
            newSettings.Editor = newEditor;
            newSettings.DisplayName = newDisplayName;
            return newDefinition;
          });

  private static IDictionary<string, Type> ImplementingTypes { get; } =
      AppDomain.CurrentDomain.GetAssemblies()
          .SelectMany(assembly => assembly.GetTypes())
          .Where(type =>
              type.IsAssignableTo<IFieldEditorSettings>() &&
              type.IsClass)
          .ToDictionary(type => type.Name);
}
