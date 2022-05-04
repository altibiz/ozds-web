using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ReceiptHandler : ContentHandlerBase
{
  public override Task UpdatingAsync(UpdateContentContext context) =>
    context.ContentItem.As<Receipt>()
      .WhenNonNullable(receipt => receipt.Site.ContentItemIds.FirstOrDefault()
        .With(
          siteId =>
            {

            }))
      .Return(Task.CompletedTask);
}
