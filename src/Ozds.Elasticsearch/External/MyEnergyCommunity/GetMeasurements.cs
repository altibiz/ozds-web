using System.Text.Json;
// NOTE: QueryBuilder
// TODO: dont use AspNetCore?
using Microsoft.AspNetCore.Http.Extensions;

namespace Ozds.Elasticsearch.MyEnergyCommunity;

public partial interface IClient : IMeasurementProvider { };

public sealed partial class Client : IClient
{
  public string Source { get => Client.s_source; }

  public IEnumerable<Ozds.Elasticsearch.Measurement> GetMeasurements(
      Device device, Period? period = null)
  {
    var task = GetElasticsearchMeasurementsAsync(device, period);
    task.Wait();
    return task.Result;
  }

  public async Task<IEnumerable<Ozds.Elasticsearch.Measurement>>
  GetMeasurementsAsync(Device device, Period? period = null)
  {
    return await GetElasticsearchMeasurementsAsync(device, period);
  }

  private async Task<IEnumerable<Ozds.Elasticsearch.Measurement>>
  GetElasticsearchMeasurementsAsync(Device device, Period? period = null)
  {
    return (await GetNativeMeasurementsAsync(device, period))
        .Select(ConvertMeasurement);
  }

  private async Task<List<Measurement>> GetNativeMeasurementsAsync(
      Device device, Period? period = null)
  {
    var result = new List<Measurement> { };
    string? continuationToken = null;

    do
    {
      var uri = "v1/measurements/device/" + device.SourceDeviceId;
      var query = new QueryBuilder();
      if (period?.From != null)
        query.Add("from", period.From.ToUtcIsoString());
      if (period?.To != null)
        query.Add("to", period.To.ToUtcIsoString());
      if (continuationToken != null)
        query.Add("continuationToken", continuationToken);
      uri += query;
      var request = new HttpRequestMessage(HttpMethod.Get, uri);
      request.Headers.Add("OwnerId", device.SourceDeviceData.ownerId);
      Logger.LogDebug($"Request to MyEnergyCommunity:\n{request}");

      HttpResponseMessage? response = null;
      try
      {
        response = await this.Http.SendAsync(request);
      }
      catch (HttpRequestException connectionException)
      {
        Logger.LogWarning(
            $"Failed connecting to {Source}\n" +
            $"Reason {connectionException.Message}");
        break;
      }

      var responseContent = await response.Content.ReadAsStreamAsync();
      Response<Measurement>? measurementsResponse = null;
      try
      {
        measurementsResponse =
            await JsonSerializer.DeserializeAsync<Response<Measurement>>(
                responseContent);
      }
      catch (JsonException jsonException)
      {
        Logger.LogWarning(
            $"Failed parsing response of {Source}\n" +
            $"Reason {jsonException.Message}");
        break;
      }

      if (measurementsResponse == null)
      {
        continue;
      }

      continuationToken = measurementsResponse.continuationToken;
      result.AddRange(measurementsResponse.items);
    } while (continuationToken != null);

    return result;
  }

  private Ozds.Elasticsearch.Measurement ConvertMeasurement(
      Measurement measurement)
  {
    return new Ozds.Elasticsearch.Measurement(measurement.deviceDateTime,
        new Nest.GeoCoordinate(
          measurement.geoCoordinates.longitude,
          measurement.geoCoordinates.latitude),
        Source, Ozds.Elasticsearch.Device.MakeId(Source, measurement.deviceId),
        new Ozds.Elasticsearch.Measurement.KnownData
        {
          dongleId = measurement.measurementData.dongleId,
          meterIdent = measurement.measurementData.meterIdent,
          meterSerial = measurement.measurementData.meterSerial,
          localTime = measurement.measurementData.localTime,
          localDate = measurement.measurementData.localDate,
          tariff = measurement.measurementData.tariff,
          limiter = measurement.measurementData.limiter,
          fuseSupervisionL1 = measurement.measurementData.fuseSupervisionL1,
          disconnectControl = measurement.measurementData.disconnectControl,
          numLongPwrFailures = measurement.measurementData.numLongPwrFailures,
          numPwrFailures = measurement.measurementData.numPwrFailures,
          numVoltageSagsL1 = measurement.measurementData.numVoltageSagsL1,
          numVoltageSagsL2 = measurement.measurementData.numVoltageSagsL2,
          numVoltageSagsL3 = measurement.measurementData.numVoltageSagsL3,
          numVoltageSwellsL1 = measurement.measurementData.numVoltageSwellsL1,
          numVoltageSwellsL2 = measurement.measurementData.numVoltageSwellsL2,
          numVoltageSwellsL3 = measurement.measurementData.numVoltageSwellsL3,
          currentL1 = measurement.measurementData.currentL1,
          currentL2 = measurement.measurementData.currentL2,
          currentL3 = measurement.measurementData.currentL3,
          energyIn = measurement.measurementData.energyIn,
          energyIn_T1 = measurement.measurementData.energyIn_T1,
          energyIn_T2 = measurement.measurementData.energyIn_T2,
          energyOut = measurement.measurementData.energyOut,
          energyOut_T1 = measurement.measurementData.energyOut_T1,
          energyOut_T2 = measurement.measurementData.energyOut_T2,
          powerIn = measurement.measurementData.powerIn,
          powerInL1 = measurement.measurementData.powerInL1,
          powerInL2 = measurement.measurementData.powerInL2,
          powerInL3 = measurement.measurementData.powerInL3,
          powerOut = measurement.measurementData.powerOut,
          powerOutL1 = measurement.measurementData.powerOutL1,
          powerOutL2 = measurement.measurementData.powerOutL2,
          powerOutL3 = measurement.measurementData.powerOutL3,
          voltageL1 = measurement.measurementData.voltageL1,
          voltageL2 = measurement.measurementData.voltageL2,
          voltageL3 = measurement.measurementData.voltageL3,
        });
  }

  private const string s_source = "MyEnergyCommunity";
}
