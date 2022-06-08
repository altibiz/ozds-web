using OrchardCore.Taxonomies.Fields;
using Ozds.Elasticsearch;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public static class SiteMeasurementSource
{
  public const string ContentItemId = "4k4556m076b1vvsmmqjccbjwn5";
  public const string FakeSourceTermId = "4ajzfswsjftfzym6sj9fc223ms";
  public const string MyEnergyCommunityTermId = "481mx70mkj8y769kc85vqhw55c";
  public const string HelbOzdsTermId = "4k73fvdf8r38krb1fypjzgm9n3";

  public static Task<TagType?> GetSiteMeasurementSource(
      this TaxonomyCacheService taxonomy,
      string termId) =>
    taxonomy.GetTerm<TagType>(ContentItemId, termId);

  public static Task<TagType?> GetSiteMeasurementSource(
      this TaxonomyCacheService taxonomy,
      TaxonomyField field) =>
    taxonomy.GetSiteStatus(field.TermContentItemIds.First());

  public static string? GetDeviceMeasurementSource(
      string termId) =>
    SiteMeasurementSourceTermIdToDeviceMeasurementSource
      .GetOrDefault(termId);

  public static string? GetDeviceMeasurementSource(
      TaxonomyField field) =>
    GetDeviceMeasurementSource(field.TermContentItemIds.First());

  public static bool IsFake(string termId) =>
    termId == FakeSourceTermId;

  private readonly static IDictionary<string, string>
  SiteMeasurementSourceTermIdToDeviceMeasurementSource =
    new Dictionary<string, string>()
    {
      {
        FakeSourceTermId,
        FakeMeasurementProvider.FakeSource
      },
      {
        MyEnergyCommunityTermId,
        Elasticsearch.MyEnergyCommunity.Client.MyEnergyCommunitySource
      },
      {
        HelbOzdsTermId,
        Elasticsearch.HelbOzds.Client.HelbOzdsSource
      }
    };
}
