using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrchardCore.Admin;
using OrchardCore.Media;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;

namespace Ozds.Modules.Ozds;

[Admin]
public class MediaImportController : Controller
{
    public IActionResult Index()
    {
      var model = new MediaImportModel { };

      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Import(MediaImportModel model)
    {
      if (model.Urls is not null)
      {
        var urls = model.Urls.Split("\n");

        var paths = await Import(urls);

        model.Paths = paths;
        Logger.LogDebug(string.Join(", ", paths));
      }

      return RedirectToAction(nameof(Index), model);
    }


    private async Task<string[]> Import(string[] urls)
    {
      var result = new List<string>();

      foreach(var url in urls)
        {
          try
          {
            using (var response = await Http.GetAsync(url))
            {
              response.EnsureSuccessStatusCode();

              using (var stream = await response.Content.ReadAsStreamAsync())
              {
                var format = await Image.DetectFormatAsync(stream);
                if (format is not null &&
                format.FileExtensions.FirstOrDefault(
                  ext => Options.Value.AllowedFileExtensions.Contains($".{ext}")) is string ext)
                {
                  var path = await Store.CreateFileFromStreamAsync(
                    $"{Guid.NewGuid().ToString()}.{ext}",
                    stream);
                  result.Add(path);
                }
                else
                {
                  Logger.LogDebug("Picture {} is in wrong format {} {} - allowed {}", url,
                   format?.Name,
                   string.Join(", ", format?.FileExtensions ?? new string[0] {}),
                    string.Join(", ", Options.Value.AllowedFileExtensions));
                }
              }
            }
          }
          catch (Exception ex)
          {
            Logger.LogDebug("Error creating image from url {} {}", url, ex.Message);
          }
        }

      return result.ToArray();
    }

    public MediaImportController(
        ILogger<MediaImportController> logger,
      IMediaFileStore store,
      IOptions<MediaOptions> options,
      HttpClient http)
    {
      Logger = logger;

      Store = store;
      Options = options;
      Http = http;
    }

    ILogger Logger { get;}

    private IMediaFileStore Store { get; }

    private IOptions<MediaOptions> Options { get; }

    private HttpClient Http { get; }
}