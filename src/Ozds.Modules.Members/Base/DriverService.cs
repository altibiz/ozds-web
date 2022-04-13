using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentManagement.Metadata.Settings;
using Ozds.Modules.Members.PartFieldSettings;

namespace Ozds.Modules.Members.Base {
  public class DriverService {
    public static ContentPartFieldDefinition? GetFieldDef(
        BuildFieldEditorContext context, bool isAdminTheme) {
      IFieldEditorSettings? partSettings = null;
      var oldDef = context.PartFieldDefinition;
      foreach (var typ in ImplementingTypes) {
        context.TypePartDefinition.Settings.TryGetValue(
            typ.Name, out JToken? val);
        if (val == null)
          continue;
        partSettings = val.ToObject(typ) as IFieldEditorSettings;
      }
      if (partSettings != null) {
        var textset = oldDef.GetSettings<ContentPartFieldSettings>();
        var newEditor =
            partSettings.GetFieldDisplayMode(context.PartFieldDefinition.Name,
                textset.Editor, context, isAdminTheme);
        if (!newEditor.IsVisible)
          return null;
        var newDispName =
            partSettings.GetFieldLabel(context.PartFieldDefinition.Name,
                textset.DisplayName, isAdminTheme);
        if (textset.Editor != newEditor || textset.DisplayName != newDispName) {
          var newDef = new ContentPartFieldDefinition(oldDef.FieldDefinition,
              oldDef.Name,
              oldDef.Settings) { PartDefinition = oldDef.PartDefinition };
          var newSett = newDef.GetSettings<ContentPartFieldSettings>();
          newSett.Editor = newEditor;
          newSett.DisplayName = newDispName;
          return newDef;
        }
      }
      return oldDef;
    }

    private static IEnumerable<Type> ImplementingTypes {
      get;
    } = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(
                type => typeof(IFieldEditorSettings).IsAssignableFrom(type) &&
                        type.IsClass)
            .ToList();
  }
}