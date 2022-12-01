using System;
using Newtonsoft.Json;
using ShopTown.Data;
using UnityEngine;
using VContainer;

namespace ShopTown.ModelComponent
{
public class DataManager : StorageManager, IGameData, IDisposable
{
    public GameDataModel GameData { get { return _gameData; } }
    public GameSettingModel Settings { get { return _settings; } }

    private readonly GameDataModel _gameData;
    private readonly GameSettingModel _settings;

    private readonly string _gameDataKey = "GameData";
    private readonly string _settingKey = "Setting";

    private readonly DefaultDataConfiguration _defaultData;

    public DataManager(IObjectResolver container)
    {
        _defaultData = container.Resolve<DefaultDataConfiguration>();

        DeleteKey(_gameDataKey);
        DeleteKey(_settingKey);

        SetData(ref _gameData, _gameDataKey, _defaultData.GameData);
        _gameData.ChangeEvent += () => Save(_gameDataKey, _gameData);

        SetData(ref _settings, _settingKey, _defaultData.Setting);
        _settings.ChangeEvent += () => Save(_settingKey, _settings);
    }

    public void Dispose()
    {
        Save(_gameDataKey, _gameData);
        Save(_settingKey, _settings);
    }
}

public abstract class StorageManager
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

    protected void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    protected void SetData<T>(ref T variable, string key, T defaultValue)
    {
        variable = JsonConvert.DeserializeObject<T>(Load(key));

        if (variable == null)
        {
            variable = defaultValue;
        }
    }

    protected void SetData<T>(ref T variable, string key, Action creationDefaultValue)
    {
        variable = JsonConvert.DeserializeObject<T>(Load(key));

        if (variable == null)
        {
            creationDefaultValue?.Invoke();
        }
    }
}

public interface IGameData
{
    public GameDataModel GameData { get; }
    public GameSettingModel Settings { get; }
}
}
