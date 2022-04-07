using Microsoft.Extensions.Logging;

namespace Ozds.Elasticsearch.Test.MyEnergyCommunity;

using Ozds.Elasticsearch.MyEnergyCommunity;

public partial class ClientTest
{
  public ClientTest(IClient client, ILogger<ClientTest> logger)
  {
    Logger = logger;
    Client = client;
  }

  private IClient Client { get; }
  private ILogger Logger { get; }
}
