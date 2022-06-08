using GraphQL.Types;
using Microsoft.Extensions.Localization;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Ozds;

public class MultiDashboardMeasurementsObjectGraphType :
  ObjectGraphType<MultiDashboardMeasurements>
{
  public MultiDashboardMeasurementsObjectGraphType(
      IStringLocalizer<MultiDashboardMeasurements> S)
  {
    Name = "MultiDashboardMeasurements";
    Description = S["Interpolated dashboard measurements of multiple devices"];

    Field<ListGraphType<StringGraphType>, IEnumerable<string>>()
      .Name("deviceIds")
      .Description(S["Ids of the devices which measured the data"])
      .Resolve(s => s.Source.DeviceIds);

    Field<
        ListGraphType<MultiDashboardMeasurementDataObjectGraphType>,
        IEnumerable<MultiDashboardMeasurementData>>()
      .Name("measurements")
      .Description(S["Measurement data with device ids"])
      .Resolve(s => s.Source.Measurements);
  }
}
