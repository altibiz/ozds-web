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
using Ozds.Util;

namespace Ozds.Modules.Ozds.PartFieldSettings;

public class PartTextFieldDriver : TextFieldDisplayDriver
{
  public override IDisplayResult? Edit(
      TextField field,
      BuildFieldEditorContext context) =>
    context
      .GetFieldDefinition(AdminAttribute.IsApplied(HttpContext.HttpContext))
      .WhenNonNullable(fieldDefinition =>
        Initialize<EditTextFieldViewModel>(
          GetEditorShapeType(fieldDefinition),
          model =>
          {
            model.Text = field.Text;
            model.Field = field;
            model.Part = context.ContentPart;
            model.PartFieldDefinition = fieldDefinition;
          }));

  public override Task<IDisplayResult?> UpdateAsync(
      TextField field,
      IUpdateModel updater,
      UpdateFieldEditorContext context) =>
    context
      .GetFieldDefinition(AdminAttribute.IsApplied(HttpContext.HttpContext))
      .WhenTask(fieldDefinition => fieldDefinition.Editor() != "Disabled",
        _ => base.UpdateAsync(field, updater, context),
        () => Edit(field, context));

  public PartTextFieldDriver(
      IStringLocalizer<TextFieldDisplayDriver> localizer,
      IHttpContextAccessor httpContext) : base(localizer)
  {
    HttpContext = httpContext;
  }

  private IHttpContextAccessor HttpContext;
}
