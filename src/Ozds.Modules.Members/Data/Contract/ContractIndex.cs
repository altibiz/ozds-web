using YesSql.Indexes;
using OrchardCore.Data;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ContractIndex : MapIndex
{
  public string ContentItemId { get; init; } = default!;
  public string CenterId { get; init; } = default!;
  public string MemberId { get; init; } = default!;
}

public class ContractIndexProvider :
  IndexProvider<ContentItem>,
  IScopedIndexProvider
{
  public override void Describe(
      DescribeContext<ContentItem> context) =>
    context
      .For<ContractIndex>()
      .Map(item => item.AsReal<Contract>()
        .WhenNonNullable(contract => contract.Center.ContentItemIds
          .SelectMany(centerId => contract.Member.ContentItemIds
            .Select(memberId =>
              new ContractIndex
              {
                ContentItemId = item.ContentItemId,
                CenterId = centerId,
                MemberId = memberId
              })),
        // NOTE: YesSql expects at least an empty enumerable here
        Enumerable.Empty<ContractIndex>()));
}
