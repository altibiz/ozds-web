using Microsoft.Extensions.Logging;

namespace Elasticsearch.Test.HelbOzds;

using Elasticsearch.HelbOzds;

public partial class ClientTest {
  public ClientTest(IClient client, ILogger<ClientTest> logger) {
    Logger = logger;
    Client = client;
  }

  private IClient Client { get; }
  private ILogger Logger { get; }
}
