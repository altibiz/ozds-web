using Xunit;

namespace Ozds.Elasticsearch.Test;

public class IndicesTest
{
  [Theory]
  [InlineData("ozds.debug.measurements")]
  public void DebugMeasurementsIndexTest(string name)
  {
    var index = Namer.MakeIndex(name);
    Assert.Equal(name, index.Name);
    Assert.Equal("measurements", index.Base);
    Assert.True(index.IsDev);
    Assert.Null(index.Test);
    Assert.Null(index.Version);
  }

  [Theory]
  [InlineData("ozds.measurements.v1")]
  public void DebugVersionedMeasurementsIndexTest(string name)
  {
    var index = Namer.MakeIndex(name);
    Assert.Equal(name, index.Name);
    Assert.Equal("measurements", index.Base);
    Assert.False(index.IsDev);
    Assert.Null(index.Test);
    Assert.Equal(1, index.Version);
  }

  [Theory]
  [InlineData("ozds.debug.measurements.test.test-name")]
  public void DebugTestMeasurementsIndexTest(string name)
  {
    var index = Namer.MakeIndex(name);
    Assert.Equal(name, index.Name);
    Assert.Equal("measurements", index.Base);
    Assert.True(index.IsDev);
    Assert.Equal("test-name", index.Test);
    Assert.Null(index.Version);
  }

  public IndicesTest(
      IIndexNamer namer)
  {
    Namer = namer;
  }

  IIndexNamer Namer { get; }
}
