using Nest;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public readonly record struct Indices
(Index Measurements,
 Index Devices,
 Index Log)
{
  public Indices(
      IEnumerable<string> indices,
      bool isDev,
      int? version = null) :
    this(
      Measurements:
        new Index(
          indices: indices,
          @base: Index.Measurements,
          isDev: isDev,
          version: version),
      Devices:
        new Index(
          indices: indices,
          @base: Index.Devices,
          isDev: isDev,
          version: version),
      Log:
        new Index(
          indices: indices,
          @base: Index.Log,
          isDev: isDev,
          version: version))
  { }

  public Indices(
      IEnumerable<Index> indices) :
    this(
      Measurements: indices
        .FirstOrDefault(index => index.Base == Index.Measurements),
      Devices: indices
        .FirstOrDefault(index => index.Base == Index.Devices),
      Log: indices
        .FirstOrDefault(index => index.Base == Index.Log))
  { }

  public Indices(
      bool isDev,
      int? version = null) :
    this(
      Measurements:
        new Index(
          @base: Index.Measurements,
          isDev: isDev,
          version: version),
      Devices:
        new Index(
          @base: Index.Devices,
          isDev: isDev,
          version: version),
      Log:
        new Index(
          @base: Index.Log,
          isDev: isDev,
          version: version))
  { }

  public Indices(
      bool isDev,
      string test) :
    this(
      Measurements:
        new Index(
          @base: Index.Measurements,
          isDev: isDev,
          test: test),
      Devices:
        new Index(
          @base: Index.Devices,
          isDev: isDev,
          test: test),
      Log:
        new Index(
          @base: Index.Log,
          isDev: isDev,
          test: test))
  { }

  public Indices(
      string measurements,
      string devices,
      string log) :
    this(
      Measurements: new Index(measurements),
      Devices: new Index(devices),
      Log: new Index(log))
  { }

  public IEnumerable<Index> Yield()
  {
    yield return Measurements;
    yield return Devices;
    yield return Log;
  }

  public Indices SwapVersions(
      int measurementsVersion,
      int devicesVersion,
      int logVersion) =>
    new Indices(
      Measurements: Measurements.WithVersion(measurementsVersion),
      Devices: Devices.WithVersion(devicesVersion),
      Log: Log.WithVersion(logVersion));
}

