using Ozds.Modules.Members.Base;
using Ozds.Modules.Members.PartFieldSettings;
using Ozds.Modules.Members.Utils;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Handlers;
using System.Threading.Tasks;

namespace Ozds.Modules.Members.Payments
{
  public class PaymentPartService : PartService<Payment>
  {
    public PaymentPartService(IHttpContextAccessor httpAccessorService)
        : base(httpAccessorService) { }

    public override Task UpdatedAsync<TPart>(
        UpdateContentContext context, Payment instance)
    {
      instance.InitFields();
      if (instance.IsPayout.Value ^ instance.Amount.Value < 0)
        instance.Amount.Value = -instance.Amount.Value;
      return Task.CompletedTask;
    }
  }
}
