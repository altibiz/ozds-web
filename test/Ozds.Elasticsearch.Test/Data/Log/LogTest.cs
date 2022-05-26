namespace Ozds.Elasticsearch.Test
{
  public static partial class Data
  {
    public static readonly LoadLog LoadLog =
      new LoadLog(
          "Id",
          new Period { });

    public static readonly MissingDataLog MissingDataLog =
      new MissingDataLog(
          "Id",
          new Period { },
          DateTime.UtcNow,
          5,
          false,
          "");
  }
}
