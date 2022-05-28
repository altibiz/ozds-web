using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OrchardCore.Recipes.Models;
using OrchardCore.Recipes.Services;
using OrchardCore.Users.Models;
using YesSql;

namespace Ozds.Modules.Ozds;

public class UserImport : IRecipeStepHandler
{
  public Task ExecuteAsync(RecipeExecutionContext context)
  {
    if (!string.Equals(
          context.Name, "users",
          StringComparison.OrdinalIgnoreCase))
    {
      return Task.CompletedTask;
    }

    var session = Services.GetRequiredService<ISession>();

    var users = context
      .Step.ToObject<Model>()
      .Data.ToObject<User[]>();
    if (users is null)
    {
      throw new InvalidOperationException(
          "Please provide users for the 'users' step");
    }

    foreach (var user in users)
    {
      session.Save(user);
    }

    return Task.CompletedTask;
  }

  private readonly record struct Model(JArray Data);

  public UserImport(IServiceProvider services)
  {
    Services = services;
  }

  private IServiceProvider Services { get; }
}
