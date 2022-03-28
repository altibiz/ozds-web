using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Nest;
using Elasticsearch.Net;

namespace Elasticsearch {
public sealed partial class Client : IClient {
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
    _indexSuffix = indexSuffix ?? "";

    var settings =
        new ConnectionSettings(uri)
#if DEBUG
            .PrettyJson(true)
            .DisableDirectStreaming()
#else
            .PrettyJson(false)
#endif
            .DefaultIndex(s_measurementsIndexName)
            .DefaultMappingFor<Measurement>(
                m => m.IndexName(s_measurementsIndexName))
            .DefaultMappingFor<Device>(m => m.IndexName(s_devicesIndexName))
            .DefaultMappingFor<Log>(m => m.IndexName(s_loaderLogIndexName))
            .ServerCertificateValidationCallback(
                CertificateValidations.AuthorityIsRoot(
                    new X509Certificate(caPath)))
            .BasicAuthentication(user, password);

    Console.WriteLine($"Checking connection of {Source} to {uri}...");
    _client = new ElasticClient(settings);
    var pingResponse = _client.Ping();
    if (!pingResponse.IsValid) {
      throw new WebException(
          $"Could not connect to Elasticsearch. Response: {pingResponse}");
    }

    TryDeleteIndicesIfDebug();
    TryCreateIndices();
  }

  public void TryDeleteIndicesIfDebug() {
#if DEBUG
    var consoleSuffix =
        _indexSuffix == null ? "" : $" with suffix '{_indexSuffix}'";
    Console.WriteLine(
        $"Trying to delete Elasticsearch indices{consoleSuffix}...");
    _client.TryDeleteIndex(s_measurementsIndexName + _indexSuffix);
    _client.TryDeleteIndex(s_devicesIndexName + _indexSuffix);
    _client.TryDeleteIndex(s_loaderLogIndexName + _indexSuffix);
#endif
  }

  public void TryCreateIndices() {
    var consoleSuffix =
        _indexSuffix == null ? "" : $" with suffix '{_indexSuffix}'";
    Console.WriteLine(
        $"Trying to create Elasticsearch indices{consoleSuffix}...");
    _client.TryCreateIndex<Measurement>(s_measurementsIndexName + _indexSuffix);
    _client.TryCreateIndex<Device>(s_devicesIndexName + _indexSuffix);
    _client.TryCreateIndex<Log>(s_loaderLogIndexName + _indexSuffix);
  }

#if DEBUG
  private const string s_measurementsIndexName = "ozds.debug.measurements";
  private const string s_devicesIndexName = "ozds.debug.devices";
  private const string s_loaderLogIndexName = "ozds.debug.loader-log";
#else
  private const string s_measurementsIndexName = "ozds.measurements";
  private const string s_devicesIndexName = "ozds.devices";
  private const string s_loaderLogIndexName = "ozds.loader-log";
#endif

  private IElasticClient _client { get; init; }
  private string _indexSuffix { get; init; }
}
}
