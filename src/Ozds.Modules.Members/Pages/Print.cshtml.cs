using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;

namespace Ozds.Modules.Members.Pages;

public class PrintModel : PageModel
{
  public async Task<IActionResult> OnGetAsync(string contentId)
  {
    var content = await ContentManger.GetAsync(contentId);
    Shape = await ContentDisplay.BuildDisplayAsync(
        content, UpdateModel.ModelUpdater, "Print");
    PrintHeader = await ContentDisplay.BuildDisplayAsync(
        content, UpdateModel.ModelUpdater, "PrintHeader");
    return Page();
  }

  public IActionResult OnGetDownload(string contentId, string fileName)
  {
    fileName = string.IsNullOrWhiteSpace(fileName) ? contentId : fileName;
    var docUrl = string.Format(
        "https://{0}/Members/Print/{1}/", Request.Host, contentId);
    return Redirect(string.Format(DownloadFormat, fileName, docUrl));
  }

  public IShape? PrintHeader { get; set; }
  public IShape? Shape { get; set; }

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