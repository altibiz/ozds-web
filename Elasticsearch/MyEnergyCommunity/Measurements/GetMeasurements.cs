using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace Elasticsearch.MyEnergyCommunity {
  public partial interface IClient : IMeasurementProvider {};

  public sealed partial class Client : IClient {
    public IEnumerable<Elasticsearch.Measurement> getMeasurements(
        string ownerId, string deviceId, DateTime? from = null,
        DateTime? to = null) {
      var task = getMeasurementsListAsync(ownerId, deviceId, from, to);
      task.Wait();
      return task.Result;
    }

    public async Task<IEnumerable<Elasticsearch.Measurement>>
    getMeasurementsAsync(string ownerId, string deviceId, DateTime? from = null,
        DateTime? to = null) {
      return await getMeasurementsListAsync(ownerId, deviceId, from, to);
    }

    public IEnumerable<Elasticsearch.Measurement> getMeasurementsSorted(
        string ownerId, string deviceId, DateTime? from = null,
        DateTime? to = null) {
      var task = getMeasurementsListAsync(ownerId, deviceId, from, to);
      task.Wait();
      return task.Result.OrderBy(m => m.deviceDateTime);
    }

    public async Task<IEnumerable<Elasticsearch.Measurement>>
    getMeasurementsSortedAsync(string ownerId, string deviceId,
        DateTime? from = null, DateTime? to = null) {
      return (await getMeasurementsListAsync(ownerId, deviceId, from, to))
          .OrderBy(m => m.deviceDateTime);
    }

    private async Task<IReadOnlyList<Elasticsearch.Measurement>>
    getMeasurementsListAsync(string ownerId, string deviceId,
        DateTime? from = null, DateTime? to = null) {
      var result = new List<Measurement> {};
      string? continuationToken = null;

      do {
        var request =
            new HttpRequestMessage(HttpMethod.Get, "measurements/" + deviceId);
        request.Headers.Add("OwnerId", ownerId);
        if (from != null && to != null) {
          request.Content = new FormUrlEncodedContent(
              new List<KeyValuePair<string ?, string?>> {
                new KeyValuePair<string ?, string?>("from", from.ToString()), 
                new KeyValuePair<string ?, string?>("to", to.ToString()), 
              });
        }

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
