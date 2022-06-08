using OrchardCore.Taxonomies.Fields;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds;

public static class SitePhase
{
  public const string ContentItemId = "44sfyxngjywp5xtz7ggs45j9w4";
  public const string L1TermId = "4jf2ckcj5wp15w32xfmca9afnf";
  public const string L2TermId = "4n1ja6t83rbxjsjg7j89j6mzce";
  public const string L3TermId = "40303xxc59ycnx2rxn6rv5ysmr";
  public const string TriphasicTermId = "4zg6j54bzryenz2rfj9kr9bdch";

  public static Task<TagType?> GetPhase(
      this TaxonomyCacheService taxonomy,
      string termId) =>
    taxonomy.GetTerm<TagType>(ContentItemId, termId);

  public static Task<TagType?> GetPhase(
      this TaxonomyCacheService taxonomy,
      TaxonomyField field) =>
    taxonomy.GetPhase(field.TermContentItemIds.First());

  public static string? GetDevicePhase(string termId) =>
    SitePhaseTermIdToDevicePhase.GetOrDefault(termId);

  public static string? GetDevicePhase(TaxonomyField field) =>
    GetDevicePhase(field.TermContentItemIds.First());

  private readonly static IDictionary<string, string>
  SitePhaseTermIdToDevicePhase =
    new Dictionary<string, string>()
    {
      {
        L1TermId,
        Elasticsearch.DevicePhase.L1
      },
      {
        L2TermId,
        Elasticsearch.DevicePhase.L2
      },
      {
        L3TermId,
        Elasticsearch.DevicePhase.L3
      },
      {
        TriphasicTermId,
        Elasticsearch.DevicePhase.Triphasic
      }
    };
}
