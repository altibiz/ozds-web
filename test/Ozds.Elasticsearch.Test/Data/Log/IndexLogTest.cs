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
    Assert.Equal(indexedId, Log.Id.ToString());

    var getResponse = Client.GetLoadLog(Log.Id);
    Assert.True(getResponse.IsValid);

    var gotLogId = getResponse.Source.Id;
    Assert.Equal(gotLogId, Log.Id);

    var deleteResponse = Client.DeleteLoadLog(Log.Id);
    Assert.True(deleteResponse.IsValid);

    var deletedLogId = deleteResponse.Id;
    Assert.Equal(deletedLogId, Log.Id.ToString());
  }

  [Fact]
  public async Task IndexLogAsyncTest()
  {
    var Log = Data.LoadLog;

    var indexResponse = await Client.IndexLoadLogAsync(Log);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(indexedId, Log.Id.ToString());

    var getResponse = await Client.GetLoadLogAsync(Log.Id);
    Assert.True(getResponse.IsValid);

    var gotLogId = getResponse.Source.Id;
    Assert.Equal(gotLogId, Log.Id);

    var deleteResponse = await Client.DeleteLoadLogAsync(Log.Id);
    Assert.True(deleteResponse.IsValid);

    var deletedLogId = deleteResponse.Id;
    Assert.Equal(deletedLogId, Log.Id.ToString());
  }
}
