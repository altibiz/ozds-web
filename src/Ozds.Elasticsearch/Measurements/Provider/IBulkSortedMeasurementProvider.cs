namespace Ozds.Elasticsearch;

public interface IBulkSortedMeasurementProvider :
  IBulkMeasurementProvider,
  ISortedMeasurementProvider
{
  public IEnumerable<Measurement> GetMeasurementsSorted(
      Period? period = null);

  public Task<IEnumerable<Measurement>> GetMeasurementsSortedAsync(
      Period? period = null);
}
