namespace Elasticsearch.Test {
  public partial class Client {
    public Client() {
      _client = new Elasticsearch.Client();
      _measurementProviderIterator =
          new Elasticsearch.ExternalMeasurementProviderIterator();
    }

    private Elasticsearch.IClient _client { get; init; }
    private Elasticsearch
        .IMeasurementProviderIterator _measurementProviderIterator { get; init;}
  }

}
