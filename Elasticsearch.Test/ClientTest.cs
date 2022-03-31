using Microsoft.Extensions.Logging;

namespace Elasticsearch.Test;

public partial class ClientTest {
  public ClientTest(Elasticsearch.IClient client, ILogger<ClientTest> logger) {
    Logger = logger;

    Client = client;
  }

  private IClient Client { get; }

  private ILogger Logger { get; }
}
