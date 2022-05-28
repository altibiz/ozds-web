using Microsoft.Extensions.Configuration;

namespace Ozds.Modules.Ozds;

public static class IConfigurationSectionExtensions
{
  public static T GetNonNullValue<T>(
      this IConfigurationSection section,
      string key) where T : notnull =>
    section.GetValue<T?>(key) ??
    throw new InvalidOperationException("Section value must not be null");
}
