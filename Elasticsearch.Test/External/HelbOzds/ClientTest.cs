using Xunit;

namespace Elasticsearch.Test.HelbOzds {
  public class ClientTestFixture {
    public Elasticsearch.HelbOzds.IClient Client { get; init; } = 
      new Elasticsearch.HelbOzds.Client();
  }

  public partial class ClientTest : IClassFixture<ClientTestFixture> {
    public ClientTest(ClientTestFixture data) { Client = data.Client; }

    private Elasticsearch.HelbOzds.IClient Client { get; init; }
  }
}
