using GraphQL.Types;
using Microsoft.Extensions.Localization;
using Ozds.Elasticsearch;

namespace Ozds.Modules.Ozds;

public class DashboardMeasurementDataObjectGraphType :
  ObjectGraphType<DashboardMeasurementData>
{
  public DashboardMeasurementDataObjectGraphType(
      IStringLocalizer<DashboardMeasurementDataObjectGraphType> S)
  {
    Name = "DashboardMeasurementData";
    Description = S["Measurement data for drawing graphs on dashboards"];

    Field<StringGraphType>()
      .Name("power")
      .Description(S["Power"])
      .Resolve(s => s.Source.Power);

    Field<StringGraphType>()
      .Name("energy")
      .Description(S["Energy"])
      .Resolve(s => s.Source.Energy);

    Field<StringGraphType>()
      .Name("lowCostEnergy")
      .Description(S["Energy during low cost tariff"])
      .Resolve(s => s.Source.LowCostEnergy);

    Field<StringGraphType>()
      .Name("highCostEnergy")
      .Description(S["Energy during high cost tariff"])
      .Resolve(s => s.Source.HighCostEnergy);
  }
}
