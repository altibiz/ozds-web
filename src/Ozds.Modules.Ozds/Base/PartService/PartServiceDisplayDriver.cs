﻿using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Mvc.ModelBinding;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class PartServiceDisplayDriver<TPart, TService>
    : ContentPartDisplayDriver<TPart>
    where TPart : ContentPart, new()
    where TService : IPartService<TPart>
{
  public override IDisplayResult Edit(
      TPart part, BuildPartEditorContext context) =>
      Service.GetEditModel(part, context)
          .WhenNonNull(
              model => Initialize(GetEditorShapeType(context), model),
              () => base.Edit(part));

  public override Task<IDisplayResult> UpdateAsync(
      TPart model, IUpdateModel updater, UpdatePartEditorContext context) =>
      updater.TryUpdateModelAsync(model, Prefix)
          .Then<Task>(() => AsyncEnumerable.ForEachAsync(Service.ValidateAsync(model)
, item => updater.ModelState
              .BindValidationResult(Prefix, item)))
          .Then(() => Edit(model, context));

  public PartServiceDisplayDriver(TService service) { Service = service; }

  private TService Service { get; }
}
