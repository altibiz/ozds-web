using System.Net;
using System.Net.Http.Headers;

namespace Ozds.Elasticsearch.MyEnergyCommunity;

public sealed partial class Client : IClient
{
  public const string MyEnergyCommunitySource = "MyEnergyCommunity";

  public Client(IConfiguration conf, ILogger<Client> logger)
  {
    Logger = logger;

    var section = conf
      .GetSection("Ozds")
      .GetSection("Elasticsearch")
      .GetSection("External")
      .GetSection("MyEnergyCommunity")
      .GetSection("Client");

    Http = new HttpClient();
    Http.BaseAddress = new Uri(section.GetNonNullValue<string>("serverUri"));
    Http.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

    var pingTask = Http.GetAsync("/");
    pingTask.Wait();
    if (pingTask.Result.StatusCode != HttpStatusCode.OK)
    {
      throw new WebException(
          $"Could not connect to {Source}\n" +
          $"Ping response code: {pingTask.Result.StatusCode}");
    }
    Logger.LogInformation(
        $"Successfully connected {Source} to {Http.BaseAddress}");
  }

  private HttpClient Http { get; }
  private ILogger Logger { get; }
}
