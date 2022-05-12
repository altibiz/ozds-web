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
    ILogger<Client> logger,
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
          $"Could not connect to {Source}\n" +
          $"Ping response debug information: {pingResponse.DebugInformation}");
      }
      else
      {
        throw new WebException(
          $"Could not connect to {Source}\n" +
          $"Ping response: {pingResponse}");
      }
    }
    Logger.LogInformation($"Successfully connected to {Source}");

    Providers = providers.ToList();

    TryReconstructIndices();
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

    var settings = new ConnectionSettings(new Uri(serverUri));

    if (user is not null && password is not null)
    {
      settings = settings
        .BasicAuthentication(user, password);
    }

    if (caPath is not null)
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
    Elasticsearch.TryDeleteIndex(MeasurementIndexName);
    Elasticsearch.TryDeleteIndex(DeviceIndexName);
    Elasticsearch.TryDeleteIndex(LogIndexName);
    Logger.LogInformation($"Deleted Elasticsearch indices{ConsoleIndexSuffix}");
  }

  private void TryCreateIndices()
  {
    Elasticsearch.TryCreateIndex<Measurement>(MeasurementIndexName);
    Elasticsearch.TryCreateIndex<Device>(DeviceIndexName);
    Elasticsearch.TryCreateIndex<Log>(LogIndexName);
    Logger.LogInformation($"Created Elasticsearch indices{ConsoleIndexSuffix}");
  }
  #endregion // Indices

  #region Index Names
  private string MeasurementIndexName
  {
    get => s_measurementIndexDebugPrefix + IndexSuffix;
  }

  private string DeviceIndexName
  {
    get => s_deviceIndexDebugPrefix + IndexSuffix;
  }

  private string LogIndexName
  {
    get => s_logIndexDebugPrefix + IndexSuffix;
  }

#if DEBUG
  private const string s_measurementIndexDebugPrefix =
    "ozds.debug.measurements";
  private const string s_deviceIndexDebugPrefix =
    "ozds.debug.devices";
  private const string s_logIndexDebugPrefix =
    "ozds.debug.log";
#else
  private const string s_measurementIndexDebugPrefix =
    "ozds.measurements";
  private const string s_deviceIndexDebugPrefix =
    "ozds.devices";
  private const string s_logIndexDebugPrefix =
    "ozds.log";
#endif
  #endregion // Index Names
}
