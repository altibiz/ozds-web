using System.Text.Json;
// NOTE: QueryBuilder
// TODO: dont use AspNetCore here
using Microsoft.AspNetCore.Http.Extensions;
using Ozds.Util;

namespace Ozds.Elasticsearch.MyEnergyCommunity;

public partial interface IClient : IMeasurementProvider { };

public sealed partial class Client : IClient
{
  public string Source { get => Client.MyEnergyCommunitySource; }

  public IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsAwait(
      ProvisionDevice device,
      Period? period = null) =>
    GetMeasurementsViaSplitting(device, period);

  public Task<IEnumerable<IExtractionBucket<ExtractionMeasurement>>>
  GetMeasurementsAsync(
      ProvisionDevice device,
      Period? period = null) =>
    GetMeasurementsAwait(device, period).Await();

  public IEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurements(
      ProvisionDevice device,
      Period? period = null) =>
    GetMeasurementsAsync(device, period).BlockTask();

  private IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsViaSplitting(
      ProvisionDevice device,
      Period? period = null) =>
    period
      .OnlyWhenNullable(device.ExtractionStart.UntilNow())
      // NOTE: API can output max 20 measurements per request without the need
      // NOTE: of a continuation token
      .SplitAscending(device.MeasurementInterval * 15)
      .Select(period => GetNativeMeasurements(device, period)
        .Then(responseOrError => responseOrError switch
          {
            (Response<Measurement> response, _) =>
              new ExtractionBucket<ExtractionMeasurement>(
                  period, response.items.Select(Convert)),
            (_, string error) =>
              new ExtractionBucket<ExtractionMeasurement>(
                  period, error),
            _ =>
              new ExtractionBucket<ExtractionMeasurement>(
                  period, "Unknown error"),
          }))
      .ToAsync();

  private async Task<ErrorWrap<Response<Measurement>>>
  GetNativeMeasurements(
      ProvisionDevice device,
      Period period,
      string? continuationToken = null)
  {
    var requestMessage = CreateRequest(device, period, continuationToken);

    var responseMessage = await SendRequest(requestMessage);
    if (responseMessage.Error is not null)
    {
      return responseMessage.Error;
    }

    return await ParseMessage(responseMessage.Result!);
  }

  private HttpRequestMessage CreateRequest(
      ProvisionDevice device,
      Period? period = null,
      string? continuationToken = null)
  {
    var query = new QueryBuilder();
    if (period?.From != null)
    {
      query.Add("from", period.From.ToUtcIsoString());
    }
    if (period?.To != null)
    {
      query.Add("to", period.To.ToUtcIsoString());
    }
    if (continuationToken != null)
    {
      query.Add("continuationToken", continuationToken);
    }

    var uri = "v1/measurements/device/" + device.SourceDeviceId;
    uri += query;

    var request = new HttpRequestMessage(HttpMethod.Get, uri);
    request.Headers.Add("OwnerId", device.SourceDeviceData.OwnerId);

    Logger.LogDebug($"Request to MyEnergyCommunity:\n{request}");

    return request;
  }

  private async Task<ErrorWrap<HttpResponseMessage>> SendRequest(
      HttpRequestMessage request)
  {
    try
    {
      return await Http.SendAsync(request);
    }
    catch (HttpRequestException exception)
    {
      var error =
        $"Failed connecting to {Source} because {exception.Message}";
      Logger.LogWarning(error);
      return error;
    }
  }

  private async Task<ErrorWrap<Response<Measurement>>> ParseMessage(
      HttpResponseMessage message)
  {
    try
    {
      var response = await message.Content
        .ReadAsStreamAsync()
        .ThenValueTask(content => JsonSerializer
          .DeserializeAsync<Response<Measurement>>(content));
      if (response is null)
      {
        return "Failed deserializing message";
      }
      return response;
    }
    catch (JsonException exception)
    {
      var error =
        $"Failed parsing response of {Source} because {exception.Message}";
      Logger.LogWarning(error);
      return error;
    }
  }

  private ExtractionMeasurement
  Convert(Measurement measurement) =>
    new ExtractionMeasurement
    {
      Timestamp = measurement.deviceDateTime,
      Geo = new ExtractionMeasurementGeo
      {
        Longitude = measurement.geoCoordinates.longitude,
        Latitude = measurement.geoCoordinates.latitude,
      },
      Source = Source,
      SourceDeviceId = measurement.deviceId,
      Data = new ExtractionMeasurementData
      {
        energyIn = measurement.measurementData.energyIn,
        energyIn_T1 = measurement.measurementData.energyIn_T1,
        energyIn_T2 = measurement.measurementData.energyIn_T2,
        powerIn = measurement.measurementData.powerIn,

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
        energyOut = measurement.measurementData.energyOut,
        energyOut_T1 = measurement.measurementData.energyOut_T1,
        energyOut_T2 = measurement.measurementData.energyOut_T2,
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
      }
    };

  // TODO: fix and use this in the future
  private async IAsyncEnumerable<IExtractionBucket<ExtractionMeasurement>>
  GetMeasurementsViaContinuationToken(
      ProvisionDevice device,
      Period? period = null)
  {
    for (
      string? continuationToken = null;
      continuationToken != null;)
    {
      var response = await GetNativeMeasurements(
          device, period!, continuationToken);

      if (response.Error is not null)
      {
        yield return new ExtractionBucket<ExtractionMeasurement>(
            period!, response.Error);
      }
      else
      {
        yield return new ExtractionBucket<ExtractionMeasurement>(
            period!, response.Result!.items.Select(Convert));
      }
    }
  }
}
