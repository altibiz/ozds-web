using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace Ozds.Modules.Ozds;

public interface ITenantActivationChecker
{
  public bool WasTenantActivated();
}

public class TenantActivationChecker : ITenantActivationChecker
{
  public bool WasTenantActivated()
  {
    using (var scope = Services.CreateScope())
    {
      var @event = scope.ServiceProvider
        .GetRequiredService<TenantActivatedEvent>();

      return @event?.TenantActivated ?? false;
    }
  }

  public TenantActivationChecker(IServiceProvider services)
  {
    Services = services;
  }

  private IServiceProvider Services { get; }
}

public class TenantActivatedEvent : ModularTenantEvents
{
  public override Task ActivatedAsync()
  {
    TenantActivated = true;
    return Task.CompletedTask;
  }

  public TenantActivatedEvent()
  {
    TenantActivated = false;
  }

  public bool TenantActivated { get; private set; }
}
