using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using System.ComponentModel.DataAnnotations;
using YesSql;
using Ozds.Util;
using ISession = YesSql.ISession;

namespace Ozds.Modules.Members;

public class PersonPartService : IPartService<Person>
{
  public async IAsyncEnumerable<ValidationResult> ValidateAsync(
      Person part)
  {
    if (part.ContentItem.ContentItemId.StartsWith("nat_"))
    {
      yield break;
    }
    var personPartSettings =
        ContentDefinitions.GetSettings<PersonSettings>(part);
    if (!string.IsNullOrWhiteSpace(part.Oib?.Text))
    {
      var oib = part.Oib.Text;
      if (oib.Length != 11)
      {
        yield return new ValidationResult(
            Localizer["Your ID must be 11 numbers."]);
      }

      if (!await IsPersonUniqueAsync(part, oib))
      {
        yield return new ValidationResult(
            Localizer["Your ID is already in use."]);
      }
    }
    if (!part.Legal &&
        string.IsNullOrWhiteSpace(part.Surname.Text))
    {
      yield return new ValidationResult(Localizer["Surname is required."]);
    }
  }

  private async Task<bool> IsPersonUniqueAsync(Person part, string oib)
  {
    return (await Session
                   .QueryIndex<PersonIndex>(
                       o => o.Oib == oib &&
                            o.ContentItemId != part.ContentItem.ContentItemId &&
                            o.Legal == part.Legal)
                   .CountAsync()) == 0;
  }

  public async Task<IEnumerable<PersonIndex>> GetByOibAsync(string oib)
  {
    return await Session.QueryIndex<PersonIndex>(o => o.Oib == oib)
        .ListAsync();
  }

  // TODO: better
  private Task<User> GetCurrentUser() =>
    Users
      .GetAuthenticatedUserAsync(HttpContext.HttpContext?.User)
      .Then(user => user.As<User>())!;

  public async Task InitializingAsync(Person part)
  {
    var user = await GetCurrentUser();
    if (user == null)
      return;
    part.Email = new TextField { Text = user.Email };
  }

  public Task PublishedAsync(
      Person instance, PublishContentContext context)
  {
    return Task.CompletedTask;
  }

  public Action<Person> GetEditModel(
      Person part, BuildPartEditorContext context)
  {
    return (part) => { };
  }

  public Task UpdatedAsync<TPart>(
      UpdateContentContext context, Person instance)
  {
    return Task.CompletedTask;
  }

  public PersonPartService(IStringLocalizer<Person> localizer,
      ISession session, IContentDefinitionManager contentDefinitions,
      IHttpContextAccessor httpContext, IUserService users)
  {
    Session = session;
    Localizer = localizer;
    ContentDefinitions = contentDefinitions;
    HttpContext = httpContext;
    Users = users;
  }

  private IStringLocalizer<Person> Localizer { get; }
  private ISession Session { get; }
  private IContentDefinitionManager ContentDefinitions { get; }
  private IHttpContextAccessor HttpContext { get; }
  private IUserService Users { get; }
}
