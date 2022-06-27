namespace Ozds.Elasticsearch.Test
{
  public static partial class Data
  {
    public static readonly LoadLog LoadLog =
      new(
        resource: "Id",
        period: new());

    public static readonly MissingDataLog MissingDataLog =
      new(
        resource: "Id",
        period: new(),
        nextExtraction: DateTime.UtcNow,
        retries: 5,
        shouldValidate: false,
        error: new(
          code: MissingDataLogErrorCode.Provider,
          description: "Error fetching"));
  }
}
