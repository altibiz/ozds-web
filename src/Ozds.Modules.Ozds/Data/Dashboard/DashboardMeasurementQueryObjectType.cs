using GraphQL.Types;
using Microsoft.Extensions.Localization;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Ozds;

public class DashboardMeasurementObjectGraphType :
  ObjectGraphType<DashboardMeasurement>
{
  public DashboardMeasurementObjectGraphType(
      IStringLocalizer<DashboardMeasurementObjectGraphType> S)
  {
    Name = "DashboardMeasurement";
    Description = S[
      "Measurement data and metadata for drawing graphs on dashboards"];

    Field<StringGraphType, string>()
      .Name("deviceId")
      .Description(S["Id of the device which measured the data"])
      .Resolve(s => s.Source.DeviceId);

    Field<DateTimeGraphType, DateTime>()
      .Name("timestamp")
      .Description(S["Date and time at which the measurement was taken"])
      .Resolve(s => s.Source.Timestamp);

    Field<DashboardMeasurementDataObjectGraphType, DashboardMeasurementData>()
      .Name("data")
      .Description(S["Measurement data for drawing graphs on dashboards"])
      .Resolve(s => s.Source.Data);
  }
}
