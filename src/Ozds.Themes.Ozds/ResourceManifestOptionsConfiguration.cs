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
        .DefineScript("ozdstheme-global")
        .SetDependencies("bootstrap")
        .SetUrl(
            "~/Ozds.Themes.Ozds/js/scripts.min.js",
            "~/Ozds.Themes.Ozds/js/scripts.js");

      _manifest
        .DefineStyle("ozdstheme-global")
        .SetDependencies("ozdstheme-bootstrap", "ozdstheme-fa")
        .SetUrl(
            "~/Ozds.Themes.Ozds/css/styles.min.css",
            "~/Ozds.Themes.Ozds/css/styles.css");

      _manifest
        .DefineScript("ozdstheme-barcode")
        .SetDependencies("libbcmath", "bcmath", "pdf417");

      _manifest
        .DefineScript("libbcmath")
        .SetCdn(
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/libbcmath.min.js",
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/libbcmath.js")
        .SetVersion("master");

      _manifest
        .DefineScript("bcmath")
        .SetCdn(
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/bcmath.min.js",
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/bcmath.js")
        .SetVersion("master");

      _manifest
        .DefineScript("pdf417")
        .SetCdn(
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/libbcmath.min.js",
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/libbcmath.js")
        .SetVersion("master");

      _manifest
        .DefineScript("ozdstheme-chart")
        .SetDependencies("chartjs");

      _manifest
        .DefineScript("chartjs")
        .SetCdn(
          "https://cdn.jsdelivr.net/npm/chart.js@3.7.1/dist/chart.min.js",
          "https://cdn.jsdelivr.net/npm/chart.js@3.7.1/dist/chart.js")
        .SetCdnIntegrity(
            "sha256-ErZ09KkZnzjpqcane4SCyyHsKAXMvID9/xwbl/Aq1pc=",
            "sha256-gjQYlCMM/HIX3lODQvv5er+D4m6hfcSetDqlGj8FGj0=")
        .SetVersion("3.7.1");

      _manifest
        .DefineStyle("ozdstheme-fa")
        .SetDependencies("font-awesome");

      _manifest
        .DefineStyle("ozdstheme-bootstrap")
        .SetDependencies("bootstrap")
        .SetUrl(
          "~/Ozds.Themes.Ozds/css/bootstrap-oc.min.css",
          "~/Ozds.Themes.Ozds/css/bootstrap-oc.css");

      _manifest
        .DefineScript("ozdstheme-jquery-fileupload")
        .SetDependencies("ozdstheme-jquery", "jquery-fileupload");

      _manifest
        .DefineScript("jquery-fileupload")
        .SetCdn(
          "https://cdn.jsdelivr.net/npm/blueimp-file-upload@10.32.0/js/jquery.fileupload.min.js",
          "https://cdn.jsdelivr.net/npm/blueimp-file-upload@10.32.0/js/jquery.fileupload.js")
        .SetDependencies(
            "admin",
            "vuejs",
            "sortable",
            "vuedraggable",
            "jQuery-ui")
        .SetVersion("10.32.0");

      _manifest
        .DefineScript("ozdstheme-jquery")
        .SetDependencies("jQuery");

      _manifest
        .DefineScript("ozdstheme-codemirror")
        .SetDependencies("codemirror", "codemirror-mode-javascript");
    }

    public void Configure(ResourceManagementOptions options)
    {
      options.ResourceManifests.Add(_manifest);
    }
  }
}
