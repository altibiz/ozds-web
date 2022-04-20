using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using OrchardCore.Users.Services;
using OrchardCore.Users.Models;
using Ozds.Util;

using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Ozds.Modules.Members;

public static partial class AddTestUsersClass
{
  public static IUserService AddTestUsers(
      this IUserService users,
      ILogger logger,
      IConfiguration conf) =>
     conf
      .GetSection("Ozds")
      .GetSection("Modules")
      .GetSection("Members")
      .GetSection("Users")
      .GetChildren()
      .ForEach(user =>
          users.CreateUserAsync(
            new User
            {
              UserId = user.GetNonNullValue<string>("Id"),
              UserName = user.GetNonNullValue<string>("Username"),
              Email = user.GetNonNullValue<string>("Email"),
              EmailConfirmed = true,
              RoleNames =
                user.GetValue<IList<string>?>("Roles")
                ?? new List<string> { },
            },
            user.GetNonNullValue<string>("Password"),
            (@event, message) => logger.LogError(@event, message)))
      .Await().Result
      .Return(users);
}
