using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Fact]
  public void IndexLogTest()
  {
    var Log = Data.LoadLog;

    var indexResponse = Client.IndexLoadLog(Log);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(Log.Id, indexedId);

    var getResponse = Client.GetLoadLog(Log.Id);
    Assert.True(getResponse.IsValid);

    var gotLogId = getResponse.Source.Id;
    Assert.Equal(Log.Id, gotLogId);

    var deleteResponse = Client.DeleteLoadLog(Log.Id);
    Assert.True(deleteResponse.IsValid);

    var deletedLogId = deleteResponse.Id;
    Assert.Equal(Log.Id, deletedLogId);
  }

  [Fact]
  public async Task IndexLogAsyncTest()
  {
    var Log = Data.LoadLog;

    var indexResponse = await Client.IndexLoadLogAsync(Log);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(Log.Id, indexedId);

    var getResponse = await Client.GetLoadLogAsync(Log.Id);
    Assert.True(getResponse.IsValid);

    var gotLogId = getResponse.Source.Id;
    Assert.Equal(Log.Id, gotLogId);

    var deleteResponse = await Client.DeleteLoadLogAsync(Log.Id);
    Assert.True(deleteResponse.IsValid);

    var deletedLogId = deleteResponse.Id;
    Assert.Equal(Log.Id, deletedLogId);
  }
}
