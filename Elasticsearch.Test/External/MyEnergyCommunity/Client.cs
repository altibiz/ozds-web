using Xunit;

namespace Elasticsearch.Test.MyEnergyCommunity {
  public class ClientFixture {
    public Elasticsearch.MyEnergyCommunity.IClient Client { get; init; } = 
      new Elasticsearch.MyEnergyCommunity.Client();
  }

  public partial class Client : IClassFixture<ClientFixture> {
    public Client(ClientFixture data) { this._client = data.Client; }

    private Elasticsearch.MyEnergyCommunity.IClient _client { get; init; }
  }
}
