using System.Text.Json;
using YesSql;
using Ozds.Util;

namespace Ozds.Modules.Members;

public static class ISessionExtensions
{
  public static ISession SaveJson<T>(
      this ISession session,
      string path)
  {
    File
      .OpenText(path)
      .Using(stream =>
          session.Save(
            JsonSerializer.Deserialize<T>(
              stream.ReadToEnd())));

    return session;
  }

  public static ISession SaveJson(
      this ISession session,
      string path,
      Type type)
  {
    File
      .OpenText(path)
      .Using(stream =>
          session.Save(
            JsonSerializer.Deserialize(
              stream.ReadToEnd(),
              type)));

    return session;
  }
}
