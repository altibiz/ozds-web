﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Admin;
using OrchardCore.ContentFields.Drivers;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentFields.ViewModels;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Globalization;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class PartNumericFieldDriver : NumericFieldDisplayDriver
{
  public override IDisplayResult? Edit(
      NumericField field, BuildFieldEditorContext context) =>
      context
          .GetFieldDefinition(AdminAttribute.IsApplied(HttpContext.HttpContext))
          .WhenNonNull(fieldDefinition =>
            Initialize<EditNumericFieldViewModel>(
              GetEditorShapeType(fieldDefinition),
              model =>
              {
                model.Value =
                    context.IsNew
                        ? context.PartFieldDefinition
                              .GetSettings<NumericFieldSettings>()
                              .DefaultValue
                        : Convert.ToString(
                              field.Value, CultureInfo.CurrentUICulture);
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = fieldDefinition;
              }));

  public override async Task<IDisplayResult?> UpdateAsync(
      NumericField field,
      IUpdateModel updater,
      UpdateFieldEditorContext context) =>
    context
      .GetFieldDefinition(AdminAttribute.IsApplied(HttpContext.HttpContext))
      .Editor() == "Disabled" ?
      Edit(field, context)
    : await base.UpdateAsync(field, updater, context);

  public PartNumericFieldDriver(
      IStringLocalizer<NumericFieldDisplayDriver> localizer,
      IHttpContextAccessor httpContextAccessor) : base(localizer)
  {
    HttpContext = httpContextAccessor;
  }

  private IHttpContextAccessor HttpContext { get; }
}
