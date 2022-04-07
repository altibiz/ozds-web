using Microsoft.AspNetCore.Identity;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var appData = Environment.GetEnvironmentVariable("ORCHARD_APP_DATA");
var logPathTemplate = Path.Join(appData, "log-{Date}.txt");

Console.WriteLine($"Env: {env}");
Console.WriteLine($"AppData: {appData}");
Console.WriteLine($"Log path template: {logPathTemplate}");

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

builder.Logging.AddFile(logPathTemplate,
    builder.Environment.IsDevelopment()? LogLevel.Debug: LogLevel.Information,
    builder.Configuration.GetSection("Logging")
        .GetSection("LogLevel")
        .GetChildren()
        .ToDictionary(x => x.Key, x => Enum.Parse<LogLevel>(x.Value)));

var app = builder.Build(); app.UseOrchardCore(); app.Run();
