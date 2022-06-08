using GraphQL.Types;
using Microsoft.Extensions.Localization;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Ozds;

public class DeviceDashboardMeasurementDataObjectGraphType :
  ObjectGraphType<DeviceDashboardMeasurementData>
{
  public DeviceDashboardMeasurementDataObjectGraphType(
      IStringLocalizer<DeviceDashboardMeasurementDataObjectGraphType> S)
  {
    Name = "DeviceDashboardMeasurementData";
    Description = S["Measurement data and a device id"];

    Field<StringGraphType, string>()
      .Name("deviceId")
      .Description(S["Id of the device which measured the data"])
      .Resolve(s => s.Source.DeviceId);

    Field<DashboardMeasurementDataObjectGraphType, DashboardMeasurementData>()
      .Name("data")
      .Description(S["Measurement data for drawing graphs on dashboards"])
      .Resolve(s => s.Source.Data);
  }
}
