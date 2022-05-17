using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using Ozds.Elasticsearch;
using Ozds.Util;

namespace Ozds.Modules.Members.Pages;

public class DashboardModel : PageModel
{
  public List<DashboardMeasurement> Measurements { get; } = new();

  public async Task<IActionResult> OnGetAsync(
      string secondarySiteContentItemId)
  {
    var secondarySite = await ContentManager
      .GetContentAsync<SecondarySiteType>(secondarySiteContentItemId)
      .ThrowWhenNull();

    var source = SiteMeasurementSource
      .GetElasticsearchSource(
          secondarySite.Site.Value.Source.TermContentItemIds[0])
      .ThrowWhenNull();
    var deviceId = secondarySite.Site.Value.DeviceId.Text;

    var deviceMeasurements = Provider
      .GetDashboardMeasurements(source, deviceId);

    Measurements.Clear();
    Measurements.AddRange(deviceMeasurements);

    return Page();
  }

  public DashboardModel(
      IDashboardMeasurementProvider provider,
      IContentManager content)
  {
    Provider = provider;
    ContentManager = content;
  }

  private IDashboardMeasurementProvider Provider { get; }
  // NOTE: Content hides a PageModel member
  private IContentManager ContentManager { get; }
}
