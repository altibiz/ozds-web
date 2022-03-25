using System.Linq;
using System.Collections.Generic;
using Nest;

namespace Elasticsearch {
public static class IBulkResponseItemBaseExtensions {
  public static IEnumerable<Id>
  Ids(this IEnumerable<BulkResponseItemBase> items) => items.Select(
      i => new Id(i.Id));
}
}
