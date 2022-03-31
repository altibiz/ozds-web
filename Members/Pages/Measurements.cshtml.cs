using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elasticsearch;

namespace Members.Pages;

public class MeasurementsModel : PageModel {
  public MeasurementsModel(IClient client, ILogger<MeasurementsModel> logger) {
    Client = client;
    Logger = logger;
  }

  public void OnGet() {
    Devices = new List<Device> {
      Client
          .GetDevice(
              Device.MakeId(Elasticsearch.MeasurementFaker.Client.FakeSource,
                  Elasticsearch.MeasurementFaker.Client.FakeDeviceId))
          .Source
    };
  }

  public List<Device> Devices { get; set; }

  private IClient Client { get; }
  private ILogger Logger { get; }
}
