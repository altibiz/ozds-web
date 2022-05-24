using YesSql;
using OrchardCore.Users.Models;

namespace Ozds.Modules.Ozds.M0;

public static class SessionMigrations
{
  public static ISession SaveUserTestOwner(this ISession session) =>
    session.SaveJson<User>(
        "../Ozds.Modules.Ozds/Migrations/0/UserTestOwner1.json");

  public static ISession SaveUserTestMember(this ISession session) =>
    session.SaveJson<User>(
        "../Ozds.Modules.Ozds/Migrations/0/UserTestMember1.json");

  public static ISession SaveTestData(this ISession session) =>
    session
      .SaveUserTestMember()
      .SaveUserTestOwner();

  public static ISession SaveData(this ISession session) =>
    session;
}