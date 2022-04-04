namespace Elasticsearch.Test
{
  public static class PeriodExtensions
  {
    public static Period Combine(this Period start,
        Period end) => new Period { From = start.From, To = end.To };
  }
}
