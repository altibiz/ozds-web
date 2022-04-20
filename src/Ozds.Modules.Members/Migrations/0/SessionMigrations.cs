using YesSql;
using OrchardCore.Users.Models;

namespace Ozds.Modules.Members.M0;

public static class SessionMigrations
{
  public static ISession SaveUserTestOwner(this ISession session) =>
    session.SaveJson<User>(
        "../Ozds.Modules.Members/Migrations/0/UserTestOwner.json");

  public static ISession SaveUserTestMember(this ISession session) =>
    session.SaveJson<User>(
        "../Ozds.Modules.Members/Migrations/0/UserTestMember.json");
}
