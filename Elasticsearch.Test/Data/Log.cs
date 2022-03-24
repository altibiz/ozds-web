using System;

namespace Elasticsearch.Test {
  public partial class Data {
    public static readonly Loader.Log TestLoaderLog =
        new Loader.Log { Timestamp = DateTime.Now,
          Type = Loader.LogType.LoadBegin,
          Period = new Loader.Period { From = DateTime.Now.AddMinutes(-5),
            To = DateTime.Now } };
  }
}
