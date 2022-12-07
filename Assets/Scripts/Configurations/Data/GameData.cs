using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "GameData", menuName = "GameData")]
public class GameData : ScriptableObject
{
    public GameDataModel DefaultGameData = new GameDataModel();

    public SettingModel DefaultSetting = new SettingModel();
}
}
