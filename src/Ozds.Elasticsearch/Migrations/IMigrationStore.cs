using Nest;

namespace Ozds.Elasticsearch;

internal interface IMigrationStore
{
  public IMigrationStore Migrate(
      string index,
      IEnumerable<IProcessor>? processors = null);

  // TODO: readonly
  public Dictionary<string, List<IProcessor>> Processors { get; }
}
