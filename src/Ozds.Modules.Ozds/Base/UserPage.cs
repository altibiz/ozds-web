using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Razor;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using Ozds.Extensions;

public abstract class UserPage<TModel> : RazorPage<TModel>
{

  protected Task<User> OrchardUser
  {
    get =>
      _orchardUser
        .WhenNull(() => _orchardUser =
          Context.RequestServices
            .GetService<IUserService>()
            .ThrowWhenNull()
            .GetAuthenticatedUserAsync(User)
            .Then(orchardUser => orchardUser
              .As<User>()
              .ThrowWhenNull()));
  }

  private Task<User> _orchardUser = default!;
}
