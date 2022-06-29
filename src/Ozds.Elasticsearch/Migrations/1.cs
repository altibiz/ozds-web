using Nest;

namespace Ozds.Elasticsearch;

internal static partial class Migrations
{
  public static IMigrationStore Apply1(
      this IMigrationStore store) =>
    store
      .Migrate(
        Index.Log,
        new IProcessor[]
        {
          new SetProcessor
          {
            If = "ctx?.type == 'missingData'",
            Field = "exception.description",
            Value = "{{{error}}}",
          },
          new SetProcessor
          {
            If = "ctx?.type == 'missingData'",
            Field = "exception.code",
            Value = "unknown",
          },
          new RemoveProcessor
          {
            If = "ctx?.type == 'missingData'",
            Field = "error",
          },
          new SetProcessor
          {
            If = "ctx?.type == 'load'",
            Field = "lastValidation",
            Value = DateTime.UtcNow,
          }
        })
      .Migrate(
        Index.Devices,
        new IProcessor[]
        {
          new RemoveProcessor
          {
            Field = "lastValidation"
          }
        })
      .Migrate(
        Index.Measurements,
        new IProcessor[]
        {
        });
}
