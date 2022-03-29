using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Nest;
using Elasticsearch.Net;

namespace Elasticsearch {
public partial interface IClient {
  public IClient WithIndexSuffix(string? suffix = null);
}

public sealed partial class Client : IClient {
#region Constructors
  public Client(string? indexSuffix = null)
      : this(new Uri(EnvironmentExtensions.AssertEnvironmentVariable(
                 "ELASTICSEARCH_SERVER_URI")),
            EnvironmentExtensions.AssertEnvironmentVariable(
                "ELASTICSEARCH_CA_PATH"),
            EnvironmentExtensions.AssertEnvironmentVariable(
                "ELASTICSEARCH_USER"),
            EnvironmentExtensions.AssertEnvironmentVariable(
                "ELASTICSEARCH_PASSWORD"),
            indexSuffix) {}

  public Client(Uri uri, string caPath, string user, string password,
      string? indexSuffix = null) {
    var settings = new ConnectionSettings(uri)
#if DEBUG
                       .PrettyJson(true)
                       .DisableDirectStreaming()
#else
                       .PrettyJson(false)
#endif
                       .ServerCertificateValidationCallback(
                           CertificateValidations.AuthorityIsRoot(
                               new X509Certificate(caPath)))
                       .BasicAuthentication(user, password);

    Console.WriteLine($"Checking connection of {Source} to {uri}...");
    Elasticsearch = new ElasticClient(settings);
    var pingResponse = Elasticsearch.Ping();
    if (!pingResponse.IsValid) {
      throw new WebException(
          $"Could not connect to Elasticsearch. Response: {pingResponse}");
    }

    IndexSuffix = indexSuffix ?? "";
  }
#endregion // Constructors

#region WithIndexSuffix
  public IClient WithIndexSuffix(string? indexSuffix = null) {
    return new Client(this, indexSuffix);
  }

  private Client(Client other, string? indexSuffix) {
    Elasticsearch = other.Elasticsearch;
    IndexSuffix = indexSuffix ?? "";
  }
#endregion // WithIndexSuffix

  private IElasticClient Elasticsearch { get; init; }

#region Index Suffix
  private string IndexSuffix {
    get => _indexSuffix;
    init {
      _indexSuffix = String.IsNullOrWhiteSpace(value) ? ""
                     : value.StartsWith('.')          ? value
                                                      : $".{value}";

#if DEBUG
      Console.WriteLine($"Trying to delete indices{ConsoleIndexSuffix}...");
      Elasticsearch.TryDeleteIndex(MeasurementIndexName);
      Elasticsearch.TryDeleteIndex(DeviceIndexName);
      Elasticsearch.TryDeleteIndex(LogIndexName);
#endif

      Console.WriteLine($"Trying to create indices{ConsoleIndexSuffix}...");
      Elasticsearch.TryCreateIndex<Measurement>(MeasurementIndexName);
      Elasticsearch.TryCreateIndex<Device>(DeviceIndexName);
      Elasticsearch.TryCreateIndex<Log>(LogIndexName);
    }
  }

  private string ConsoleIndexSuffix {
    get => String.IsNullOrWhiteSpace(IndexSuffix) ? "" : $" '{IndexSuffix}'";
  }

  private string _indexSuffix = "";
#endregion // Index Suffix

#region Index Names
  private string MeasurementIndexName {
    get => s_measurementIndexPrefix + IndexSuffix;
  }

  private string DeviceIndexName { get => s_deviceIndexPrefix + IndexSuffix; }

  private string LogIndexName { get => s_logIndexPrefix + IndexSuffix; }

#if DEBUG
  private const string s_measurementIndexPrefix = "ozds.debug.measurements";
  private const string s_deviceIndexPrefix = "ozds.debug.devices";
  private const string s_logIndexPrefix = "ozds.debug.log";
#else
  private const string s_measurementIndexPrefix = "ozds.measurements";
  private const string s_deviceIndexPrefix = "ozds.devices";
  private const string s_logIndexPrefix = "ozds.log";
#endif
#endregion // Index Names
}
}
