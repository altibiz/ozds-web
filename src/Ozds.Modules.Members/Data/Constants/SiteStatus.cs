namespace Ozds.Modules.Members;

public static class SiteStatus
{
  public const string ContentItemId = "47p9e5rkms3m012qv3z2t26jcg";
  public const string ActiveTermId = "444t1z24wt35hy89d6g3r5yan4";
  public const string InactiveTermId = "4k6wy5m7hn86m3fy8b49k4z5xg";
  public const string TemporarilyInactiveTermId = "4wv183b13azkq2vf2cq71wxmwz";

  public static Task<TagType?> GetSiteStatus(
      this TaxonomyCacheService taxonomy,
      string termId) =>
    taxonomy.GetTerm<TagType>(ContentItemId, termId);
}
