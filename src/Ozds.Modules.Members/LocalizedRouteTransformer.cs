using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace Ozds.Modules.Members;

public class LocalizedRouteTransformer : DynamicRouteValueTransformer
{
  public override ValueTask<RouteValueDictionary> TransformAsync(
      HttpContext context, RouteValueDictionary values)
  {
    var result =
        new RouteValueDictionary { { "area", "Ozds.Modules.Members" } };

    if (values?.TryGetValue("page", out object? page) ?? false)
    {
      if (String.Equals(page, "index") || String.Equals(page, "Index"))
      {
        result.Add("page", $"/portal");
      }
      else
      {
        result.Add("page", $"/{page}");
      }
    }
    else
    {
      result.Add("page", "/portal");
    }

    return ValueTask.FromResult(result);
  }
}
