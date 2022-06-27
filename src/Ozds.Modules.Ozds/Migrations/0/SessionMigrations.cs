using YesSql;
using OrchardCore.Users.Models;
using OrchardCore.BackgroundTasks.Models;

namespace Ozds.Modules.Ozds.M0;

public static class SessionMigrations
{
  public static ISession SaveUserTestOwner(
      this ISession session) =>
    session.SaveJson<User>(
        "../Ozds.Modules.Ozds/Migrations/0/UserTestOwner1.json");

  public static ISession SaveUserTestMember(
      this ISession session) =>
    session.SaveJson<User>(
        "../Ozds.Modules.Ozds/Migrations/0/UserTestMember1.json");

  public static ISession SaveBackgroundTasksSettings(
      this ISession session) =>
    session.SaveJson<BackgroundTaskDocument>(
        "../Ozds.Modules.Ozds/Migrations/0/BackgroundTasksSettings.json");

  public static ISession SaveDevBackgroundTasksSettings(
      this ISession session) =>
    session.SaveJson<BackgroundTaskDocument>(
        "../Ozds.Modules.Ozds/Migrations/0/DevBackgroundTasksSettings.json");

  public static ISession SaveData(
      this ISession session) =>
    session
      .SaveBackgroundTasksSettings();

  public static ISession SaveDemoData(
      this ISession session) =>
    session
      .SaveDevBackgroundTasksSettings();

  public static ISession SaveDevData(
      this ISession session) =>
    session
      .SaveUserTestMember()
      .SaveUserTestOwner()
      .SaveDevBackgroundTasksSettings();
}