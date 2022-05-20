using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Mvc.ModelBinding;
using Ozds.Util;

namespace Ozds.Modules.Ozds;

public class PartServiceDisplayDriver<TPart, TService>
    : ContentPartDisplayDriver<TPart>
    where TPart : ContentPart, new()
    where TService : IPartService<TPart>
{
  public override IDisplayResult Edit(
      TPart part, BuildPartEditorContext context) =>
      Service.GetEditModel(part, context)
          .WhenNonNullable(
              model => Initialize(GetEditorShapeType(context), model),
              () => base.Edit(part));

  public override Task<IDisplayResult> UpdateAsync(
      TPart model, IUpdateModel updater, UpdatePartEditorContext context) =>
      updater.TryUpdateModelAsync(model, Prefix)
          .After(() => Service.ValidateAsync(model)
            .ForEachAsync(item => updater.ModelState
              .BindValidationResult(Prefix, item)))
          .After(() => Edit(model, context));

  public PartServiceDisplayDriver(TService service) { Service = service; }

  private TService Service { get; }
}
