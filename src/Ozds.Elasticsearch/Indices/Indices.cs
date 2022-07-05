using System.Collections;

namespace Ozds.Elasticsearch;

public readonly record struct Indices
(IIndex Measurements,
 IIndex Devices,
 IIndex Log) : IIndices, INamedIndices
{
  IEnumerator<IIndex> IEnumerable<IIndex>.GetEnumerator() =>
    Yield().GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() =>
    Yield().GetEnumerator();

  public IEnumerable<IIndex> Yield()
  {
    yield return Measurements;
    yield return Devices;
    yield return Log;
  }

  public INamedIndices GetNamed() =>
    this;
}
