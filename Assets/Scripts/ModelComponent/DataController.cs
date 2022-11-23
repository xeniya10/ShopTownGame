using Newtonsoft.Json;
using ShopTown.Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShopTown.ModelComponent
{
public class StorageManager
{
    protected void Save(string key, object value)
    {
        var json = JsonConvert.SerializeObject(value, Formatting.Indented);
        PlayerPrefs.SetString(key, json);
    }

    protected string Load(string key)
    {
        return PlayerPrefs.GetString(key);
    }
}

public class DataController : StorageManager, IInitializable
{
    public GameDataModel GameData;
    public GameSettingModel Settings;

    private readonly string _gameDataKey = "GameData";
    private readonly string _settingKey = "Setting";

    [Inject] private readonly DefaultDataConfiguration _defaultData;

    public void Initialize()
    {
        // PlayerPrefs.DeleteKey(_gameDataKey);
        // PlayerPrefs.DeleteKey(_settingKey);
        SetData(ref GameData, _gameDataKey, _defaultData.GameData);
        GameData.ChangeEvent += () => Save(_gameDataKey, GameData);

        SetData(ref Settings, _settingKey, _defaultData.Setting);
        Settings.ChangeEvent += () => Save(_settingKey, Settings);
    }

    private void SetData<T>(ref T variable, string key, T defaultValue)
    {
        variable = JsonConvert.DeserializeObject<T>(Load(key));

        if (variable == null)
        {
            variable = defaultValue;
        }
    }

    public void SaveGameData()
    {
        Save(_gameDataKey, GameData);
    }

    public void SaveSettings()
    {
        Save(_settingKey, Settings);
    }
}
}
