using GraphQL.Types;
using Microsoft.Extensions.Localization;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Ozds;

public class MultiDashboardMeasurementDataObjectGraphType :
  ObjectGraphType<MultiDashboardMeasurementData>
{
  public MultiDashboardMeasurementDataObjectGraphType(
      IStringLocalizer<MultiDashboardMeasurementData> S)
  {
    Name = "MultiDashboardMeasurementData";
    Description = S[
      "Measurement data of a single timestamp for multiple devices"];

    Field<DateTimeGraphType, DateTime>()
      .Name("timestamp")
      .Description(S["Date and time of measurement"])
      .Resolve(s => s.Source.Timestamp);

    Field<
        ListGraphType<DeviceDashboardMeasurementDataObjectGraphType>,
        IEnumerable<DeviceDashboardMeasurementData>>()
      .Name("measurements")
      .Description(S["Measurement data with device ids"])
      .Resolve(s => s.Source.Data
          .Select(kv =>
            new DeviceDashboardMeasurementData
            {
              DeviceId = kv.Key,
              Data = kv.Value
            }));
  }
}
