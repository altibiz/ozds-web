using Microsoft.AspNetCore.Identity;
using Ozds.Util;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var logging = builder.Logging;
var env = builder.Environment;

if (env.IsDevelopment())
{
  var vars = Environment.GetEnvironmentVariables();

  var aspNetCoreEnv = vars.Get<string>("ASPNETCORE_ENVIRONMENT");
  var dotnetEnv = vars.Get<string>("DOTNET_ENVIRONMENT");
  var orchardAppData = Path.GetFullPath(vars.Get<string>("ORCHARD_APP_DATA"));
  var logPathTemplate = Path.Join(orchardAppData, "log-{Date}.txt");

  Console.WriteLine($"ASP.NET Core Environment: {aspNetCoreEnv}");
  Console.WriteLine($".NET Environment: {dotnetEnv}");
  Console.WriteLine($"Orchard AppData: {orchardAppData}");
  Console.WriteLine($"Log path template: {logPathTemplate}");

  logging.AddFile(
    logPathTemplate,
    builder.Environment.IsDevelopment() ? LogLevel.Debug : LogLevel.Information,
    builder.Configuration
      .GetSection("Logging")
      .GetSection("LogLevel")
      .GetChildren()
      .ToDictionary(
        x => x.Key,
        x => Enum.Parse<LogLevel>(x.Value)));
}

services
  .Configure<IdentityOptions>(
    options =>
    {
      options.Password.RequireDigit = false;
      options.Password.RequireLowercase = false;
      options.Password.RequireUppercase = false;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequiredUniqueChars = 3;
      options.Password.RequiredLength = 6;
    });

services
  .AddOrchardCms()
  .AddSetupFeatures("OrchardCore.AutoSetup");

var app = builder.Build();
app.UseOrchardCore();
app.Run();
