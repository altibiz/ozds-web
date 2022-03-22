namespace Elasticsearch.MyEnergyCommunity.Test {
  public partial class Client {
    private Elasticsearch.MyEnergyCommunity.Client _client { get; init; }

    public Client() {
      this._client = new Elasticsearch.MyEnergyCommunity.Client();
    }
  }
}
