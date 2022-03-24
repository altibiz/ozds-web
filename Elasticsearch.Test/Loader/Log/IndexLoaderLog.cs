using System.Threading;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void IndexLoaderLog() {
      var loaderLog = Data.TestLoaderLog;

      var id = this._client.IndexLoaderLog(loaderLog).Id;
      Thread.Sleep(1000);
      var gotLoaderLog = this._client.GetLoaderLog(id);
      Assert.NotNull(loaderLog.Source);
    }
  }
}
