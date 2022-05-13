using Nest;
using Ozds.Util;

namespace Ozds.Elasticsearch
{
  public static class IElasticClientExtensions
  {
    public static CreateIndexResponse TryCreateIndex<T>(
        this IElasticClient client,
        string name,
        Func<TypeMappingDescriptor<T>, ITypeMapping>? map = null)
        where T : class =>
      map
        .WhenNonNullable(
          map => client.Indices
            .Create(name, c => c
              .Map<T>(m => map(m.AutoMap<T>()))),
          () => client.Indices
            .Create(name, c => c
              .Map<T>(m => m.AutoMap<T>())));

    public static DeleteIndexResponse TryDeleteIndex(
        this IElasticClient client, string name) =>
      client.Indices.Delete(name);
  }
}
