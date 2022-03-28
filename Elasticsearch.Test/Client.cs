using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Elasticsearch.Test {
  public class ClientFixture {
    public Elasticsearch
        .IMeasurementProviderIterator MeasurementProviderIterator { get; init; } =
        new Elasticsearch.ExternalMeasurementProviderIterator();
  }

  public partial class Client : IClassFixture<ClientFixture> {
    public Client(ClientFixture data, ITestOutputHelper output) {
      _output = output;

      // NOTE: https://github.com/xunit/xunit/issues/416#issuecomment-378512739
      var type = output.GetType();
      var testMember =
          type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
      _test = (ITest?)testMember?.GetValue(output);

      _measurementProviderIterator = data.MeasurementProviderIterator;
      _client = new Elasticsearch.Client(
          _test != null ? $".{_test.DisplayName}".ToLowerInvariant() : null);
    }

    private ITest? _test { get; init; }
    private ITestOutputHelper _output { get; init; }
    private Elasticsearch.IClient _client { get; init; }
    private Elasticsearch
        .IMeasurementProviderIterator _measurementProviderIterator { get; init;}
  }
}
