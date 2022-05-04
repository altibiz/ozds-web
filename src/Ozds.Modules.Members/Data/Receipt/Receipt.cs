using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Ozds.Modules.Members;

public class Receipt : ContentPart
{
  public ContentPickerField Site { get; set; } = new();
  public DateField Date { get; set; } = new();
  public NumericField InTotal { get; set; } = new();
  public NumericField Tax { get; set; } = new();
  public NumericField InTotalWithTax { get; set; } = new();
  public ReceiptData? Data { get; set; } = null;
}

public record ReceiptData
(ReceiptDataPerson Operator,
 ReceiptDataPerson Consumer,
 ReceiptDataCalculation Calculation,
 IEnumerable<ReceiptDataItem> Items,
 DateTime Date,
 decimal InTotal,
 decimal Tax,
 decimal InTotalWithTax);

public record ReceiptDataCalculation
(string SiteContentItemId,
 string TariffModelTermId,
 DateTime DateFrom,
 DateTime DateTo,
 decimal MeasurementServiceFee,
 ReceiptDataExpenditure UsageExpenditure,
 ReceiptDataExpenditure SupplyExpenditure);

public record ReceiptDataExpenditure
(IEnumerable<ReceiptDataExpenditureItem> Items,
 decimal InTotal);

public record ReceiptDataExpenditureItem
(string TariffItemTermId,
 decimal ValueFrom,
 decimal ValueTo,
 decimal Consumption,
 decimal UnitPrice,
 decimal Amount);

public record ReceiptDataItem
(string OrdinalNumber,
 string ArticleTermId,
 decimal Amount,
 decimal Price,
 decimal InTotal);

public record ReceiptDataPerson
(string Title,
 string Oib,
 string Address,
 string City,
 string PostalCode,
 string Contact);
