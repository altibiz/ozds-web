using Nest;

namespace Ozds.Elasticsearch;

internal interface IMigrationStore
{
  public IMigrationStore Migrate(
      string index,
      IEnumerable<IProcessor> processors);

  // TODO: readonly
  public Dictionary<string, List<IProcessor>> Processors { get; }
}

internal class MigrationStore : IMigrationStore
{
  public IMigrationStore Migrate(
      string index,
      IEnumerable<IProcessor> processors)
  {
    if (Migrations.TryGetValue(index, out var storedProcessors))
    {
      storedProcessors.AddRange(processors);
    }
    else
    {
      Migrations.Add(index, processors.ToList());
    }

    return this;
  }

  public Dictionary<string, List<IProcessor>> Processors
  {
    get => Migrations;
  }

  private Dictionary<string, List<IProcessor>> Migrations { get; } = new();
}
