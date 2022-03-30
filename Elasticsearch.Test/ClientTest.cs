using Microsoft.Extensions.Logging;

namespace Elasticsearch.Test;

public partial class ClientTest {
  public ClientTest(Elasticsearch.IClient client,
      Elasticsearch.IMeasurementProviderIterator providers,
      ILogger<ClientTest> logger) {
    Logger = logger;

    Client = client;
    Providers = providers;
  }

  private IClient Client { get; }
  private IMeasurementProviderIterator Providers { get; }

  private ILogger Logger { get; }
}
