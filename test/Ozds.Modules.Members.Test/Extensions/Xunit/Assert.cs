using Xunit;

namespace Ozds.Modules.Members.Test;

public static class Asserts
{
  public static T AssertNotNull<T>(this T? @this)
  {
    Assert.NotNull(@this);
    return @this!;
  }

  public static T AssertEquals<T>(this T @this, T other)
  {
    Assert.Equal(@this, other);
    return @this;
  }
}
