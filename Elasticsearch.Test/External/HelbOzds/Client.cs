namespace Elasticsearch.HelbOzds.Test {
  public partial class Client {
    public Client() { this._client = new Elasticsearch.HelbOzds.Client(); }

    private Elasticsearch.HelbOzds.Client _client { get; init; }
  }
}
