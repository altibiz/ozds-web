using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class LocalizedRouteTransformer : DynamicRouteValueTransformer
{
  public override ValueTask<RouteValueDictionary> TransformAsync(
      HttpContext context,
      RouteValueDictionary values) =>
    new RouteValueDictionary
    {
      {
        "area",
        "Ozds.Modules.Members"
      },
      {
        "page",
        values
          .GetOrDefault("page")
          .WhenFinally(
            page => !page.In("index", "Index"),
            page => $"/{page}",
            "/portal")
      }
    }.ToValueTask();
}
