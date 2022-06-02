using System.Net;
using System.Security.Cryptography.X509Certificates;
using Nest;
using Elasticsearch.Net;

namespace Ozds.Elasticsearch;

public interface IClientPrototype
{
  public IClient ClonePrototype(string? indexSuffix = null);
}

public sealed partial class Client : IClientPrototype, IClient
{

  #region Constructors
  public Client(
    IHostEnvironment env,
    ILogger<IClient> logger,
    IConfiguration conf,
    IEnumerable<IMeasurementProvider> providers)
  {
    Env = env;
    Logger = logger;

    Elasticsearch = CreateElasticClient(env, conf);
    var pingResponse = Elasticsearch.Ping();
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
    Logger.LogInformation($"Successfully connected to Elasticsearch");

    Providers = providers.ToList();
    if (Providers.Count > 0)
    {
      Logger.LogInformation(
          "Registered " +
          string.Join(
            ", ",
            Providers
              .Select(provider => provider.Source)) +
          " providers");
    }
    else
    {
      Logger.LogInformation("No providers registered");
    }

    TryCreateIndices();
  }

  public static bool Ping(
    IHostEnvironment env,
    IConfiguration conf)
  {
    var client = CreateElasticClient(env, conf);
    return client.Ping().IsValid;
  }

  private static IElasticClient CreateElasticClient(
      IHostEnvironment env,
      IConfiguration conf)
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

    var settings = new ConnectionSettings(new Uri(serverUri));

    if (!string.IsNullOrWhiteSpace(apiKey))
    {
      if (!string.IsNullOrWhiteSpace(apiKeyId))
      {
        settings = settings.ApiKeyAuthentication(
          new ApiKeyAuthenticationCredentials(apiKeyId, apiKey));
      }
      else
      {
        settings = settings.ApiKeyAuthentication(
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
        .PrettyJson(true)
        .DisableDirectStreaming();
    }

    return new ElasticClient(settings);
  }
  #endregion // Constructors

  #region Prototype
  public IClient ClonePrototype(string? indexSuffix = null)
  {
    return new Client(this, indexSuffix);
  }

  private Client(Client other, string? indexSuffix)
  {
    Env = other.Env;
    Logger = other.Logger;

    Elasticsearch = other.Elasticsearch;

    Providers = other.Providers;

    IndexSuffix = indexSuffix ?? "";
  }
  #endregion // Prototype

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }

  private IElasticClient Elasticsearch { get; init; }

  private List<IMeasurementProvider> Providers { get; }

  #region Index Suffix
  private string IndexSuffix
  {
    get => _indexSuffix;
    init
    {
      _indexSuffix =
        string.IsNullOrWhiteSpace(value) ? ""
        : value.StartsWith('.') ? value
        : $".{value}";
      TryReconstructIndices();
    }
  }

  private string ConsoleIndexSuffix
  {
    get =>
      string.IsNullOrWhiteSpace(IndexSuffix) ? ""
      : $" '{IndexSuffix}'";
  }

  private string _indexSuffix = "";
  #endregion // Index Suffix

  #region Indices
  private void TryReconstructIndices()
  {
    if (Env.IsDevelopment())
    {
      TryDeleteIndices();
    }
    TryCreateIndices();
  }

  private void TryDeleteIndices()
  {
    Elasticsearch.Indices.Delete(MeasurementIndexName);
    Elasticsearch.Indices.Delete(DeviceIndexName);
    Elasticsearch.Indices.Delete(LogIndexName);
    Logger.LogInformation(
        $"Deleted Elasticsearch indices{ConsoleIndexSuffix}");
  }

  private void TryCreateIndices()
  {
    Elasticsearch.Indices
      .Create(DeviceIndexName, c => c
        .Map<Measurement>(m => m
          .AutoMap<Measurement>()));

    Elasticsearch.Indices
      .Create(DeviceIndexName, c => c
        .Map<Device>(m => m
          .AutoMap<Device>()));

    Elasticsearch.Indices
      .Create(LogIndexName, c => c
        .Map<LoadLog>(m => m
          .AutoMap<LoadLog>())
        .Map<MissingDataLog>(m => m
          .AutoMap<MissingDataLog>()));

    Logger.LogInformation(
        $"Created Elasticsearch indices{ConsoleIndexSuffix}");
  }
  #endregion // Indices

  // NOTE: please don't touch this
  #region Index Names
  private string MeasurementIndexName
  {
    get => $"{EnvIndexPrefix}measurements{IndexSuffix}";
  }

  private string DeviceIndexName
  {
    get => $"{EnvIndexPrefix}devices{IndexSuffix}";
  }

  private string LogIndexName
  {
    get => $"{EnvIndexPrefix}log{IndexSuffix}";
  }

  private string EnvIndexPrefix
  {
    get => Env.IsDevelopment() ? "ozds.debug." : "ozds.";
  }
  #endregion // Index Names
}
