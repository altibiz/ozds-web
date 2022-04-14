using System.Security.Claims;
using Ozds.Modules.Members.Base;
using Ozds.Modules.Members.Persons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OrchardCore;
using OrchardCore.ContentFields.Indexing.SQL;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using YesSql;
using ISession = YesSql.ISession;

namespace Ozds.Modules.Members.Core;

public enum ContentType { Member, Company, Offer }

public class MemberService
{
  public async Task<ContentValidateResult> CreateMemberDraft(
      ContentItem memberItem)
  {
    var user = await GetCurrentUser();
    memberItem.Owner = user.UserName;
    memberItem.Alter<Member>(
        member => { member.User.UserIds = new[] { user.UserId }; });

    return await Content.UpdateValidateAndCreateAsync(
        memberItem, VersionOptions.Draft);
  }

  public async Task<ContentValidateResult> CreateNew(
      ContentItem memberItem, bool published = false)
  {
    return await Content.UpdateValidateAndCreateAsync(memberItem,
        published ? VersionOptions.Published : VersionOptions.Draft);
  }

  public async Task<ContentItem?> GetUserMember(
      bool includeDraft = false, ClaimsPrincipal? userPrincipal = null)
  {
    var user = await GetCurrentUser(userPrincipal);
    var query = Session.Query<ContentItem, UserPickerFieldIndex>(
        x =>
            x.ContentType == nameof(Member) && x.SelectedUserId == user.UserId);
    if (!includeDraft) { query = query.Where(x => x.Published); }
    var member =
await query.ListAsync();
    return member.FirstOrDefault();
  }

  public async Task<ContentItem> GetContentItemById(string contentItemId)
  {
    return await Session.GetItemById(contentItemId);
  }

  public async Task<ContentItem> GetByOib(string oib)
  {
    return await Session.Query<ContentItem>()
        .With<PersonPartIndex>(x => x.Oib == oib)
        .FirstOrDefaultAsync();
  }

  public async Task<(ContentItem item, IShape shape)> GetNewItem(
      ContentType cType)
  {
    return await GetNewItem(cType.ToString());
  }

  public async Task<(ContentItem item, IShape shape)> GetNewItem(string cType)
  {
    var contentItem = await Content.NewAsync(cType.ToString());
    var model = await ContentDisplay.BuildEditorAsync(
        contentItem, UpdateModel.ModelUpdater, true);
    return (contentItem, model);
  }

  public async Task<(ContentItem item, IShape shape)> ModelToNew(
      ContentType memberType)
  {
    return await ModelToNew(memberType.ToString());
  }

  public async Task<(ContentItem item, IShape shape)> ModelToNew(
      string memberType)
  {
    return await ModelToItem(await Content.NewAsync(memberType));
  }

  public async Task<(ContentItem item, IShape shape)> ModelToItem(
      string? id = null)
  {
    return await ModelToItem(await Content.GetAsync(id, VersionOptions.Latest));
  }

  public async Task<(ContentItem item, IShape shape)> ModelToItem(
      ContentItem contentItem)
  {
    var shape = await ContentDisplay.UpdateEditorAsync(
        contentItem, UpdateModel.ModelUpdater, true);
    if (!UpdateModel.ModelUpdater.ModelState.IsValid)
    {
      await Session.CancelAsync();
    }

    return (contentItem, shape);
  }

  public async Task<(ContentItem item, IShape shape)> GetEditorById(
      string contentId)
  {
    var contentItem = await Content.GetAsync(contentId, VersionOptions.Latest);
    var shape = await ContentDisplay.BuildEditorAsync(
        contentItem, UpdateModel.ModelUpdater, false);
    return (contentItem, shape);
  }

  public async Task<ContentValidateResult> UpdateContentItem(
      ContentItem contentItem)
  {
    await Content.UpdateAsync(contentItem);
    return await Content.ValidateAsync(contentItem);
  }

  public MemberService(ISession session, IUserService userService,
      UserManager<User> users, IContentManager contentManager,
      IOrchardHelper orchardHelper,
      IContentItemDisplayManager contentItemDisplayManager,
      IUpdateModelAccessor updateModelAccessor,
      IHttpContextAccessor httpContextAccessor)
  {
    UserService = userService;
    Session = session;
    Orchard = orchardHelper;
    Content = contentManager;
    ContentDisplay = contentItemDisplayManager;
    UpdateModel = updateModelAccessor;
    Http = httpContextAccessor;
    Users = users;
  }

  // TODO: make better when link gets better
  // http://github.com/OrchardCMS/OrchardCore/blob/c2e1e3975db1b6e6d5a69940a3c5081969dd4bc2/src/OrchardCore/OrchardCore.Users.Core/Services/UserService.cs#L142s
  private UserManager<User> Users { get; }
  private async Task<User> GetCurrentUser(ClaimsPrincipal? principal = null)
  {
    return await Users.GetUserAsync(principal);
  }

  private IUserService UserService { get; }
  private ISession Session { get; }

  private IContentManager Content { get; }
  private IContentItemDisplayManager ContentDisplay { get; }
  private IUpdateModelAccessor UpdateModel { get; }
  private IHttpContextAccessor Http { get; }
  private IOrchardHelper Orchard { get; }
}