public readonly record struct Index
(string Name,
 string Base,
 bool IsDev,
 int? Version = null,
 string? Test = null)
{
  public const string BasePrefix = "ozds";
  public const string DevPrefix = "debug";
  public const string TestSuffix = "test";

  public const string Measurements = "measurements";
  public const string Devices = "devices";
  public const string Log = "log";

  public const int DefaultVersion = 0;

  public static readonly
  IReadOnlyDictionary<string, Func<
      IElasticClient, Index, Task<CreateIndexResponse>>>
  AsyncCreatorFor =
    new Dictionary<string, Func<
      IElasticClient, Index, Task<CreateIndexResponse>>>
    {
      {
        Measurements,
        (client, index) => client.Indices
          .CreateAsync(index.Name, i => i
            .Map<Measurement>(m => m
              .AutoMap<Measurement>()))
      },
      {
        Devices,
        (client, index) => client.Indices
          .CreateAsync(index.Name, i => i
            .Map<Device>(m => m
              .AutoMap<Device>()))
      },
      {
        Log,
        (client, index) => client.Indices
          .CreateAsync(index.Name, i => i
            .Map<MissingDataLog>(m => m
              .AutoMap<MissingDataLog>())
            .Map<LoadLog>(m => m
              .AutoMap<LoadLog>()))
      }
    };

  public static readonly
  IReadOnlyDictionary<string, Func<
      IElasticClient, Index, CreateIndexResponse>>
  CreatorFor =
    new Dictionary<string, Func<
      IElasticClient, Index, CreateIndexResponse>>
    {
      {
        Measurements,
        (client, index) => client.Indices
          .Create(index.Name, i => i
            .Map<Measurement>(m => m
              .AutoMap<Measurement>()))
      },
      {
        Devices,
        (client, index) => client.Indices
          .Create(index.Name, i => i
            .Map<Device>(m => m
              .AutoMap<Device>()))
      },
      {
        Log,
        (client, index) => client.Indices
          .Create(index.Name, i => i
            .Map<MissingDataLog>(m => m
              .AutoMap<MissingDataLog>())
            .Map<LoadLog>(m => m
              .AutoMap<LoadLog>()))
      }
    };

  public static readonly
  IReadOnlyDictionary<string, Func<
      IElasticClient, Index, Task<PutMappingResponse>>>
  AsyncMapperFor =
    new Dictionary<string, Func<
      IElasticClient, Index, Task<PutMappingResponse>>>
    {
      {
        Measurements,
        async (client, index) =>
          await client.Indices
            .PutMappingAsync<Measurement>(m => m
              .Index(index.Name)
              .AutoMap())
      },
      {
        Devices,
        async (client, index) =>
          await client.Indices
            .PutMappingAsync<Device>(m => m
              .Index(index.Name)
              .AutoMap())
      },
      {
        Log,
        async (client, index) =>
          await client.Indices
            .PutMappingAsync<LoadLog>(m => m
              .Index(index.Name)
              .AutoMap()) is { IsValid : false } response ?
          response
        : await client.Indices
            .PutMappingAsync<MissingDataLog>(m => m
              .Index(index.Name)
              .AutoMap())
      }
    };

  public static readonly
  IReadOnlyDictionary<string, Func<
      IElasticClient, Index, PutMappingResponse>>
  MapperFor =
    new Dictionary<string, Func<
      IElasticClient, Index, PutMappingResponse>>
    {
      {
        Measurements,
        (client, index) =>
          client.Indices
            .PutMapping<Measurement>(m => m
              .Index(index.Name)
              .AutoMap())
      },
      {
        Devices,
        (client, index) =>
          client.Indices
            .PutMapping<Device>(m => m
              .Index(index.Name)
              .AutoMap())
      },
      {
        Log,
        (client, index) =>
          client.Indices
            .PutMapping<LoadLog>(m => m
              .Index(index.Name)
              .AutoMap()) is { IsValid : false } response ?
          response
        : client.Indices
            .PutMapping<MissingDataLog>(m => m
              .Index(index.Name)
              .AutoMap())
      }
    };

  public static string MakeName(
      string @base,
      bool isDev,
      int? version = null) =>
    $"{MakePrefix(isDev)}.{@base}{MakeVersion(version)}";

  public static string MakeTestName(
      string @base,
      bool isDev,
      string test) =>
    $"{MakePrefix(isDev)}.{@base}.{TestSuffix}.{test}";

  public static string MakePrefix(
      bool isDev) =>
    isDev ? $"{BasePrefix}.{DevPrefix}" : $"{BasePrefix}";

  public static string MakeVersion(
      int? version) =>
    version is null ? "" : $".v{version}";

  public static bool IsTest(
      string name) =>
    name.Contains(TestSuffix);

  public Index(
      IEnumerable<string> indices,
      string @base,
      bool isDev,
      int? version = null) :
    this(
      name: ExtractLatest(indices, @base, isDev, version))
  { }

  public Index(
      string @base,
      bool isDev,
      int? version = null) :
    this(
      Name: MakeName(@base, isDev, version),
      Base: @base,
      IsDev: isDev,
      Version: version)
  { }

  public Index(
      string @base,
      bool isDev,
      string test) :
    this(
      Name: MakeTestName(@base, isDev, test),
      Base: @base,
      IsDev: isDev,
      Test: test)
  { }

  public Index(
      string name) :
    this(
      Name: name,
      Base: ExtractBase(name),
      IsDev: ExtractIsDev(name),
      Version: ExtractVersion(name),
      Test: ExtractTest(name))
  { }

  public Index WithVersion(
      int version) =>
    new Index(
      Name: MakeName(Base, IsDev, version),
      Base: Base,
      IsDev: IsDev,
      Version: version,
      Test: null);

  public static string ExtractLatest(
      IEnumerable<string> indices,
      string @base,
      bool isDev,
      int? version = null) =>
    indices
      .Where(index => index.Contains(@base))
      .MaxBy(ExtractVersion)
      .WhenNull(MakeName(@base, isDev, version));

  public static string ExtractBase(
      string name) =>
    name
      // NOTE: separate because the capture pushes the dev prefix out
      .RegexRemove($"^{BasePrefix}\\.(?:{DevPrefix}\\.)?")
      .RegexReplace($"^([a-z]*)\\..*$", @"$1");

  public static bool ExtractIsDev(
      string name) =>
    !name
      .RegexReplace($"^{BasePrefix}\\.({DevPrefix}\\.)?.*$", @"$1")
      .Empty();

  public static int? ExtractVersion(
      string name) =>
    name
      .RegexReplace(@"^.*v([0-9]*).*$", @"$1")
      .TryParseInt();

  public static string? ExtractTest(
      string name) =>
    name
      .RegexReplace($"^.*{TestSuffix}\\.([a-z\\.\\-]*)$", @"$1")
      .Var(test =>
          test == name ?
          null
        : test);
}

public static class IndexExtensions
{
  public static Indices Indices(
      this IEnumerable<Index> indices) =>
    new Indices(indices);
}
