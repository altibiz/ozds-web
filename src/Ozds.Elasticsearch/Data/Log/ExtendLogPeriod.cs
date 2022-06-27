using Nest;

namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient
{
  public Task<UpdateResponse<LoadLog>>
  ExtendLoadLogPeriodAsync(
      Id id,
      DateTime to);

  public UpdateResponse<LoadLog>
  ExtendLoadLogPeriod(
      Id id,
      DateTime to);

  public Task<UpdateResponse<LoadLog>>
  ExtendLoadLogPeriodWithLastValidationAsync(
      Id id,
      DateTime to);

  public UpdateResponse<LoadLog>
  ExtendLoadLogPeriodWithLastValidation(
      Id id,
      DateTime to);
};

public sealed partial class ElasticsearchClient : IElasticsearchClient
{
  public Task<UpdateResponse<LoadLog>>
  ExtendLoadLogPeriodAsync(
      Id id,
      DateTime to) =>
    Elastic.UpdateAsync<LoadLog, ExtendLoadLogPeriodPartial>(
        id,
        u => u
          .Doc(new ExtendLoadLogPeriodPartial(to))
          .RefreshInDevelopment(Env)
          .Index(LogIndexName));

  public UpdateResponse<LoadLog>
  ExtendLoadLogPeriod(
      Id id,
      DateTime to) =>
    Elastic.Update<LoadLog, ExtendLoadLogPeriodPartial>(
        id,
        u => u
          .Doc(new ExtendLoadLogPeriodPartial(to))
          .RefreshInDevelopment(Env)
          .Index(LogIndexName));

  public Task<UpdateResponse<LoadLog>>
  ExtendLoadLogPeriodWithLastValidationAsync(
      Id id,
      DateTime to) =>
    Elastic.UpdateAsync<LoadLog, ExtendLoadLogPeriodWithLastValidationPartial>(
        id,
        u => u
          .Doc(new ExtendLoadLogPeriodWithLastValidationPartial(to))
          .RefreshInDevelopment(Env)
          .Index(LogIndexName));

  public UpdateResponse<LoadLog>
  ExtendLoadLogPeriodWithLastValidation(
      Id id,
      DateTime to) =>
    Elastic.Update<LoadLog, ExtendLoadLogPeriodWithLastValidationPartial>(
        id,
        u => u
          .Doc(new ExtendLoadLogPeriodWithLastValidationPartial(to))
          .RefreshInDevelopment(Env)
          .Index(LogIndexName));
}

internal class ExtendLoadLogPeriodPartial
{
  public ExtendLoadLogPeriodPartial(
      DateTime to)
  {
    Period =
      new PeriodPartial
      {
        To = to
      };
  }

  [Object(Name = "period")]
  public PeriodPartial Period { get; init; }
}

internal class ExtendLoadLogPeriodWithLastValidationPartial
{
  public ExtendLoadLogPeriodWithLastValidationPartial(
      DateTime to)
  {
    Period =
      new PeriodPartial
      {
        To = to
      };

    LastValidation = to;
  }

  [Object(Name = "period")]
  public PeriodPartial Period { get; init; }

  [Date(Name = "lastValidation")]
  public DateTime LastValidation { get; init; }
}

internal class PeriodPartial
{
  [Date(Name = "to")]
  public DateTime To { get; init; }
}
