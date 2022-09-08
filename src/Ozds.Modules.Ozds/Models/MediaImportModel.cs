using System.ComponentModel.DataAnnotations;

namespace Ozds.Modules.Ozds;

public class MediaImportModel
{
  [Required]
  public string? Urls { get; set; } = default;

  public string[]? Paths { get; set; } = default;
};