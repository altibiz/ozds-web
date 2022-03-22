using System;
using Nest;

namespace Elasticsearch {
public sealed partial class Client : IClient {
#if DEBUG
  public const string DefaultServerUri = "http://localhost:9200";
#else
  // TODO: something else here
  public const string DefaultServerUri = "http://localhost:9200";
#endif

  public Client() : this(new Uri(DefaultServerUri)) {}

  public Client(Uri uri) {
    var settings = new ConnectionSettings(uri)
#if DEBUG
                       .PrettyJson(true)
#else
                       .PrettyJson(false)
#endif
                       .DefaultIndex(MeasurementsIndexName);

    _client = new ElasticClient(settings);
  }

  private const string MeasurementsIndexName = "ozdsMeasurements";
  private const string LoaderLogIndexName = "ozdsLoaderLog";

  private IElasticClient _client { get; init; }
}
}
