using OrchardCore.Contents.ViewModels;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;

namespace Ozds.Modules.Members;

public class PersonOptionsDisplayDriver
    : DisplayDriver<ContentOptionsViewModel>
{
  protected override void BuildPrefix(
      ContentOptionsViewModel model,
      string htmlFieldPrefix) =>
    Prefix = "Options";

  public override IDisplayResult Display(ContentOptionsViewModel model) =>
    Combine(
      View("ContentsAdminFilters_Thumbnail__Oib", model)
        .Location("Thumbnail", "Content:10"));
}
