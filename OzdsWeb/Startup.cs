using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OzdsWeb {
public class Startup {
  // This method gets called by the runtime. Use this method to add services to
  // the container. For more information on how to configure your application,
  // visit https://go.microsoft.com/fwlink/?LinkID=398940
  public void ConfigureServices(IServiceCollection services) {
    services
        .AddOrchardCms()
#if DEBUG
        .AddSetupFeatures("OrchardCore.AutoSetup")
#else
        .AddAzureShellsConfiguration() // put shells info into blob
#endif
        ;

    services.Configure<IdentityOptions>(options => {
      options.Password.RequireDigit = false;
      options.Password.RequireLowercase = false;
      options.Password.RequireUppercase = false;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequiredUniqueChars = 3;
      options.Password.RequiredLength = 6;
    });
  }

  // This method gets called by the runtime. Use this method to configure the
  // HTTP request pipeline.
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
    if (env.IsDevelopment()) {
      app.UseDeveloperExceptionPage();
    }

    app.UseRouting();

    app.UseOrchardCore();
  }
}
}