using System;

namespace Ozds.Elasticsearch {
  public static class EnvironmentExtensions {
    public static string AssertEnvironmentVariable(string key) {
      var value = System.Environment.GetEnvironmentVariable(key);

      if (value == null) {
        throw new EnvironmentVariableNotFoundException(
            $"{key} environment variable not found.");
      }

      return value;
    }
  }

  public class EnvironmentVariableNotFoundException : SystemException {
    public EnvironmentVariableNotFoundException(string message)
        : base(message) {}
  }
}
