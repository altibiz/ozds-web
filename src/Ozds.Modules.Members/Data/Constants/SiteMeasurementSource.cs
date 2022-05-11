namespace Ozds.Modules.Members;

public static class SiteMeasurementSource
{
  public const string ContentItemId = "4k4556m076b1vvsmmqjccbjwn5";
  public const string FakeSourceTermId = "4ajzfswsjftfzym6sj9fc223ms";
  public const string MyEnergyCommunityTermId = "4k73fvdf8r38krb1fypjzgm9n3";
  public const string HelbOzdsTermId = "481mx70mkj8y769kc85vqhw55c";

  public static Task<TagType?> GetSiteMeasurementSource(
      this TaxonomyCacheService taxonomy,
      string termId) =>
    taxonomy.GetTerm<TagType>(ContentItemId, termId);
}
