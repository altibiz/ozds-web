using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOrchardCms().AddSetupFeatures("OrchardCore.AutoSetup");

builder.Services.Configure<IdentityOptions>(options => {
  options.Password.RequireDigit = false;
  options.Password.RequireLowercase = false;
  options.Password.RequireUppercase = false;
  options.Password.RequireNonAlphanumeric = false;
  options.Password.RequiredUniqueChars = 3;
  options.Password.RequiredLength = 6;
});

var app = builder.Build(); app.UseOrchardCore(); app.Run();
