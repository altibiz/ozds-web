namespace Lombiq.HelpfulExtensions.Extensions.CodeGeneration
{
  public class ContentTypeMigrationsViewModel
  {
    internal Lazy<string> MigrationCodeLazy { get; set; } = default!;
    public string MigrationCode => MigrationCodeLazy.Value;

    internal Lazy<string> ModelCodeLazy { get; set; } = default!;
    public string ModelCode => ModelCodeLazy.Value;
  }
}
