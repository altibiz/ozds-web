namespace Elasticsearch.Test {
  public partial class Client {
    private Elasticsearch.Client _client { get; init; }

    public Client() { this._client = new Elasticsearch.Client(); }
  }
}
