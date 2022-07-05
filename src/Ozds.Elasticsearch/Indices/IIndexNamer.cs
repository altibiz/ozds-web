namespace Ozds.Elasticsearch;

public interface IIndexNamer
{
#region Indices
  public IIndices MakeIndices(
      IEnumerable<string> indices,
      int? version = null);

  public IIndices MakeIndices(
      IEnumerable<IIndex> indices);

  public IIndices MakeIndices(
      int? version = null);

  public IIndices MakeTestIndices(
      string test);
#endregion Indices

#region Index
  public IIndex MakeIndex(
      IEnumerable<string> indices,
      string @base,
      int? version = null);

  public IIndex MakeIndex(
      string @base,
      int? version = null);

  public IIndex MakeTestIndex(
      string @base,
      string test);

  public IIndex MakeIndex(
      string name);

  public IIndex WithVersion(
      IIndex index,
      int version);
#endregion Index

#region Make Component
  public string MakeName(
      string @base,
      int? version = null);

  public string MakeTestName(
      string @base,
      string test);

  public string MakePrefix();

  public string MakeVersion(
      int? version);
#endregion Make Component

#region Extract Component
  public string ExtractLatest(
      IEnumerable<string> indices,
      string @base,
      int? version = null);

  public string ExtractBase(
      string name);

  public bool ExtractIsDev(
      string name);

  public int? ExtractVersion(
      string name);

  public string? ExtractTest(
      string name);
#endregion Extract Component

#region Tests
  public bool IsTest(
      string name);

  public bool IsDev(
      string name);
#endregion Tests
}
