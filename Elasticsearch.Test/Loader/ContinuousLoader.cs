using System;
using System.Threading;
using Xunit;

namespace Elasticsearch.Test {
  public partial class Client {
    [Fact]
    public void LoadContinuously() {
      var from = DateTime.Now.AddDays(-1);
      var to = DateTime.Now.AddDays(-1).AddMinutes(5);

      Elasticsearch.Loader.LoadContinuously(_client,
          _measurementProviderIterator,
          new Loader.Period { From = from, To = to });
      Thread.Sleep(1000);
      Assert.NotEmpty(_client.SearchMeasurements(from, to).Hits);

      Thread.Sleep(1000);
      Elasticsearch.Loader.LoadContinuously(
          _client, _measurementProviderIterator);
      Thread.Sleep(1000);
      Assert.NotEmpty(_client.SearchMeasurements(to, DateTime.Now).Hits);
    }
  }
}
