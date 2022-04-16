using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentManagement.Metadata.Settings;
using Ozds.Modules.Members.PartFieldSettings;

namespace Ozds.Modules.Members;

public static class BuildFieldEditorContextExtensions
{
  public static ContentPartFieldDefinition GetFieldDefinition(
      this BuildFieldEditorContext context, bool isAdminTheme) =>
      context.TypePartDefinition.Settings.Properties()
          .SelectFirstOrDefault(
              property => ImplementingTypes.GetOrDefault(property.Name)
                              .SelectOrDefault(
                                  type => property.Value.ToObject(type)
                                              as IFieldEditorSettings))
          .SelectOrDefault(partSettings =>
          {
            var oldDefinition = context.PartFieldDefinition;
            var oldSettings =
                oldDefinition.GetSettings<ContentPartFieldSettings>();
            var newEditor = partSettings.GetFieldDisplayMode(
                context.PartFieldDefinition.Name, oldSettings.Editor, context,
                isAdminTheme);
            var newDisplayName =
                partSettings.GetFieldLabel(context.PartFieldDefinition.Name,
                    oldSettings.DisplayName, isAdminTheme);
            if (!newEditor.IsVisible ||
                (oldSettings.Editor == newEditor &&
                    oldSettings.DisplayName == newDisplayName))
            {
              return oldDefinition;
            }

            var newDefinition = new ContentPartFieldDefinition(
                oldDefinition.FieldDefinition, oldDefinition.Name,
                oldDefinition.Settings)
            {
              PartDefinition =
                                              oldDefinition.PartDefinition
            };
            var newSettings =
                newDefinition.GetSettings<ContentPartFieldSettings>();
            newSettings.Editor = newEditor;
            newSettings.DisplayName = newDisplayName;
            return newDefinition;
          }, context.PartFieldDefinition);

  private static IDictionary<string, Type> ImplementingTypes { get; } =
      AppDomain.CurrentDomain.GetAssemblies()
          .SelectMany(assembly => assembly.GetTypes())
          .Where(type => typeof(IFieldEditorSettings).IsAssignableFrom(type) &&
                         type.IsClass)
          .ToDictionary(type => type.Name);
}
