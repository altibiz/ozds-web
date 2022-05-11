using Ozds.Util;

namespace Ozds.Modules.Members;

public static class OperatorCatalogue
{
  public const string WhiteHighVoltageContentItemId =
    "468ng0rw168cp78ssaecqh3adn";
  public const string WhiteMediumVoltageContentItemId =
    "46k372fszexy61xan98qkz42w0";
  public const string BlueContentItemId =
    "4z8pmcx911avk5xkxm6wr4w9wh";
  public const string WhiteLowVoltageContentItemId =
    "4hyntwsawqkety42v8q4dgphn7";
  public const string RedContentItemId =
    "403658syw0s92swkkevr7h67cs";
  public const string YellowContentItemId =
    "4tnr9dxj784q607fvxdbgeq6nx";

  public static string? ContentItemIdFor(string tariffModelTermId) =>
    s_contentItemIdByTariffModelTermId.GetOrDefault(tariffModelTermId);

  private static readonly IDictionary<string, string>
      s_contentItemIdByTariffModelTermId =
    new Dictionary<string, string>
    {
      { TariffModel.WhiteHighVoltageTermId, WhiteHighVoltageContentItemId },
      { TariffModel.WhiteMediumVoltageTermId, WhiteMediumVoltageContentItemId },
      { TariffModel.BlueTermId, BlueContentItemId },
      { TariffModel.WhiteLowVoltageTermId, WhiteLowVoltageContentItemId },
      { TariffModel.RedTermId, RedContentItemId },
      { TariffModel.YellowTermId, YellowContentItemId },
    };
}
