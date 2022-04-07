using Microsoft.Extensions.Logging;

namespace Ozds.Elasticsearch.Test.HelbOzds;

using Ozds.Elasticsearch.HelbOzds;

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
