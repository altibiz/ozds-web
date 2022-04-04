// TODO: fix these when the Exists API starts working?

using System;
using Nest;
// using System.Net; NOTE: for WebException

namespace Elasticsearch
{
  public static class IElasticClientExtensions
  {
    public static CreateIndexResponse? TryCreateIndex<T>(
        this IElasticClient client, string name,
        Func<TypeMappingDescriptor<T>, ITypeMapping>? map = null)
        where T : class
    {
      // NOTE: Exists API doesn't work for some reason
      //     var exists = client.Indices.Exists(name);
      //     if (!exists.IsValid) {
      // #if DEBUG
      //       Console.WriteLine($"Failed checking existence of index {name}");
      //       Console.WriteLine($"Reason ${exists.DebugInformation}");
      // #endif
      //       throw new WebException($"Failed checking existence of index
      //       {name}");
      //     }
      //
      //     if (exists.Exists) {
      //       Console.WriteLine($"Index {name} already exists");
      //       return null;
      //     }

      var create = map is null ? client.Indices.Create(
                                     name, c => c.Map<T>(m => m.AutoMap<T>()))
                               : client.Indices.Create(name,
                                     c => c.Map<T>(m => map(m.AutoMap<T>())));
      if (!create.IsValid)
      {
        // #if DEBUG
        //       Console.WriteLine($"Failed creating index {name}");
        //       Console.WriteLine($"Reason ${create.DebugInformation}");
        // #endif
        // throw new WebException($"Failed creating index {name}");
        return null;
      }

      return create;
    }

    public static DeleteIndexResponse? TryDeleteIndex(
        this IElasticClient client, string name)
    {
      // NOTE: Exists API doesn't work for some reason
      //     var exists = client.Indices.Exists(name);
      //     if (!exists.IsValid) {
      // #if DEBUG
      //       Console.WriteLine($"Failed checking existence of index {name}");
      //       Console.WriteLine($"Reason ${exists.DebugInformation}");
      // #endif
      //       throw new WebException($"Failed checking existence of index
      //       {name}");
      //     }
      //
      //     if (!exists.Exists) {
      //       Console.WriteLine($"Index {name} doesn't exist");
      //       return null;
      //     }

      var delete = client.Indices.Delete(name);
      if (!delete.IsValid)
      {
        // #if DEBUG
        //       Console.WriteLine($"Failed deleting index {name}");
        //       Console.WriteLine($"Reason ${delete.DebugInformation}");
        // #endif
        // throw new WebException($"Failed deleting index {name}");
        return null;
      }

      return delete;
    }
  }
}
