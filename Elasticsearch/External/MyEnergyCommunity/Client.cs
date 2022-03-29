using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Elasticsearch.MyEnergyCommunity {
  public sealed partial class Client : IClient {
    public Client()
        : this(new Uri(EnvironmentExtensions.AssertEnvironmentVariable(
              "MY_ENERGY_COMMUNITY_SERVER_URI"))) {}

    public Client(Uri baseUri) {
      Http = new HttpClient();
      Http.BaseAddress = baseUri;
      Http.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));

      Console.WriteLine($"Checking connection of {Source} to {baseUri}...");
      var pingTask = Http.GetAsync("/");
      pingTask.Wait();
      if (pingTask.Result.StatusCode != HttpStatusCode.OK) {
        throw new WebException($"Could not connect to {Source}\n" +
                               $"Status code: {pingTask.Result.StatusCode}");
      }
    }

    private HttpClient Http { get; init; }
  }
}
