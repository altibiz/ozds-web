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
    : @this.Term(f => f.DeviceData.DeviceId.Suffix("keyword"), deviceId);

  public static QueryContainer OwnerIdTerm(
      this QueryContainerDescriptor<Measurement> @this,
      string? ownerId) =>
    ownerId is null ? @this
    : @this.Term(f => f.DeviceData.OwnerId.Suffix("keyword"), ownerId);

  public static QueryContainer OwnerUserIdTerm(
      this QueryContainerDescriptor<Measurement> @this,
      string? ownerUserId) =>
    ownerUserId is null ? @this
    : @this.Term(f => f.DeviceData.OwnerUserId.Suffix("keyword"), ownerUserId);
}
