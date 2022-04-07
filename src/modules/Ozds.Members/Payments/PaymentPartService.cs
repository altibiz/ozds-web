using Ozds.Members.Base;
using Ozds.Members.PartFieldSettings;
using Ozds.Members.Utils;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Handlers;
using System.Threading.Tasks;

namespace Ozds.Members.Payments {
  public class PaymentPartService : PartService<Payment> {
    public PaymentPartService(IHttpContextAccessor httpAccessorService)
        : base(httpAccessorService) {}

    public override Task UpdatedAsync<TPart>(
        UpdateContentContext context, Payment instance) {
      instance.InitFields();
      if (instance.IsPayout.Value ^ instance.Amount.Value < 0)
        instance.Amount.Value = -instance.Amount.Value;
      return Task.CompletedTask;
    }
  }
}
