using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Members.Pages;

public class MeasurementsModel : PageModel
{
  public MeasurementsModel(IClient client, ILogger<MeasurementsModel> logger)
  {
    Client = client;
    Logger = logger;
  }

  public void OnGet()
  {
    var now = DateTime.UtcNow;
    Measurements = Client
                       .SearchMeasurementsSorted(
                           new Period { From = now.AddMinutes(-5), To = now })
                       .Sources()
                       .ToList();
  }

  public List<Measurement> Measurements { get; set; }

  private IClient Client { get; }
  private ILogger Logger { get; }
}
