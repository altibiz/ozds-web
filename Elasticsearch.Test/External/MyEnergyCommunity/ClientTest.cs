using Xunit;

namespace Elasticsearch.Test.MyEnergyCommunity {
  public class ClientTestFixture {
    public Elasticsearch.MyEnergyCommunity.IClient Client { get; init; } = 
      new Elasticsearch.MyEnergyCommunity.Client();
  }

  public partial class ClientTest : IClassFixture<ClientTestFixture> {
    public ClientTest(ClientTestFixture data) { Client = data.Client; }

    private Elasticsearch.MyEnergyCommunity.IClient Client { get; init; }
  }
}
