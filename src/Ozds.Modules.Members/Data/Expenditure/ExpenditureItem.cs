using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.ContentManagement;
using Ozds.Util;

namespace Ozds.Modules.Members;

public class ExpenditureItem : ContentPart
{
  public TaxonomyField TariffItem { get; set; } = new();
  public NumericField ValueFrom { get; set; } = new();
  public NumericField ValueTo { get; set; } = new();
  public NumericField Consumption { get; set; } = new();
  public NumericField UnitPrice { get; set; } = new();
  public NumericField Amount { get; set; } = new();
}

public readonly record struct ExpenditureItemData
{
  public readonly string TariffItemTermId { get; init; }
  public readonly string Title { get; init; }
  public readonly decimal ValueFrom { get; init; }
  public readonly decimal ValueTo { get; init; }
  public readonly decimal Amount { get; init; }
  public readonly decimal UnitPrice { get; init; }
  public readonly decimal InTotal { get; init; }

  public static ExpenditureItemData Create(
      TariffTagType tag,
      decimal valueFrom,
      decimal valueTo,
      decimal unitPrice) =>
    new()
    {
      TariffItemTermId = tag.ContentItem.ContentItemId,
      Title = tag.TariffTag.Value.Abbreviation.Text,
      ValueFrom = valueFrom,
      ValueTo = valueTo,
      Amount = valueTo - valueFrom,
      UnitPrice = unitPrice,
      InTotal = (valueTo - valueFrom) * unitPrice,
    };

  public static ExpenditureItemData Create(
      TariffTagType tag,
      decimal amount,
      decimal unitPrice) =>
    new()
    {
      TariffItemTermId = tag.ContentItem.ContentItemId,
      Title = tag.TariffTag.Value.Abbreviation.Text,
      ValueFrom = default,
      ValueTo = default,
      Amount = amount,
      UnitPrice = unitPrice,
      InTotal = unitPrice * amount
    };

  public static ExpenditureItemData Create(
      TariffTagType tag,
      decimal unitPrice) =>
    new()
    {
      TariffItemTermId = tag.ContentItem.ContentItemId,
      Title = tag.TariffTag.Value.Abbreviation.Text,
      ValueFrom = default,
      ValueTo = default,
      Amount = 1M,
      UnitPrice = unitPrice,
      InTotal = unitPrice
    };
}

public static class ExpenditureItemDataTaxonomyExtensions
{
  public static Task<ExpenditureItemData> CreateExpenditureItemData(
      this TaxonomyCacheService taxonomy,
      string termId,
      decimal valueFrom,
      decimal valueTo,
      decimal unitPrice) =>
    taxonomy
      .GetTariffItem(termId)
      .ThenWhenNonNullable(tag => ExpenditureItemData
        .Create(tag, valueFrom, valueTo, unitPrice));

  public static Task<ExpenditureItemData> CreateExpenditureItemData(
      this TaxonomyCacheService taxonomy,
      string termId,
      decimal amount,
      decimal unitPrice) =>
    taxonomy
      .GetTariffItem(termId)
      .ThenWhenNonNullable(tag => ExpenditureItemData
        .Create(tag, amount, unitPrice));

  public static Task<ExpenditureItemData> CreateExpenditureItemData(
      this TaxonomyCacheService taxonomy,
      string termId,
      decimal unitPrice) =>
    taxonomy
      .GetTariffItem(termId)
      .ThenWhenNonNullable(tag => ExpenditureItemData
        .Create(tag, unitPrice));
}
