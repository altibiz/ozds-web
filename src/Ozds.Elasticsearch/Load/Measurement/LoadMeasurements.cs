namespace Ozds.Elasticsearch;

public partial interface IElasticsearchClient : IMeasurementLoader { }

public partial class ElasticsearchClient : IElasticsearchClient
{
  public async Task LoadMeasurementsAsync(
      AsyncEnrichedMeasurementExtraction extraction)
  {
    var containsItemsThatShouldBeValidated = false;
    var itemsThatShouldBeValidatedValid = true;
    await foreach (var item in extraction.Items)
    {
      if (item.Next is null or
        { Error.Code: >= ExtractionPlanItemErrorCode.Consistency })
      {
        await IndexMeasurementsAsync(item.Bucket
          .Select(LoadMeasurementExtensions.ToMeasurement));
      }

      if (item.Original.ShouldValidate)
      {
        containsItemsThatShouldBeValidated = true;
      }

      if (item.Next is not null)
      {
        await IndexMissingDataLogAsync(item.Next.Value
          .ToMissingDataLogFor(extraction.Device));

        Logger.LogDebug(
          $"Missing data for {extraction.Device.Id} " +
          $"at {extraction.Period} " +
          $"because {item.Next.Value.Error}");

        if (item.Original is { ShouldValidate: true, Error: null })
        {
          itemsThatShouldBeValidatedValid = false;
        }
      }
    }

    if (containsItemsThatShouldBeValidated && itemsThatShouldBeValidatedValid)
    {
      await ExtendLoadLogPeriodWithLastValidationAsync(
        LoadLog.MakeId(extraction.Device.Id),
        extraction.Period.To);

      Logger.LogDebug(
        $"Validated data for {extraction.Device.Id} " +
        $"at {extraction.Period}");
    }
    else
    {
      await ExtendLoadLogPeriodAsync(
        LoadLog.MakeId(extraction.Device.Id),
        extraction.Period.To);
    }

    Logger.LogDebug(
      $"Finished load for {extraction.Device.Id} " +
      $"at {extraction.Period}");
  }

  public void LoadMeasurements(
      EnrichedMeasurementExtraction extraction)
  {
    var containsItemsThatShouldBeValidated = false;
    var itemsThatShouldBeValidatedValid = true;
    foreach (var item in extraction.Items)
    {
      // NOTE: indexing first, so in case something happens right here, we
      // NOTE: don't have faulty logs
      // NOTE: duplication of measurements is not a problem ever and even
      // NOTE: if it was ids of measurements prevent that
      IndexMeasurements(item.Bucket
        .Select(LoadMeasurementExtensions.ToMeasurement));

      if (item.Original.ShouldValidate)
      {
        containsItemsThatShouldBeValidated = true;
      }

      if (item.Next.HasValue)
      {
        IndexMissingDataLog(item.Next.Value
          .ToMissingDataLogFor(extraction.Device));

        Logger.LogDebug(
          $"Missing data for {extraction.Device.Id} " +
          $"at {extraction.Period} " +
          $"because {item.Next.Value.Error}");

        if (item.Original.ShouldValidate)
        {
          itemsThatShouldBeValidatedValid = false;
        }
      }
    }

    if (containsItemsThatShouldBeValidated && itemsThatShouldBeValidatedValid)
    {
      ExtendLoadLogPeriodWithLastValidation(
        LoadLog.MakeId(extraction.Device.Id),
        extraction.Period.To);

      Logger.LogDebug(
        $"Validated data for {extraction.Device.Id} " +
        $"at {extraction.Period}");
    }
    else
    {
      ExtendLoadLogPeriod(
        LoadLog.MakeId(extraction.Device.Id),
        extraction.Period.To);
    }

    Logger.LogDebug(
      $"Finished load for {extraction.Device.Id} " +
      $"at {extraction.Period}");
  }
}
