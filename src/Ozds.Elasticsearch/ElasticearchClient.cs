using System.Net;
using System.Security.Cryptography.X509Certificates;
using Nest;
using Elasticsearch.Net;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public interface IElasticsearchTestClientPrototype
{
  public IElasticsearchClient MakeTestClient(IIndices? indices = null);
}

public partial interface IElasticsearchClient
{
  // TODO: from NEST?
  public const int MaxSize = 10000;
}

public sealed partial class ElasticsearchClient :
  IElasticsearchTestClientPrototype,
  IElasticsearchClient
{
  #region Constructors
  public ElasticsearchClient(
    IHostEnvironment env,
    ILogger<IElasticsearchClient> logger,
    IConfiguration conf,

    IIndexNamer namer,
    IIndexMapper mapper,
    IEnumerable<IMeasurementProvider> providers,
    IElasticsearchMigratorAccessor migratorAccessor)
  {
    Env = env;
    Logger = logger;

    Elastic = CreateElasticClient(env, conf, this);
    var pingResponse = Elastic.Ping();
    if (!pingResponse.IsValid)
    {
      if (Env.IsDevelopment())
      {
        throw new WebException(
          $"Could not connect to Elasticsearch\n" +
          $"Ping response debug information: {pingResponse.DebugInformation}");
      }
      else
      {
        throw new WebException(
          $"Could not connect to Elasticsearch\n" +
          $"Ping response: {pingResponse}");
      }
    }
    Logger.LogInformation($"Connected to Elasticsearch");

    Providers = providers.ToList();
    if (Providers.Count > 0)
    {
      Logger.LogInformation(
        string.Format(
          "Registered {0} {1}",
          string.Join(
            ", ",
            Providers
              .Select(provider => $"'{provider.Source}'")),
          Providers.Count() > 1 ? "providers"
          : "provider"));
    }
    else
    {
      Logger.LogInformation("No providers registered");
    }

    Namer = namer;
    Mapper = mapper;

    MigratorAccessor = migratorAccessor;
    if (MigratorAccessor.Migrator is null)
    {
      Indices = Namer.MakeIndices();
    }
    else
    {
      Indices = MigratorAccessor.Migrator.Migrate(Elastic);
    }
    NamedIndices = Indices.GetNamed();
    Logger.LogInformation("Using {Indices}", NamedIndices);
    TryCreateIndices();
    TryMapIndices();

    ShouldLog = true;
  }

  public static bool Ping(
    IHostEnvironment env,
    IConfiguration conf,
    ILogger log)
  {
    var client = CreateElasticClient(env, conf);
    var response = client.Ping();

    log.LogDebug("Pinged Elastic\n{Response}", response.DebugInformation);

    return response.IsValid;
  }

  private static IElasticClient CreateElasticClient(
      IHostEnvironment env,
      IConfiguration conf,
      ElasticsearchClient? client = null)
  {
    var section = conf
      .GetSection("Ozds")
      .GetSection("Elasticsearch")
      .GetSection("Client");
    var serverUri = section.GetNonNullValue<string>("serverUri");
    var user = section.GetValue<string?>("user");
    var password = section.GetValue<string?>("password");
    var caPath = section.GetValue<string?>("caPath");
    var apiKey = section.GetValue<string?>("apiKey");
    var apiKeyId = section.GetValue<string?>("apiKeyId");

    var settings =
      new ConnectionSettings(new Uri(serverUri))
        .OnRequestCompleted(
          call =>
          {
            if (client is { ShouldLog: true })
            {
              client.Logger.LogDebug(call.DebugInformation);
            }
          })
        .EnableApiVersioningHeader();

    if (!string.IsNullOrWhiteSpace(apiKey))
    {
      if (!string.IsNullOrWhiteSpace(apiKeyId))
      {
        settings = settings
          .ApiKeyAuthentication(
            new ApiKeyAuthenticationCredentials(apiKeyId, apiKey));
      }
      else
      {
        settings = settings
          .ApiKeyAuthentication(
            new ApiKeyAuthenticationCredentials(apiKey));
      }
    }

    if (!string.IsNullOrWhiteSpace(user) &&
        !string.IsNullOrWhiteSpace(password))
    {
      settings = settings
        .BasicAuthentication(user, password);
    }

    if (!string.IsNullOrWhiteSpace(caPath) &&
        File.Exists(caPath))
    {
      settings = settings
        .ServerCertificateValidationCallback(
          CertificateValidations.AuthorityIsRoot(
            new X509Certificate(caPath)));
    }

    if (env.IsDevelopment())
    {
      settings = settings
        .PrettyJson()
        .DisableDirectStreaming();
    }

    return new ElasticClient(settings);
  }
  #endregion Constructors

  #region Prototype
  public IElasticsearchClient MakeTestClient(
      IIndices? indices = null)
  {
    // NOTE: because the OnRequestCompleted lambda carries a reference to
    // NOTE: the prototype
    ShouldLog = false;

    var client = new ElasticsearchClient(this, indices);

    // NOTE: because the OnRequestCompleted lambda carries a reference to
    // NOTE: the prototype
    ShouldLog = true;

    return client;
  }

  private ElasticsearchClient(
      ElasticsearchClient other,
      IIndices? indices)
  {
    Env = other.Env;
    Logger = other.Logger;

    Elastic = other.Elastic;

    Providers = other.Providers;

    Namer = other.Namer;
    Mapper = other.Mapper;

    MigratorAccessor = other.MigratorAccessor;

    Indices = indices ?? other.Indices;
    NamedIndices = Indices.GetNamed();
    Logger.LogInformation("Using {Indices}", NamedIndices);
    TryReconstructIndices();

    ShouldLog = true;
  }
  #endregion Prototype

  #region Members
  private IHostEnvironment Env { get; init; }
  private ILogger Logger { get; init; }

  private IElasticClient Elastic { get; init; }

  private List<IMeasurementProvider> Providers { get; init; }

  private IIndexNamer Namer { get; init; }
  private IIndexMapper Mapper { get; init; }

  private IElasticsearchMigratorAccessor MigratorAccessor { get; init; }

  private IIndices Indices { get; init; }
  private INamedIndices NamedIndices { get; init; }

  private bool ShouldLog = false;
  #endregion Members

  #region Indices
  private string MeasurementIndexName
  {
    get => NamedIndices.Measurements.Name;
  }

  private string DeviceIndexName
  {
    get => NamedIndices.Devices.Name;
  }

  private string LogIndexName
  {
    get => NamedIndices.Log.Name;
  }

  private void TryReconstructIndices()
  {
    TryDeleteIndices();
    TryCreateIndices();
    TryMapIndices();
  }

  private void TryDeleteIndices()
  {
    Indices
      .ForEach(index => Mapper.DeleterFor[index.Base](Elastic, index))
      .Run();

    Logger.LogInformation("Deleted Elasticsearch indices");
  }

  private void TryCreateIndices()
  {
    Indices
      .ForEach(index => Mapper.CreatorFor[index.Base](Elastic, index))
      .Run();

    Logger.LogInformation("Created Elasticsearch indices");
  }

  private void TryMapIndices()
  {
    Indices
      .ForEach(index => Mapper.MapperFor[index.Base](Elastic, index))
      .Run();

    Logger.LogInformation("Mapped Elasticsearch indices");
  }
  #endregion Indices
}
