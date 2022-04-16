using OrchardCore.ContentManagement.Display.Models;

namespace Ozds.Modules.Members;

public interface IFieldEditorSettings
{
  string GetFieldLabel(
      string propertyName, string defaultVale, bool isAdminTheme);

  DisplayModeResult GetFieldDisplayMode(string propertyName, string defaultMode,
      BuildFieldEditorContext context, bool isAdminTheme);
}

public struct DisplayModeResult
{
  public string? DisplayMode { get; }
  public bool IsVisible { get; }

  public DisplayModeResult(string? displayMode, bool isVisible = true)
  {
    DisplayMode = displayMode;
    IsVisible = isVisible;
  }

  public static implicit operator DisplayModeResult(bool visible)
  {
    return new DisplayModeResult(null, visible);
  }

  public static implicit operator DisplayModeResult(string displayMode)
  {
    return new DisplayModeResult(displayMode);
  }

  public static implicit operator bool(DisplayModeResult dm)
  {
    return dm.IsVisible;
  }

  public static implicit operator string?(DisplayModeResult dm)
  {
    return dm.DisplayMode;
  }
}