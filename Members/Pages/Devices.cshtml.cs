using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elasticsearch;

namespace Members.Pages;

public class DevicesModel : PageModel
{
  public DevicesModel(IClient client, ILogger<DevicesModel> logger)
  {
    Client = client;
    Logger = logger;
  }

  public void OnGet()
  {
    Devices =
        Client.SearchDevices(Elasticsearch.MeasurementFaker.Client.FakeSource)
            .Sources()
            .ToList();
  }

  public List<Device> Devices { get; set; }

  private IClient Client { get; }
  private ILogger Logger { get; }
}