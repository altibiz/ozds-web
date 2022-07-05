using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public class IndexNamer : IIndexNamer
{
#region Indices
  public IIndices MakeIndices(
      IEnumerable<string> indices,
      int? version = null) =>
    indices
      .ToArray()
      .Var(indices =>
        new Indices(
          Measurements:
            MakeIndex(
              indices: indices,
              @base: Measurements,
              version: version),
          Devices:
            MakeIndex(
              indices: indices,
              @base: Devices,
              version: version),
          Log:
            MakeIndex(
              indices: indices,
              @base: Log,
              version: version)));

  public IIndices MakeIndices(
      IEnumerable<IIndex> indices) =>
    indices
      .ToArray()
      .Var(indices =>
        new Indices(
          Measurements: indices.First(index => index.Base == Measurements),
          Devices: indices.First(index => index.Base == Devices),
          Log: indices.First(index => index.Base == Log)));

  public IIndices MakeIndices(
      int? version = null) =>
    new Indices(
      Measurements:
        MakeIndex(
          @base: IndexNamer.Measurements,
          version: version),
      Devices:
        MakeIndex(
          @base: IndexNamer.Devices,
          version: version),
      Log:
        MakeIndex(
          @base: IndexNamer.Log,
          version: version));

  public IIndices MakeTestIndices(
      string test) =>
    new Indices(
      Measurements: MakeTestIndex(Measurements, test),
      Devices: MakeTestIndex(Devices, test),
      Log: MakeTestIndex(Log, test));
#endregion Indices

#region Index
  public IIndex MakeIndex(
      IEnumerable<string> indices,
      string @base,
      int? version = null) =>
    MakeIndex(
      ExtractLatest(
        indices,
        @base,
        version));

  public IIndex MakeIndex(
      string @base,
      int? version = null) =>
    new Index(
        Name: MakeName(@base, version),
        Base: @base,
        IsDev: Env.IsDevelopment(),
        Version: version);

  public IIndex MakeIndex(
      string name) =>
    new Index(
        Name: name,
        Base: ExtractBase(name),
        IsDev: ExtractIsDev(name),
        Version: ExtractVersion(name),
        Test: ExtractTest(name));

  public IIndex MakeTestIndex(
      string @base,
      string test) =>
    new Index(
        Name: MakeTestName(@base, test),
        Base: @base,
        IsDev: Env.IsDevelopment(),
        Test: test);

  public IIndex WithVersion(
      IIndex index,
      int version) =>
    new Index(
        Name: MakeName(index.Base, version),
        Base: index.Base,
        IsDev: Env.IsDevelopment(),
        Version: version);
#endregion Index

#region Make Component
  public string MakeName(
      string @base,
      int? version = null) =>
    $"{MakePrefix()}.{@base}{MakeVersion(version)}";

  public string MakeTestName(
      string @base,
      string test) =>
    $"{MakePrefix()}.{@base}.{TestSuffix}.{test}";

  public string MakePrefix() =>
    Env.IsDevelopment() ? $"{BasePrefix}.{DevPrefix}" : $"{BasePrefix}";

  public string MakeVersion(
      int? version) =>
    version is null or <= 0 ? "" : $".v{version}";
#endregion Make Component

#region Extract Component
  public string ExtractLatest(
      IEnumerable<string> indices,
      string @base,
      int? version = null) =>
    indices
      .Where(index => index.Contains(@base))
      .MaxBy(ExtractVersion)
      .WhenNull(MakeName(@base, version));

  public string ExtractBase(
      string name) =>
    name
      // NOTE: separate because the capture pushes the dev prefix out
      .RegexRemove($"^{BasePrefix}\\.(?:{DevPrefix}\\.)?")
      .RegexReplace($"^([a-z]*)\\..*$", @"$1");

  public bool ExtractIsDev(
      string name) =>
    !name
      .RegexReplace($"^{BasePrefix}\\.({DevPrefix}\\.)?.*$", @"$1")
      .Empty();

  public int? ExtractVersion(
      string name) =>
    name
      .RegexReplace(@"^.*v([0-9]*).*$", @"$1")
      .TryParseInt();

  public string? ExtractTest(
      string name) =>
    name
      .RegexReplace($"^.*{TestSuffix}\\.([a-z\\.\\-]*)$", @"$1")
      .Var(test =>
          test == name ?
          null
        : test);
#endregion Extract Component

#region Tests
  public bool IsTest(
      string name) =>
    name.Contains(TestSuffix);

  public bool IsDev(
      string name) =>
    name.Contains(DevPrefix);
#endregion Tests

  public const string BasePrefix = "ozds";
  public const string DevPrefix = "debug";
  public const string TestSuffix = "test";

  public const string Measurements = "measurements";
  public const string Devices = "devices";
  public const string Log = "log";

  public const int DefaultVersion = 0;

  public IndexNamer(
      IHostEnvironment env)
  {
    Env = env;
  }

  IHostEnvironment Env { get; init; }
}
