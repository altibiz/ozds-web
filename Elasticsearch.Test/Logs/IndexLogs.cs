using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Nest;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void IndexLoaderLogs() {
      var loaderLogs = new List<Log> { Data.TestLoaderLog };
      var loaderLogIds = loaderLogs.Select(d => new Id(d.Id));

      var indexResponse = _client.IndexLoaderLogs(loaderLogs);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(indexResponse.IsValid);

          var indexedLoaderLogIds = indexResponse.Items.Ids();
          AssertExtensions.ElementsEqual(loaderLogIds, indexedLoaderLogIds);

          foreach (var loaderLog in loaderLogs) {
            var getResponse = _client.GetLoaderLog(loaderLog.Id);
            Assert.True(getResponse.IsValid);

            var gotLoaderLog = getResponse.Source;
            Assert.Equal(loaderLog, gotLoaderLog);
          }

          var deleteResponse = _client.DeleteLoaderLogs(loaderLogIds);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(deleteResponse.IsValid);

          var deletedLoaderLogIds = deleteResponse.Items.Ids();
          AssertExtensions.ElementsEqual(loaderLogIds, deletedLoaderLogIds);
    }

    [Fact]
    public async Task IndexLoaderLogsAsync() {
      var loaderLogs = new List<Log> { Data.TestLoaderLog };
      var loaderLogIds = loaderLogs.Select(d => new Id(d.Id));

      var indexResponse = await _client.IndexLoaderLogsAsync(loaderLogs);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(indexResponse.IsValid);

          var indexedLoaderLogIds = indexResponse.Items.Ids();
          AssertExtensions.ElementsEqual(loaderLogIds, indexedLoaderLogIds);

          foreach (var loaderLog in loaderLogs) {
            var getResponse = await _client.GetLoaderLogAsync(loaderLog.Id);
            Assert.True(getResponse.IsValid);

            var gotLoaderLog = getResponse.Source;
            Assert.Equal(loaderLog, gotLoaderLog);
          }

          var deleteResponse =
              await _client.DeleteLoaderLogsAsync(loaderLogIds);
          // NOTE: https://github.com/elastic/elasticsearch-net/issues/6154
          // Assert.True(deleteResponse.IsValid);

          var deletedLoaderLogIds = deleteResponse.Items.Ids();
          AssertExtensions.ElementsEqual(loaderLogIds, deletedLoaderLogIds);
    }
  }
}
