using System.Threading;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void DeleteLoaderLog() {
      var loaderLog = Data.TestLoaderLog;

      var id = this._client.IndexLoaderLog(loaderLog).Id;
      Thread.Sleep(1000);
      var gotLoaderLog = this._client.GetLoaderLog(id);
      Assert.NotNull(gotLoaderLog.Source);

      this._client.DeleteLoaderLog(id);
      Thread.Sleep(1000);
      gotLoaderLog = this._client.GetLoaderLog(id);
      Assert.Null(gotLoaderLog.Source);
    }
  }
}
