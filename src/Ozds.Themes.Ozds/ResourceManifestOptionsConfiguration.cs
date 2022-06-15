using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

// NOTE: move some of these to the module

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
        .DefineScript("ozdstheme-graphql")
        .SetDependencies(
          "luxon",
          "regenerator-runtime")
        .SetUrl(
          "~/Ozds.Themes.Ozds/js/graphql.min.js",
          "~/Ozds.Themes.Ozds/js/graphql.js");

      _manifest
        .DefineScript("ozdstheme-barcode")
        .SetDependencies("libbcmath", "bcmath", "pdf417");

      // NOTE: min is dynamic, so no SRI
      _manifest
        .DefineScript("libbcmath")
        .SetCdn(
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/libbcmath.min.js",
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/libbcmath.js")
        .SetVersion("master");

      // NOTE: min is dynamic, so no SRI
      _manifest
        .DefineScript("bcmath")
        .SetCdn(
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/bcmath.min.js",
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/bcmath.js")
        .SetVersion("master");

      // NOTE: min is dynamic, so no SRI
      _manifest
        .DefineScript("pdf417")
        .SetCdn(
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/libbcmath.min.js",
          "https://cdn.jsdelivr.net/gh/pkoretic/pdf417-generator@master/lib/libbcmath.js")
        .SetVersion("master");

      _manifest
        .DefineScript("ozdstheme-chart")
        .SetDependencies(
          "chartjs",
          "chartjs-adapter-luxon",
          "luxon");

      _manifest
        .DefineScript("chartjs-adapter-luxon")
        .SetCdn(
          "https://cdn.jsdelivr.net/npm/chartjs-adapter-luxon@1.1.0/dist/chartjs-adapter-luxon.min.js",
          "https://cdn.jsdelivr.net/npm/chartjs-adapter-luxon@1.1.0/dist/chartjs-adapter-luxon.js")
        .SetCdnIntegrity(
          // TODO: find a way to ignore this codespell warning
          "sha256-tOhXNe/Ue+TjR33s/CryFYOGMwNfkTjTuvM4LEOAHzc=",
          "sha256-q2NAytoP6eS3ONNWvLhccUNJ2kTlP++ZAhU89aIKt2Y=")
        .SetVersion("1.1.0");

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
        .SetCdnIntegrity(
          "sha256-4RfqSBFKfPPSZG1qUfDKAfuRkU36AI4QgfxyXWLcIMM=",
          "sha256-NGbgsp9o9UL0oC1p3Kz6FWhyqTAAnY8K/YX3F1FcLD8=")
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

      _manifest
        .DefineScript("ozdstheme-luxon")
        .SetDependencies("luxon");

      _manifest
        .DefineScript("luxon")
        .SetCdn(
          "https://cdn.jsdelivr.net/npm/luxon@2.4.0/build/global/luxon.min.js",
          "https://cdn.jsdelivr.net/npm/luxon@2.4.0/build/global/luxon.js")
        .SetCdnIntegrity(
          "sha256-DIsAGD2EF8Qq2PCH9yzX/yt9FliJfWf+aGcdgR6tKwo=",
          "sha256-DGUtr8kBmLOJRqQqoM5aWfE/v0CwH+JgCDNaqZZrHFM=")
        .SetVersion("2.4.0");

      // NOTE: needed for async/await
      // NOTE: min is dynamic, so no SRI
      _manifest
        .DefineScript("regenerator-runtime")
        .SetCdn(
          "https://cdn.jsdelivr.net/npm/regenerator-runtime@0.13.9/runtime.min.js",
          "https://cdn.jsdelivr.net/npm/regenerator-runtime@0.13.9/runtime.js")
        .SetVersion("0.13.9");
    }

    public void Configure(ResourceManagementOptions options)
    {
      options.ResourceManifests.Add(_manifest);
    }
  }
}
