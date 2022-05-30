using Microsoft.AspNetCore.Routing;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public static class RouteValueDictionaryExtensions
{
  public static string GetNonNull(
      this RouteValueDictionary routes,
      string key) =>
    routes[key].As<string>().ThrowWhenNull();

  public static T GetNonNull<T>(
      this RouteValueDictionary routes,
      string key) where T : class =>
    routes[key].As<T>().ThrowWhenNull();

  public static string? GetNullable(
      this RouteValueDictionary routes,
      string key) =>
    routes[key].As<string>();

  public static T? GetNullable<T>(
      this RouteValueDictionary routes,
      string key) where T : class =>
    routes[key].As<T>();
}
