using System;
using Xunit;

namespace Elasticsearch.HelbOzds.Test {
  public partial class Client {
    [Fact]
    public void GetMeasurements() {
      var measurements =
          this._client.GetMeasurements(Client.TestOwnerId, Client.TestDeviceId,
              DateTime.Now.Add(TimeSpan.FromMinutes(-5)), DateTime.Now);
      Assert.NotEmpty(measurements);
    }
  }
}
