namespace Ozds.Elasticsearch;

public interface IIndex
{
  public string Name { get; }

  public string Base { get; }
  public bool IsDev { get; }

  public int? Version { get; }
  public string? Test { get; }
}
