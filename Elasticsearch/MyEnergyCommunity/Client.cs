using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Elasticsearch.MyEnergyCommunity {
  public sealed partial class Client : IClient {
    public const string DefaultServerBaseUri =
        "http://informel-sense.azurewebsites.net/api";

    public Client() : this(new Uri(DefaultServerBaseUri)) {}

    public Client(Uri baseUri) {
      _client = new HttpClient();
      _client.BaseAddress = baseUri;
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private const string MeasurementsIndexName = "ozdsMeasurements";
    private const string LoaderLogIndexName = "ozdsLoaderLog";

    private HttpClient _client;
  }
}
