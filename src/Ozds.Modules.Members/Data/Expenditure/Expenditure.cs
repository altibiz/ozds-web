using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using Newtonsoft.Json;

namespace Ozds.Modules.Members;

public class Expenditure : ContentPart
{
  public NumericField InTotal { get; set; } = new();

  [JsonIgnore]
  public Lazy<ExpenditureData> Data { get; }

  public Expenditure()
  {
    Data = new Lazy<ExpenditureData>(
      () =>
        new ExpenditureData
        {
          InTotal = this.InTotal.Value ?? 0,
          Items = this.ContentItem
            .FromBag<ExpenditureItem>()!
            .Select(item => item.Data.Value),
        }
      );
  }
}

public readonly record struct ExpenditureData
{
  public readonly IEnumerable<ExpenditureItemData> Items { get; init; }
  public readonly decimal InTotal { get; init; }

  public static ExpenditureData FromItems(
      IEnumerable<ExpenditureItemData> items) =>
    new ExpenditureData
    {
      Items = items,
      InTotal = items.Aggregate(
          0M,
          (current, next) => current + next.Amount)
    };
};
