using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace Ozds.Themes.Ozds
{
  public class ResourceManagementOptionsConfiguration
      : IConfigureOptions<ResourceManagementOptions>
  {
    private static ResourceManifest _manifest;

    static ResourceManagementOptionsConfiguration()
    {
      _manifest = new ResourceManifest();

      _manifest
        .DefineScript("ozdstheme-bootstrap-bundle")
        .SetCdn(
          "https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.bundle.min.js",
          "https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.bundle.js")
        .SetCdnIntegrity(
          "sha384-gtEjrD/SeCtmISkJkNUaaKMoLD0//ElJ19smozuHV6z3Iehds+3Ulb9Bn9Plx0x4",
          "sha384-zlQmapo6noJSGz1A/oxylOFtN0k8EiXX45sOWv3x9f/RGYG0ECMxTbMao6+OLt2e")
        .SetVersion("5.0.1");

      _manifest
        .DefineScript("ozdstheme-jQuery")
        .SetCdn(
          "https://code.jquery.com/jquery-3.4.1.min.js",
          "https://code.jquery.com/jquery-3.4.1.js")
        .SetCdnIntegrity(
          "sha384-vk5WoKIaW/vJyUAd9n/wmopsmNhiy+L2Z+SBxGYnUkunIxVxAv/UtMOhba/xskxh",
          "sha384-mlceH9HlqLp7GMKHrj5Ara1+LvdTZVMx4S1U43/NxCvAkzIo8WJ0FE7duLel3wVo")
        .SetVersion("3.4.1");

      _manifest
        .DefineScript("ozdstheme-chartjs")
        .SetCdn(
          "https://cdn.jsdelivr.net/npm/chart.js@3.7.1/dist/chart.min.js",
          "https://cdn.jsdelivr.net/npm/chart.js@3.7.1/dist/chart.js")
        .SetCdnIntegrity(
            "sha256-ErZ09KkZnzjpqcane4SCyyHsKAXMvID9/xwbl/Aq1pc=",
            "sha256-gjQYlCMM/HIX3lODQvv5er+D4m6hfcSetDqlGj8FGj0=")
        .SetVersion("3.7.1");

      _manifest
        .DefineStyle("ozdstheme-bootstrap-oc")
        .SetUrl(
          "~/Ozds.Themes.Ozds/css/bootstrap-oc.min.css",
          "~/Ozds.Themes.Ozds/css/bootstrap-oc.css")
        .SetVersion("1.0.0");

      _manifest
        .DefineScript("ozdstheme")
        .SetDependencies("ozdstheme-bootstrap-bundle")
        .SetUrl(
            "~/Ozds.Themes.Ozds/js/scripts.min.js",
            "~/Ozds.Themes.Ozds/js/scripts.js")
        .SetVersion("6.0.0");

      _manifest
        .DefineScript("ozdstheme-custom")
        .SetDependencies("ozdstheme-jQuery")
        .SetUrl("~/Ozds.Themes.Ozds/js/custom.js")
        .SetVersion("6.0.0");

      _manifest
        .DefineScript("ozdstheme-libbcmath")
        .SetUrl("~/Ozds.Themes.Ozds/js/libbcmath.js");

      _manifest
        .DefineScript("ozdstheme-bcmath")
        .SetUrl("~/Ozds.Themes.Ozds/js/bcmath.js");

      _manifest
        .DefineScript("ozdstheme-pdf417")
        .SetUrl("~/Ozds.Themes.Ozds/js/pdf417.js")
        .SetVersion("1.0.005");

      _manifest
        .DefineStyle("ozdstheme")
        .SetUrl(
            "~/Ozds.Themes.Ozds/css/styles.min.css",
            "~/Ozds.Themes.Ozds/css/styles.css")
        .SetVersion("6.0.0");
    }

    public void Configure(ResourceManagementOptions options)
    {
      options.ResourceManifests.Add(_manifest);
    }
  }
}
