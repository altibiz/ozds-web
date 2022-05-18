using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;

namespace Ozds.Modules.Members.Pages;

[Authorize(Roles = "Administrator")]
public class PrintModel : PageModel
{
  public IShape? Print { get; set; }

  public async Task<IActionResult> OnGetAsync(string contentItemId)
  {
    Print = await ContentDisplay
      .BuildDisplayAsync(
          await ContentManger.GetAsync(contentItemId),
          UpdateModel.ModelUpdater,
          "Print");

    return Page();
  }

  public IActionResult OnGetDownload(string contentItemId, string fileName) =>
    Redirect(
      string.Format(
        DownloadFormat,
        string.IsNullOrWhiteSpace(fileName) ? contentItemId
        : fileName,
        string.Format(
          "https://{0}/Ozds.Modules.Members/Print/{1}/",
          Request.Host,
          contentItemId)));


  public PrintModel(IContentItemDisplayManager contentDisplay,
      IContentManager content, IUpdateModelAccessor updateModel,
      IConfiguration conf)
  {
    ContentManger = content;
    ContentDisplay = contentDisplay;
    UpdateModel = updateModel;
    DownloadFormat = conf.GetValue<string>("PrintPdfUrl");
  }

  private string DownloadFormat { get; }

  private IContentManager ContentManger { get; }
  private IContentItemDisplayManager ContentDisplay { get; }
  private IUpdateModelAccessor UpdateModel { get; }
}
