using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class LocalizedRouteTransformer : DynamicRouteValueTransformer
{
  public override ValueTask<RouteValueDictionary> TransformAsync(
      HttpContext context,
      RouteValueDictionary values) =>
    new RouteValueDictionary
    {
      {
        "area",
        "Ozds.Modules.Ozds"
      },
      {
        "page",
        values
          .WhenNonNullable(
            values => values
              .GetOrDefault("page")
              .WhenFinally(
                page => !page.In("index", "Index", "/", "/index", "/Index"),
                page => $"/{page}",
                "/Overview"),
            "/Overview")
      }
    }.ToValueTask();
}
