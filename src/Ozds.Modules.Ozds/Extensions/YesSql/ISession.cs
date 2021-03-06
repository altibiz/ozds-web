using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Logging;
using YesSql;
using YesSql.Indexes;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Lists.Models;
using Ozds.Extensions;
using Ozds.Extensions.Disposable;

namespace Ozds.Modules.Ozds;

public static class ISessionExtensions
{
  public static ISession SaveJson<T>(
      this ISession session,
      string path) =>
    session.SaveJson(path, typeof(T));

  public static ISession SaveJson(
      this ISession session,
      string path,
      Type type) =>
    File
      .OpenText(path)
      .Using(stream =>
        JsonConvert
          .DeserializeObject(stream.ReadToEnd(), type)
          .ThrowWhenNull()
          .WithNonNull(@object =>
            {
              if (@object is IEnumerable<object> items)
              {
                foreach (var item in items)
                {
                  session.Save(item);
                }
              }
              else
              {
                session.Save(@object);
              }
            }))
      .Return(session);

  public static Task<ContentItem?> GetItemById(
      this ISession session,
      string contentItemId) =>
    session
      .Query<ContentItem, ContentItemIndex>(
          item => item.ContentItemId == contentItemId)
      .FirstOrDefaultAsync()
      .NullableTask();

  public static Task<ContentItem?> GetListParent(
      this ISession session, ContentItem child) =>
    child
      .As<ContainedPart>()
      .WhenNonNullFinallyAsync(part => session
        .GetItemById(part.ListContentItemId));

  public static Task<ContentItem> FirstOrDefaultAsync<TIndex>(
      this ISession session,
      IContentManager manager,
      Expression<Func<TIndex, bool>> query) where TIndex : class, IIndex =>
    session
      .Query<ContentItem, TIndex>(query)
      .FirstOrDefaultAsync(manager);

  public async static Task RefreshReduceIndex(this ISession templateSess,
      IIndexProvider indexProvider, string contentItemType = "",
      string collection = "", ILogger? logger = null)
  {
    templateSess.Store.Configuration.Logger = logger;
    var store = await StoreFactory.CreateAndInitializeAsync(
        templateSess.Store.Configuration);
    using var sess = (Session)store.CreateSession();
    sess.RegisterIndexes(indexProvider);
    var desc = GetDescriptor(sess, indexProvider);
    if (!typeof(ReduceIndex).IsAssignableFrom(desc.IndexType))
      throw new InvalidOperationException(
          "Wrong index type expected reduceIndex, got " + desc.IndexType);
    var conn = await sess.CreateConnectionAsync();
    conn.ClearReduceIndexTable(desc.IndexType, store.Configuration);
    desc.Delete = (ndx, map) => ndx; // disable deletion for new stuff
    var docs = await conn.GetContentItems(
        contentItemType, store.Configuration, collection);
    var items = sess.Get<ContentItem>(docs.ToList(), collection);
    int i = 1;
    foreach (var itm in items)
    {
      sess.Save(itm);
      if (i % 100 == 0)
        await sess.SaveChangesAsync();
      i++;
    }
    await sess.SaveChangesAsync();
  }

  public async static Task RefreshMapIndex(this ISession templateSes,
      IIndexProvider indexProvider, string contentItemType = "",
      string collection = "")
  {
    var store = await StoreFactory.CreateAndInitializeAsync(
        templateSes.Store.Configuration);
    using var sess = (Session)store.CreateSession();
    sess.RegisterIndexes(indexProvider);
    var conn = await sess.CreateConnectionAsync();
    var desc = GetDescriptor(sess, indexProvider);
    if (!typeof(MapIndex).IsAssignableFrom(desc.IndexType))
      throw new InvalidOperationException(
          "Wrong index type expected MapIndex, got " + desc.IndexType);
    var docs = await conn.GetContentItems(
        contentItemType, store.Configuration, collection);
    var items = sess.Get<ContentItem>(docs.ToList(), collection);
    foreach (var itm in items)
    {
      sess.Save(itm);
    }
    await sess.SaveChangesAsync();
  }

  private static IndexDescriptor GetDescriptor(
      this ISession sess, IIndexProvider indexProvider)
  {
    MethodInfo getDesc = sess.GetType().GetMethod(
        "GetDescriptors", BindingFlags.NonPublic | BindingFlags.Instance)!;
    var descs =
        (getDesc.Invoke(sess, new object[] { indexProvider.ForType(), "" })
                as IEnumerable<IndexDescriptor>)!;
    return descs.First();
  }
}
