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
    ValueTask.FromResult(
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
            .FinallyWhen(
              page => !page.In("index", "Index"),
              page => $"/{page}",
              "/portal")
        }
      });
}
