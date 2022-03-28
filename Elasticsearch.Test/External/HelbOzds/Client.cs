using Xunit;

namespace Elasticsearch.Test.HelbOzds {
  public class ClientFixture {
    public Elasticsearch.HelbOzds.IClient Client { get; init; } = 
      new Elasticsearch.HelbOzds.Client();
  }

  public partial class Client : IClassFixture<ClientFixture> {
    public Client(ClientFixture data) { this._client = data.Client; }

    private Elasticsearch.HelbOzds.IClient _client { get; init; }
  }
}
