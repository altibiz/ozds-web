using Ozds.Modules.Members.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Admin;
using OrchardCore.ContentFields.Drivers;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.ViewModels;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Ozds.Modules.Members.PartFieldSettings {
  public class PartTextFieldDriver : TextFieldDisplayDriver {
    private IHttpContextAccessor _httpCA;

    public PartTextFieldDriver(
        IStringLocalizer<TextFieldDisplayDriver> localizer,
        IHttpContextAccessor httpContextAccessor)
        : base(localizer) {
      _httpCA = httpContextAccessor;
    }

    public override IDisplayResult Edit(
        TextField field, BuildFieldEditorContext context) {
      var fieldDef = DriverService.GetFieldDef(
          context, AdminAttribute.IsApplied(_httpCA.HttpContext));
      if (fieldDef == null)
        return null;
      return Initialize<EditTextFieldViewModel>(
          GetEditorShapeType(fieldDef), model => {
            model.Text = field.Text;
            model.Field = field;
            model.Part = context.ContentPart;
            model.PartFieldDefinition = fieldDef;
          });
    }

    public override async Task<IDisplayResult> UpdateAsync(TextField field,
        IUpdateModel updater, UpdateFieldEditorContext context) {
      var fieldDef = DriverService.GetFieldDef(
          context, AdminAttribute.IsApplied(_httpCA.HttpContext));
      if (fieldDef == null)
        return null;
      if (fieldDef.Editor() == "Disabled")
        return Edit(field, context);
      return await base.UpdateAsync(field, updater, context);
    }
  }
}
