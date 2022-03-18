using System;
using Nest;

namespace Elasticsearch {
public sealed partial class Client : IClient {
  public const string MeasurementsIndexName = "measurements";

  public Client() {
    var settings =
#if DEBUG
        new ConnectionSettings(new Uri("https://localhost:9200"))
            .PrettyJson(true)
#else
        new ConnectionSettings(new Uri("https://localhost:9200"))
            .PrettyJson(false)
#endif
            .DefaultIndex(MeasurementsIndexName);

    _client = new ElasticClient(settings);
  }

  private IElasticClient _client { get; }
}
}
