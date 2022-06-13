using Microsoft.AspNetCore.Authorization;
using Ozds.Elasticsearch;

public class AuthorizeData : IAuthorizeData
{
  public string? Policy { get; set; }
  public string? Roles { get; set; }
  public string? AuthenticationSchemes { get; set; }
}

public class Query
{
  public Task<IEnumerable<DashboardMeasurement>>
  GetDashboardMeasurements(
      [Service] IDashboardMeasurementProvider provider,
      string deviceId,
      Period? period = null) =>
    provider.GetDashboardMeasurementsAsync(deviceId, period);

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwner(
      [Service] IDashboardMeasurementProvider provider,
      string ownerId,
      Period? period = null) =>
    provider.GetDashboardMeasurementsByOwnerAsync(ownerId, period);

  public Task<MultiDashboardMeasurements>
  GetDashboardMeasurementsByOwnerUser(
      [Service] IDashboardMeasurementProvider provider,
      string userId,
      Period? period = null) =>
    provider.GetDashboardMeasurementsByOwnerUserAsync(userId, period);
}

public class MultiDashboardMeasurementsType :
  ObjectType<MultiDashboardMeasurements>
{
  protected override void Configure(
      IObjectTypeDescriptor<MultiDashboardMeasurements> descriptor)
  {
    descriptor
      .Field(f => f.DeviceIds)
      .Type<ListType<StringType>>();

    descriptor
      .Field(f => f.Measurements)
      .Type<ListType<MultiDashboardMeasurementDataType>>();
  }
}

public class MultiDashboardMeasurementDataType :
  ObjectType<MultiDashboardMeasurementData>
{
  protected override void Configure(
      IObjectTypeDescriptor<MultiDashboardMeasurementData> descriptor)
  {
    descriptor
      .Field(f => f.Timestamp)
      .Type<DateTimeType>();

    descriptor
      .Field(f => f.Data)
      .Type<ListType<DeviceDashboardMeasurementDataType>>();
  }
}

public class DeviceDashboardMeasurementDataType :
  ObjectType<DeviceDashboardMeasurementData>
{
  protected override void Configure(
      IObjectTypeDescriptor<DeviceDashboardMeasurementData> descriptor)
  {
    descriptor
      .Field(f => f.DeviceId)
      .Type<StringType>();

    descriptor
      .Field(f => f.Data)
      .Type<DashboardMeasurementDataType>();
  }
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

    descriptor.Ignore(f => f.Span);
    descriptor.Ignore(f => f.HalfPoint);
  }
}
