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
      Amount = decimal.Round(valueTo - valueFrom),
      UnitPrice = unitPrice,
      InTotal = decimal.Round(
        decimal.Round(valueTo - valueFrom, 2) * unitPrice,
        2),
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
      Amount = decimal.Round(amount),
      UnitPrice = unitPrice,
      InTotal = decimal.Round(unitPrice * amount, 2)
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
      InTotal = decimal.Round(unitPrice, 2)
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
