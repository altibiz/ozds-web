using System;
using Nest;

namespace Elasticsearch {
public static class IElasticClientExtensions {
  public static void TryCreateIndex<T>(this IElasticClient client, string name,
      Func<TypeMappingDescriptor<T>, ITypeMapping>? map = null)
      where T : class {
    if (!client.Indices.Exists(name).Exists) {
      if (map != null) {
        client.Indices.Create(name, c => c.Map<T>(m => map(m.AutoMap<T>())));
      }

      client.Indices.Create(name, c => c.Map<T>(m => m.AutoMap<T>()));
    }
  }

  public static void TryDeleteIndex(this IElasticClient _client, string name) {
    if (!_client.Indices.Exists(name).Exists) {
      _client.Indices.Delete(name);
    }
  }
}
}
