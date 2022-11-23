using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "DefaultGameData", menuName = "DefaultGameData")]
public class DefaultDataConfiguration : ScriptableObject
{
    public GameDataModel GameData = new GameDataModel()
    {
        CurrentDollarBalance = new MoneyModel(1000000, Currency.Dollar),
        CurrentGoldBalance = new MoneyModel(0, Currency.Gold),
        MinLevel = 1,
        MaxLevel = 27,
        MaxOpenedLevel = 0,
        MaxUpgradeLevel = 3,
        ActivationNumber = 0
    };

    public GameSettingModel Setting = new GameSettingModel()
    {
        MusicOn = true,
        SoundOn = true,
        NotificationsOn = true,
        AdsOn = true
    };
}
}
