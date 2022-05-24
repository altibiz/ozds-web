using Xunit;
using Nest;

namespace Ozds.Elasticsearch.Test;

public partial class ClientTest
{
  [Fact]
  public void IndexLogsTest()
  {
    var Logs = new List<Log> { Data.LoadBeginLog };
    var LogIds = Logs.Select(d => new Id(d.Id));

    var indexResponse = Client.IndexLogs(Logs);
    // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
    // Assert.True(indexResponse.IsValid);

    var indexedLogIds = indexResponse.Items.Ids();
    AssertExtensions.ElementsEqual(LogIds, indexedLogIds);

    foreach (var Log in Logs)
    {
      var getResponse = Client.GetLog(Log.Id);
      Assert.True(getResponse.IsValid);

      var gotLog = getResponse.Source;
      Assert.Equal(Log, gotLog);
    }

    var deleteResponse = Client.DeleteLogs(LogIds);
    // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
    // Assert.True(deleteResponse.IsValid);

    var deletedLogIds = deleteResponse.Items.Ids();
    AssertExtensions.ElementsEqual(LogIds, deletedLogIds);
  }

  [Fact]
  public async Task IndexLogsAsyncTest()
  {
    var logs = new List<Log> { Data.LoadBeginLog };
    var logIds = logs.Select(d => new Id(d.Id));

    var indexResponse = await Client.IndexLogsAsync(logs);
    // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
    // Assert.True(indexResponse.IsValid);

    var indexedLogIds = indexResponse.Items.Ids();
    AssertExtensions.ElementsEqual(logIds, indexedLogIds);

    foreach (var log in logs)
    {
      var getResponse = await Client.GetLogAsync(log.Id);
      Assert.True(getResponse.IsValid);

      var gotLog = getResponse.Source;
      Assert.Equal(log, gotLog);
    }

    var deleteResponse = await Client.DeleteLogsAsync(logIds);
    // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
    // Assert.True(deleteResponse.IsValid);

    var deletedLogIds = deleteResponse.Items.Ids();
    AssertExtensions.ElementsEqual(logIds, deletedLogIds);
  }
}
