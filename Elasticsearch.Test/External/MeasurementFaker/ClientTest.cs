using Xunit;

namespace Elasticsearch.Test.MeasurementFaker {
  public class ClientTestFixture {
    public Elasticsearch.MeasurementFaker.IClient Client { get; init; } =
      new Elasticsearch.MeasurementFaker.Client();
  }

  public partial class ClientTest : IClassFixture<ClientTestFixture> {
    public ClientTest(ClientTestFixture fixture) { Client = fixture.Client; }

    public Elasticsearch.MeasurementFaker.IClient Client { get; init; }
  }
}
