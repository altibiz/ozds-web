namespace Elasticsearch.MyEnergyCommunity.Test {
  public partial class Client {
    public const string TestOwnerId = "test-owner";
    public const string TestDeviceId = "M9EQCU59";

    public Client() {
      this._client = new Elasticsearch.MyEnergyCommunity.Client();
    }

    private Elasticsearch.MyEnergyCommunity.Client _client { get; init; }
  }
}
