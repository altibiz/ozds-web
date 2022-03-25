using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Nest;
using Elasticsearch.Net;

namespace Elasticsearch {
public sealed partial class Client : IClient {
  public Client()
      : this(new Uri(EnvironmentExtensions.AssertEnvironmentVariable(
                 "ELASTICSEARCH_SERVER_URI")),
            EnvironmentExtensions.AssertEnvironmentVariable(
                "ELASTICSEARCH_CA_PATH"),
            EnvironmentExtensions.AssertEnvironmentVariable("ELASTICSEARCH_USER"),
            EnvironmentExtensions.AssertEnvironmentVariable(
                "ELASTICSEARCH_PASSWORD")) {}

  public Client(Uri uri, string caPath, string user, string password) {
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
            .DefaultMappingFor<Loader.Log>(
                m => m.IndexName(s_loaderLogIndexName))
            .ServerCertificateValidationCallback(
                CertificateValidations.AuthorityIsRoot(
                    new X509Certificate(caPath)))
            .BasicAuthentication(user, password);

    Console.WriteLine($"Connecting {Source} to {uri}");
    _client = new ElasticClient(settings);
    var pingResponse = _client.Ping();
    if (!pingResponse.IsValid) {
      throw new WebException(
          $"Could not connect to Elasticsearch. Response: {pingResponse}");
    }

    // NOTE: this messes up tests because of parallelism
    // TODO: disable test parallelism?
    // TryDeleteIndicesIfDebug();

    TryCreateIndices();
  }

  ~Client() {
    // NOTE: this messes up tests because of parallelism
    // TODO: disable test parallelism?
    // TryDeleteIndicesIfDebug();
  }

  public void TryDeleteIndicesIfDebug() {
#if DEBUG
    Console.WriteLine("Trying to delete Elasticsearch indices...");
    _client.TryDeleteIndex(s_measurementsIndexName);
    _client.TryDeleteIndex(s_devicesIndexName);
    _client.TryDeleteIndex(s_loaderLogIndexName);
#endif
  }

  public void TryCreateIndices() {
    Console.WriteLine("Trying to create Elasticsearch indices...");
    _client.TryCreateIndex<Measurement>(s_measurementsIndexName);
    _client.TryCreateIndex<Device>(s_devicesIndexName);
    _client.TryCreateIndex<Loader.Log>(s_loaderLogIndexName);
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
}
}
