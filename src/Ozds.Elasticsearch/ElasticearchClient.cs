using System.Net;
using System.Security.Cryptography.X509Certificates;
using Nest;
using Elasticsearch.Net;
using Ozds.Extensions;

namespace Ozds.Elasticsearch;

public interface IElasticsearchClientPrototype
{
  public IElasticsearchClient ClonePrototype(
      Indices? indices = null);
}

public sealed partial class ElasticsearchClient :
  IElasticsearchClientPrototype,
  IElasticsearchClient
{
  #region Constructors
  public ElasticsearchClient(
    IHostEnvironment env,
    ILogger<IElasticsearchClient> logger,
    IConfiguration conf,

    IEnumerable<IMeasurementProvider> providers,
    ElasticsearchMigratorAccessor migratorAccessor)
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

    MigratorAccessor = migratorAccessor;
    if (MigratorAccessor.Migrator is null)
    {
      Indices = new(isDev: Env.IsDevelopment());
    }
    else
    {
      Console.WriteLine("here");
      Indices = MigratorAccessor.Migrator.Migrate(Elastic);
    }
    Logger.LogInformation("Using {Indices}", Indices);
    TryCreateIndices();
    TryMapIndices();

    Ready = true;
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
            if (client is { Ready: true })
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
  #endregion // Constructors

  #region Prototype
  public IElasticsearchClient ClonePrototype(
      Indices? indices = null)
  {
    // NOTE: because the OnRequestCompleted lambda carries a reference to
    // NOTE: the prototype
    Ready = false;

    var client = new ElasticsearchClient(this, indices);

    // NOTE: because the OnRequestCompleted lambda carries a reference to
    // NOTE: the prototype
    Ready = true;

    return client;
  }

  private ElasticsearchClient(
      ElasticsearchClient other,
      Indices? indices)
  {
    Env = other.Env;
    Logger = other.Logger;

    Elastic = other.Elastic;

    Providers = other.Providers;

    MigratorAccessor = other.MigratorAccessor;

    Indices = indices ?? other.Indices;
    Logger.LogInformation("Using {Indices}", Indices);
    TryReconstructIndices();

    Ready = true;
  }
  #endregion // Prototype

  private IHostEnvironment Env { get; init; }
  private ILogger Logger { get; init; }

  private IElasticClient Elastic { get; init; }

  private List<IMeasurementProvider> Providers { get; init; }

  private ElasticsearchMigratorAccessor
  MigratorAccessor
  { get; init; }

  private bool Ready = false;

  #region Indices
  private Indices Indices { get; init; }

  private string MeasurementIndexName
  {
    get => Indices.Measurements.Name;
  }

  private string DeviceIndexName
  {
    get => Indices.Devices.Name;
  }

  private string LogIndexName
  {
    get => Indices.Log.Name;
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
      .Yield()
      .ForEach(index => Elastic.Indices.Delete(index.Name))
      .Run();

    Logger.LogInformation("Deleted Elasticsearch indices");
  }

  private void TryCreateIndices()
  {
    Indices
      .Yield()
      .ForEach(index => Index.CreatorFor[index.Base](Elastic, index))
      .Run();

    Logger.LogInformation("Created Elasticsearch indices");
  }

  private void TryMapIndices()
  {
    Indices
      .Yield()
      .ForEach(index => Index.MapperFor[index.Base](Elastic, index))
      .Run();

    Logger.LogInformation("Mapped Elasticsearch indices");
  }
  #endregion // Indices
}
