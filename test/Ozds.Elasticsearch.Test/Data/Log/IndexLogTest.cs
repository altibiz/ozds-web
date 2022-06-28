using Xunit;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Fact]
  public void IndexLoadLogTest()
  {
    var log = Data.LoadLog;

    var indexResponse = Client.IndexLoadLog(log);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(log.Id, indexedId);

    var getResponse = Client.GetLoadLog(log.Id);
    Assert.True(getResponse.IsValid);

    var gotLogId = getResponse.Source.Id;
    Assert.Equal(log.Id, gotLogId);

    var deleteResponse = Client.DeleteLoadLog(log.Id);
    Assert.True(deleteResponse.IsValid);

    var deletedLogId = deleteResponse.Id;
    Assert.Equal(log.Id, deletedLogId);
  }

  [Fact]
  public async Task IndexLoadLogAsyncTest()
  {
    var log = Data.LoadLog;

    var indexResponse = await Client.IndexLoadLogAsync(log);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(log.Id, indexedId);

    var getResponse = await Client.GetLoadLogAsync(log.Id);
    Assert.True(getResponse.IsValid);

    var gotLogId = getResponse.Source.Id;
    Assert.Equal(log.Id, gotLogId);

    var deleteResponse = await Client.DeleteLoadLogAsync(log.Id);
    Assert.True(deleteResponse.IsValid);

    var deletedLogId = deleteResponse.Id;
    Assert.Equal(log.Id, deletedLogId);
  }

  [Fact]
  public void IndexMissingDataLogTest()
  {
    var log = Data.MissingDataLog;

    var indexResponse = Client.IndexMissingDataLog(log);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(log.Id, indexedId);

    var getResponse = Client.GetMissingDataLog(log.Id);
    Assert.True(getResponse.IsValid);

    var gotLogId = getResponse.Source.Id;
    Assert.Equal(log.Id, gotLogId);

    var deleteResponse = Client.DeleteMissingDataLog(log.Id);
    Assert.True(deleteResponse.IsValid);

    var deletedLogId = deleteResponse.Id;
    Assert.Equal(log.Id, deletedLogId);
  }

  [Fact]
  public async Task IndexMissingDataLogAsyncTest()
  {
    var log = Data.MissingDataLog;

    var indexResponse = await Client.IndexMissingDataLogAsync(log);
    Assert.True(indexResponse.IsValid);

    var indexedId = indexResponse.Id;
    Assert.Equal(log.Id, indexedId);

    var getResponse = await Client.GetMissingDataLogAsync(log.Id);
    Assert.True(getResponse.IsValid);

    var gotLogId = getResponse.Source.Id;
    Assert.Equal(log.Id, gotLogId);

    var deleteResponse = await Client.DeleteMissingDataLogAsync(log.Id);
    Assert.True(deleteResponse.IsValid);

    var deletedLogId = deleteResponse.Id;
    Assert.Equal(log.Id, deletedLogId);
  }
}
