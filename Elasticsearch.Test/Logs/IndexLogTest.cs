using System.Threading.Tasks;
using Xunit;

namespace Elasticsearch.Test
{
  public partial class ClientTest
  {
    [Fact]
    public void IndexLogTest()
    {
      var Log = Data.LoadBeginLog;

      var indexResponse = Client.IndexLog(Log);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, Log.Id.ToString());

      var getResponse = Client.GetLog(Log.Id);
      Assert.True(getResponse.IsValid);

      var gotLogId = getResponse.Source.Id;
      Assert.Equal(gotLogId, Log.Id);

      var deleteResponse = Client.DeleteLog(Log.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedLogId = deleteResponse.Id;
      Assert.Equal(deletedLogId, Log.Id.ToString());
    }

    [Fact]
    public async Task IndexLogAsyncTest()
    {
      var Log = Data.LoadBeginLog;

      var indexResponse = await Client.IndexLogAsync(Log);
      Assert.True(indexResponse.IsValid);

      var indexedId = indexResponse.Id;
      Assert.Equal(indexedId, Log.Id.ToString());

      var getResponse = await Client.GetLogAsync(Log.Id);
      Assert.True(getResponse.IsValid);

      var gotLogId = getResponse.Source.Id;
      Assert.Equal(gotLogId, Log.Id);

      var deleteResponse = await Client.DeleteLogAsync(Log.Id);
      Assert.True(deleteResponse.IsValid);

      var deletedLogId = deleteResponse.Id;
      Assert.Equal(deletedLogId, Log.Id.ToString());
    }
  }
}
