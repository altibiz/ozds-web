using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Elasticsearch.Test {
  public class ClientTestFixture {
    public IClient Client { get; init; } =
        new Client();

    public IMeasurementProviderIterator MeasurementProviderIterator { get; init; } =
        new FakeMeasurementProviderIterator();
  }

  public partial class ClientTest : IClassFixture<ClientTestFixture> {
    public ClientTest(ClientTestFixture fixture, ITestOutputHelper output) {
      TestOutput = output;
      Test = GetTest(TestOutput);
      IndexSuffix = Test?.DisplayName;

      Client = fixture.Client.WithIndexSuffix(IndexSuffix);
      MeasurementProviderIterator = fixture.MeasurementProviderIterator;
    }

    private IClient Client { get; init; }
    private IMeasurementProviderIterator MeasurementProviderIterator { get; init; }

    private ITestOutputHelper TestOutput { get; init; }
    private ITest? Test { get; init; }

    private string? IndexSuffix {
      get => _indexSuffix;
      init => _indexSuffix = value?.RegexReplace(@"^.+\.(.+?)$", @"$1")
                                 .RegexReplace(@"([a-z])([A-Z])", @"$1-$2")
                                 .ToLowerInvariant();
    }

    private string? _indexSuffix = null;

    // NOTE: https://github.com/xunit/xunit/issues/416#issuecomment-378512739
    private ITest? GetTest(ITestOutputHelper output) =>
        (ITest?)output.GetType()
            .GetField("test", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(output);
  }
}
