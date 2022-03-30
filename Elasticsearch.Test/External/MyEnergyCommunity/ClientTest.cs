using Microsoft.Extensions.Logging;

namespace Elasticsearch.Test.MyEnergyCommunity;

using Elasticsearch.MyEnergyCommunity;

public partial class ClientTest {
  public ClientTest(IClient client, ILogger<ClientTest> logger) {
    Logger = logger;
    Client = client;
  }

  private IClient Client { get; }
  private ILogger Logger { get; }
}
