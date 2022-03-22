namespace Elasticsearch.HelbOzds.Test {
  public partial class Client {
    public const string TestOwnerId = "test-owner";
    public const string TestDeviceId = "M9EQCU59";

    public Client() { this._client = new Elasticsearch.HelbOzds.Client(); }

    private Elasticsearch.HelbOzds.Client _client { get; init; }
  }
}
