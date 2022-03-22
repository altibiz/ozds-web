using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace Elasticsearch.MyEnergyCommunity {
  public partial interface IClient : IMeasurementProvider {};

  public sealed partial class Client : IClient {
    public IEnumerable<Elasticsearch.Measurement> GetMeasurements(
        string ownerId, string deviceId, DateTime? from = null,
        DateTime? to = null) {
      var task = GetMeasurementsListAsync(ownerId, deviceId, from, to);
      task.Wait();
      return task.Result;
    }

    public async Task<IEnumerable<Elasticsearch.Measurement>>
    GetMeasurementsAsync(string ownerId, string deviceId, DateTime? from = null,
        DateTime? to = null) {
      return await GetMeasurementsListAsync(ownerId, deviceId, from, to);
    }

    public IEnumerable<Elasticsearch.Measurement> GetMeasurementsSorted(
        string ownerId, string deviceId, DateTime? from = null,
        DateTime? to = null) {
      var task = GetMeasurementsListAsync(ownerId, deviceId, from, to);
      task.Wait();
      return task.Result.OrderBy(m => m.deviceDateTime);
    }

    public async Task<IEnumerable<Elasticsearch.Measurement>>
    GetMeasurementsSortedAsync(string ownerId, string deviceId,
        DateTime? from = null, DateTime? to = null) {
      return (await GetMeasurementsListAsync(ownerId, deviceId, from, to))
          .OrderBy(m => m.deviceDateTime);
    }

    private async Task<IReadOnlyList<Elasticsearch.Measurement>>
    GetMeasurementsListAsync(string ownerId, string deviceId,
        DateTime? from = null, DateTime? to = null) {
      var result = new List<Measurement> {};
      string? continuationToken = null;

      do {
        var request = new HttpRequestMessage(
            HttpMethod.Get, "measurements/device/" + deviceId);
        request.Headers.Add("OwnerId", ownerId);
        if (from != null && to != null) {
          request.Content = new FormUrlEncodedContent(
              new List<KeyValuePair<string ?, string?>> {
                new KeyValuePair<string ?, string?>("from", from.ToString()), 
                new KeyValuePair<string ?, string?>("to", to.ToString()), 
              });
        }
        Console.Write(request);

        var response = await this._client.SendAsync(request);
        Console.Write(response);

        var responseContent = await response.Content.ReadAsStreamAsync();

        var measurementsResponse =
            await JsonSerializer.DeserializeAsync<Response<Measurement>>(
                responseContent);
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
