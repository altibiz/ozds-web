using System.Security.Claims;
using Microsoft.AspNetCore.Http;
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
using Ozds.Util;
using ISession = YesSql.ISession;

namespace Ozds.Modules.Members;

// TODO: does this make any sense?
// TODO: use in migrations?
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

  public async Task<ContentItem?> GetContentItemById(string contentItemId)
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
      IContentManager contentManager,
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
  }

  // TODO: better
  private Task<User> GetCurrentUser(ClaimsPrincipal? principal = null) =>
    UserService
      .GetAuthenticatedUserAsync(principal)
      .Then(user => user.As<User>())!;

  private IUserService UserService { get; }
  private ISession Session { get; }

  private IContentManager Content { get; }
  private IContentItemDisplayManager ContentDisplay { get; }
  private IUpdateModelAccessor UpdateModel { get; }
  private IHttpContextAccessor Http { get; }
  private IOrchardHelper Orchard { get; }
}