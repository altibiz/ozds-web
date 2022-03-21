using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace Elasticsearch.MyEnergyCommunity {
  public partial interface IClient {
    public IEnumerable<Measurement> getMeasurements(
        string ownerId, string deviceId, DateTime from, DateTime to);

    public Task<IEnumerable<Measurement>> getMeasurementsAsync(
        string ownerId, string deviceId, DateTime from, DateTime to);
  };

  public sealed partial class Client : IClient {
    public IEnumerable<Measurement> getMeasurements(
        string ownerId, string deviceId, DateTime from, DateTime to) {
      var task = getMeasurementsAsync(ownerId, deviceId, from, to);
      task.Wait();
      return task.Result;
    }

    public async Task<IEnumerable<Measurement>> getMeasurementsAsync(
        string ownerId, string deviceId, DateTime from, DateTime to) {
      var result = new List<Measurement> {};
      string? continuationToken = null;

      do {
        var request =
            new HttpRequestMessage(HttpMethod.Get, "measurements/" + deviceId);
        request.Headers.Add("OwnerId", ownerId);
        request.Content = new FormUrlEncodedContent(
            new List<KeyValuePair<string ?, string?>> {
              new KeyValuePair<string ?, string?>("from", from.ToString()), 
              new KeyValuePair<string ?, string?>("to", to.ToString()), 
            });

        var responseContentTask =
            await this._client.Send(request).Content.ReadAsStringAsync();
        var measurementsResponse =
            JsonSerializer.Deserialize<Response<Measurement>>(
                responseContentTask);
        if (measurementsResponse == null) {
          continue;
        }

        continuationToken = measurementsResponse.continuationToken;
        result.AddRange(measurementsResponse.items);
      } while (continuationToken != null);

      return result;
    }
  }
}
