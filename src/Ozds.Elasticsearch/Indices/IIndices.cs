namespace Ozds.Elasticsearch;

public interface IIndices : IEnumerable<IIndex>
{
  public IEnumerable<IIndex> Yield();

  public INamedIndices GetNamed();
}
