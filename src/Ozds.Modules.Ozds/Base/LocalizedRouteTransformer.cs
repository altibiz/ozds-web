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
        values?.GetOrDefault("page") switch
        {
          var page when page.In("/", "/Index", "/index") => $"/Overview",
          string page => $"/{page}",
          _ => "/Overview",
        }
      }
    }.ToValueTask();
}
