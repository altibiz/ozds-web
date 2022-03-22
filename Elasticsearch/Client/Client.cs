using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Nest;
using Elasticsearch.Net;

namespace Elasticsearch {
public sealed partial class Client : IClient {
#if DEBUG
  public const string DefaultServerUri = "dummy";
  public const string DefaultCaPath = "dummy";
  public const string DefaultUser = "dummy";
  public const string DefaultPassword = "dummy";
#else
  // TODO: something else here
  public const string DefaultServerUri = "dummy";
  public const string DefaultCaPath = "dummy";
  public const string DefaultUser = "dummy";
  public const string DefaultPassword = "dummy";
#endif

  public Client()
      : this(new Uri(DefaultServerUri), DefaultCaPath, DefaultUser,
            DefaultPassword) {}

  public Client(Uri uri, string caPath, string user, string password) {
    var settings = new ConnectionSettings(uri)
#if DEBUG
                       .PrettyJson(true)
#else
                       .PrettyJson(false)
#endif
                       .DefaultIndex(MeasurementsIndexName)
                       .ServerCertificateValidationCallback(
                           CertificateValidations.AuthorityIsRoot(
                               new X509Certificate(caPath)))
                       .BasicAuthentication(user, password);

    _client = new ElasticClient(settings);
    var pingResponse = _client.Ping();
    if (!pingResponse.IsValid) {
      throw new WebException(String.Format(
          "Could not connect to Elasticsearch. Response: {0}", pingResponse));
    }
  }

  private const string MeasurementsIndexName = "ozdsMeasurements";
  private const string LoaderLogIndexName = "ozdsLoaderLog";

  private IElasticClient _client { get; init; }
}
}
