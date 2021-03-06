using Xunit;
using Ozds.Extensions;

namespace Ozds.Elasticsearch.Test.MeasurementFaker;

public partial class ClientTest
{
  public ClientTest(FakeMeasurementProvider faker, ILogger<ClientTest> logger)
  {
    Logger = logger;
    Faker = faker;
  }

  private FakeMeasurementProvider Faker { get; }
  private ILogger Logger { get; }

  [Fact]
  public void GetMeasurementsTest()
  {
    var device = Data.FakeDevice;
    var period = new Period
    {
      From = DateTime.UtcNow.AddMinutes(-5),
      To = DateTime.UtcNow
    };

    var measurements = Faker
      .GetMeasurements(device.ToProvisionDevice(), period)
      .SelectMany(Functions.Id)
      .ToList();
    Assert.NotEmpty(measurements);
    Assert.InRange(measurements.Count, 15, 25);
    Assert.All(
      measurements,
      measurement =>
      {
        Assert.InRange(
            measurement.Timestamp,
            period.From,
            period.To);
      });
  }

  [Fact]
  public async Task GetMeasurementsAsyncTest()
  {
    var device = Data.FakeDevice;
    var period = new Period
    {
      From = DateTime.UtcNow.AddMinutes(-5),
      To = DateTime.UtcNow
    };

    var measurements = await Faker
      .GetMeasurementsAsync(device.ToProvisionDevice(), period)
      .Await()
      .Then(buckets => buckets
        .SelectMany(Functions.Id)
        .ToList());
    Assert.NotEmpty(measurements);
    Assert.InRange(measurements.Count, 15, 25);
    Assert.All(
      measurements,
      measurement =>
      {
        Assert.InRange(
            measurement.Timestamp,
            period.From,
            period.To);
      });
  }
}
