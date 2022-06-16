using Microsoft.AspNetCore.Authorization;
using Ozds.Elasticsearch;

public class AuthorizeData : IAuthorizeData
{
  public string? Policy { get; set; }
  public string? Roles { get; set; }
  public string? AuthenticationSchemes { get; set; }
}

public readonly record struct InitialQuery<R>
(dynamic Variables,
 R Result);

public class Query
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByDevice(
      [Service] IDashboardMeasurementProvider provider,
      string deviceId,
      Period? period = null) =>
    provider.GetDashboardMeasurementsByDeviceAsync(deviceId, period);

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByOwner(
      [Service] IDashboardMeasurementProvider provider,
      string ownerId,
      Period? period = null) =>
    provider.GetDashboardMeasurementsByOwnerAsync(ownerId, period);

  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurementsByOwnerUser(
      [Service] IDashboardMeasurementProvider provider,
      string ownerUserId,
      Period? period = null) =>
    provider.GetDashboardMeasurementsByOwnerUserAsync(ownerUserId, period);
}

public class DashboardMeasurementType :
  ObjectType<DashboardMeasurement>
{
  protected override void Configure(
      IObjectTypeDescriptor<DashboardMeasurement> descriptor)
  {
    descriptor
      .Field(f => f.Timestamp)
      .Type<DateTimeType>();

    descriptor
      .Field(f => f.DeviceId)
      .Type<StringType>();

    descriptor
      .Field(f => f.Data)
      .Type<DashboardMeasurementDataType>();
  }
}

public class DashboardMeasurementDataType :
  ObjectType<DashboardMeasurementData>
{
  protected override void Configure(
      IObjectTypeDescriptor<DashboardMeasurementData> descriptor)
  {
    descriptor
      .Field(f => f.Energy)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.HighCostEnergy)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.LowCostEnergy)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.Power)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.PowerL1)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.PowerL2)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.PowerL3)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.CurrentL1)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.CurrentL2)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.CurrentL3)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.VoltageL1)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.VoltageL2)
      .Type<DecimalType>();

    descriptor
      .Field(f => f.VoltageL3)
      .Type<DecimalType>();
  }
}

public class PeriodType :
  InputObjectType<Period>
{
  protected override void Configure(
      IInputObjectTypeDescriptor<Period> descriptor)
  {
    descriptor
      .Field(f => f.From)
      .Type<DateTimeType>();

    descriptor
      .Field(f => f.To)
      .Type<DateTimeType>();

    descriptor
      .Ignore(f => f.Span);

    descriptor
      .Ignore(f => f.HalfPoint);
  }
}
