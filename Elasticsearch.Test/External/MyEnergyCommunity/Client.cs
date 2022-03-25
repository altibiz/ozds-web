namespace Elasticsearch.Test.MyEnergyCommunity {
  public partial class Client {
    public Client() {
      this._client = new Elasticsearch.MyEnergyCommunity.Client();
    }

    private Elasticsearch.MyEnergyCommunity.Client _client { get; init; }
  }
}
