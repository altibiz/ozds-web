using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Elasticsearch.MyEnergyCommunity {
  public sealed partial class Client : IClient {
    public Client() : this(new Uri(s_defaultBaseUri)) {}

    public Client(Uri baseUri) {
      _client = new HttpClient();
      _client.BaseAddress = baseUri;
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));

      var pingTask = _client.GetAsync("/");
      pingTask.Wait();
      if (pingTask.Result.StatusCode != HttpStatusCode.OK) {
        throw new WebException($"Could not connect to {Source}\n" +
                               $"Status code: {pingTask.Result.StatusCode}");
      }
    }

#if DEBUG
    private const string s_defaultBaseUri = "dummy";
#else
    // TODO: something else?
    private const string s_defaultBaseUri = "dummy";
#endif

    private HttpClient _client;
  }
}
