using System;

namespace Elasticsearch {
public static class EnvironmentExtensions {
  public static string GetEnvironmentVariable(string key) {
    var value = Environment.GetEnvironmentVariable(key);

    if (value == null) {
      throw new EnvironmentVariableNotFoundException(
          $"{key} environment variable not found.");
    }

    return value;
  }
}

public class EnvironmentVariableNotFoundException : SystemException {
  public EnvironmentVariableNotFoundException(string message) : base(message) {}
}
}
