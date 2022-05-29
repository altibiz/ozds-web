﻿using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OrchardCore.Navigation;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public class AdminMenu : INavigationProvider
{
  public Task BuildNavigationAsync(
      string name,
      NavigationBuilder builder) =>
    Task.Run(() =>
      name.When(
        name => string.Equals(
          name, "admin",
          StringComparison.OrdinalIgnoreCase),
        _ =>
          Env.IsDevelopment() &&
          (Conf
            .GetSection("Ozds")
            .GetSection("Modules")
            .GetSection("Ozds")
            .GetValue<object?>("IsDemo") is null) ?
            BuildDevelopmentNavigation(builder)
          : BuildProductionNavigation(builder)));

  public NavigationBuilder BuildDevelopmentNavigation(
      NavigationBuilder builder) =>
    builder
      .Add(
        S["OZDS"],
        "0",
        root => root
          .Add(S["Cjenik"], "1", child => child
            .Action("List", "Admin",
              new
              {
                area = "OrchardCore.Contents",
                contentTypeId = "Catalogue"
              }),
            new[]
            {
              "icon-class-fas",
              "icon-class-fa-euro-sign"
            })
          .Add(S["ZDS"], "2", child => child
            .Action("List", "Admin",
              new
              {
                area = "OrchardCore.Contents",
                contentTypeId = "Center"
              }),
            new[]
            {
              "icon-class-fas",
              "icon-class-fa-bolt"
            })
          .Add(S["Korisnici ZDS-a"], "3", child => child
            .Action("List", "Admin",
              new
              {
                area = "OrchardCore.Contents",
                contentTypeId = "Consumer"
              }),
            new[]
            {
              "icon-class-fas",
              "icon-class-fa-users"
            })
          .Add(S["OMM"], "4", child => child
            .Action("List", "Admin",
              new
              {
                area = "OrchardCore.Contents",
                contentTypeId = "SecondarySite"
              }),
            new[]
            {
              "icon-class-fas",
              "icon-class-fa-tachometer"
            })
          .Add(S["Računi"], "5", child => child
            .Action("List", "Admin",
              new
              {
                area = "OrchardCore.Contents",
                contentTypeId = "Receipt",
              }),
            new[]
            {
              "icon-class-fas",
              "icon-class-fa-receipt"
            }),
        new[]
        {
          "icon-class-fas",
          "icon-class-fa-users"
        });

  public NavigationBuilder BuildProductionNavigation(
      NavigationBuilder builder) =>
    builder
      .Remove(_ => true)
      .Add(S["Cjenik"], "1", child => child
        .Action("List", "Admin",
          new
          {
            area = "OrchardCore.Contents",
            contentTypeId = "Catalogue"
          }),
        new[]
        {
          "icon-class-fas",
          "icon-class-fa-euro-sign"
        })
      .Add(S["ZDS"], "2", child => child
        .Action("List", "Admin",
          new
          {
            area = "OrchardCore.Contents",
            contentTypeId = "Center"
          }),
        new[]
        {
          "icon-class-fas",
          "icon-class-fa-bolt"
        })
      .Add(S["Korisnici ZDS-a"], "3", child => child
        .Action("List", "Admin",
          new
          {
            area = "OrchardCore.Contents",
            contentTypeId = "Consumer"
          }),
        new[]
        {
          "icon-class-fas",
          "icon-class-fa-users"
        })
      .Add(S["OMM"], "4", child => child
        .Action("List", "Admin",
          new
          {
            area = "OrchardCore.Contents",
            contentTypeId = "SecondarySite"
          }),
        new[]
        {
          "icon-class-fas",
          "icon-class-fa-tachometer"
        })
      .Add(S["Računi"], "5", child => child
        .Action("List", "Admin",
          new
          {
            area = "OrchardCore.Contents",
            contentTypeId = "Receipt",
          }),
        new[]
        {
          "icon-class-fas",
          "icon-class-fa-receipt"
        });

  public AdminMenu(
      IHostEnvironment env,
      IConfiguration conf,
      IStringLocalizer<AdminMenu> localizer)
  {
    Env = env;
    Conf = conf;

    S = localizer;
  }

  private IHostEnvironment Env { get; }
  private IConfiguration Conf { get; }

  private IStringLocalizer S { get; }
}
