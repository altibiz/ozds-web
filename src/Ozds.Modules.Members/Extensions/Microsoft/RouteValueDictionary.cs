using Microsoft.AspNetCore.Routing;
using Ozds.Util;

namespace Ozds.Modules.Members;

public static class RouteValueDictionaryExtensions
{
  public static string GetNonNullable(
      this RouteValueDictionary routes,
      string key) =>
    routes[key].As<string>().ThrowWhenNull();

  public static T GetNonNullable<T>(
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
