using Nest;

namespace Ozds.Elasticsearch;

internal class MigrationStore : IMigrationStore
{
  public IMigrationStore Migrate(
      string index,
      IEnumerable<IProcessor>? processors = null)
  {
    processors = processors ?? Enumerable.Empty<IProcessor>();
    if (Processors.TryGetValue(index, out var storedProcessors))
    {
      storedProcessors.AddRange(processors);
    }
    else
    {
      Processors.Add(index, processors.ToList());
    }

    return this;
  }

  public Dictionary<string, List<IProcessor>> Processors { get; } = new();
}
