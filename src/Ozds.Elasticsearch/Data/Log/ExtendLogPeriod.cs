using Nest;

namespace Ozds.Elasticsearch;

public partial interface IClient
{
  public Task<UpdateResponse<LoadLog>> ExtendLoadLogPeriodAsync(
      Id id,
      DateTime to);

  public UpdateResponse<LoadLog> ExtendLoadLogPeriod(
      Id id,
      DateTime to);
};

public sealed partial class Client : IClient
{
  public Task<UpdateResponse<LoadLog>> ExtendLoadLogPeriodAsync(
      Id id,
      DateTime to) =>
    Elasticsearch.UpdateAsync<LoadLog, ExtendLogPeriodPartial>(
        id,
        d => d
          .Doc(new ExtendLogPeriodPartial(to))
          .Index(LogIndexName));

  public UpdateResponse<LoadLog> ExtendLoadLogPeriod(
      Id id,
      DateTime to) =>
    Elasticsearch.Update<LoadLog, ExtendLogPeriodPartial>(
        id,
        d => d
          .Doc(new ExtendLogPeriodPartial(to))
          .Index(LogIndexName));
}

internal class ExtendLogPeriodPartial
{
  public ExtendLogPeriodPartial(
      DateTime to)
  {
    Period =
      new PeriodPartial
      {
        To = to
      };
  }

  public PeriodPartial Period { get; init; }

  internal class PeriodPartial
  {
    public DateTime To { get; init; }
  }
}
