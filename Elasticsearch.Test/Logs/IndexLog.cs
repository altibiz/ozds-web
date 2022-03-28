using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void IndexLoaderLog() {
      var loaderLog = Data.TestLoaderLog;

      var indexResponse = _client.IndexLoaderLog(loaderLog);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, loaderLog.Id.ToString());

      var getResponse = _client.GetLoaderLog(loaderLog.Id);
      Assert.True(getResponse.IsValid);

      var gotLoaderLogId = getResponse.Source.Id;
      Assert.Equal(gotLoaderLogId, loaderLog.Id);

      var deleteResponse = _client.DeleteLoaderLog(loaderLog.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedLoaderLogId = deleteResponse.Id;
      Assert.Equal(deletedLoaderLogId, loaderLog.Id.ToString());
    }

    [Fact]
    public async Task IndexLoaderLogAsync() {
      var loaderLog = Data.TestLoaderLog;

      var indexResponse = await _client.IndexLoaderLogAsync(loaderLog);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, loaderLog.Id.ToString());

      var getResponse = await _client.GetLoaderLogAsync(loaderLog.Id);
      Assert.True(getResponse.IsValid);

      var gotLoaderLogId = getResponse.Source.Id;
      Assert.Equal(gotLoaderLogId, loaderLog.Id);

      var deleteResponse = await _client.DeleteLoaderLogAsync(loaderLog.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedLoaderLogId = deleteResponse.Id;
      Assert.Equal(deletedLoaderLogId, loaderLog.Id.ToString());
    }
  }
}
