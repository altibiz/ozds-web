namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  public ClientTest(
      IElasticsearchClient client,
      ILogger<ClientTest> logger)
  {
    Logger = logger;
    Client = client;
  }

  private IElasticsearchClient Client { get; }
  private ILogger Logger { get; }
}
