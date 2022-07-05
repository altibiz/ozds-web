namespace Ozds.Elasticsearch;

public interface INamedIndices
{
  public IIndex Measurements { get; }
  public IIndex Devices { get; }
  public IIndex Log { get; }
}
