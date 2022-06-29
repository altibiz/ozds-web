using Nest;

namespace Ozds.Elasticsearch;

public static class QueryContainerExtensions
{
  public static QueryContainer PeriodRange(
      this QueryContainerDescriptor<Measurement> @this,
      Period? period) =>
    period is null ? @this
    : @this
        .DateRange(d => d
          .Field(f => f.Timestamp)
          .GreaterThanOrEquals(period.From)
          .LessThan(period.To));

  public static QueryContainer DeviceIdTerm(
      this QueryContainerDescriptor<Measurement> @this,
      string? deviceId) =>
    deviceId is null ? @this
    : @this.Term(f => f.DeviceData.DeviceId, deviceId);

  public static QueryContainer OwnerIdTerm(
      this QueryContainerDescriptor<Measurement> @this,
      string? ownerId) =>
    ownerId is null ? @this
    : @this.Term(f => f.DeviceData.OwnerId, ownerId);

  public static QueryContainer OwnerUserIdTerm(
      this QueryContainerDescriptor<Measurement> @this,
      string? ownerUserId) =>
    ownerUserId is null ? @this
    : @this.Term(f => f.DeviceData.OwnerUserId, ownerUserId);

  public static QueryContainer OwnerIdTerm(
      this QueryContainerDescriptor<Device> @this,
      string? ownerId) =>
    ownerId is null ? @this
    : @this.Term(f => f.OwnerData.OwnerId, ownerId);

  public static QueryContainer OwnerUserIdTerm(
      this QueryContainerDescriptor<Device> @this,
      string? ownerUserId) =>
    ownerUserId is null ? @this
    : @this.Term(f => f.OwnerData.OwnerUserId, ownerUserId);

  public static QueryContainer SourceTerm(
      this QueryContainerDescriptor<Device> @this,
      string? source) =>
    source is null ? @this
    : @this.Term(f => f.Source, source);

  public static QueryContainer Active(
      this QueryContainerDescriptor<Device> @this) =>
    @this.Term(t => t.StateData.State, DeviceState.Active);

  public static QueryContainer All(
      this QueryContainerDescriptor<Device> @this,
      bool all) =>
    all ? @this
    : @this.Term(t => t.StateData.State, DeviceState.Active);
}
