using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using VContainer;

namespace ShopTown.ControllerComponent
{
public class DataManager : StorageManager, IGameData, IDisposable
{
    public GameDataModel GameData { get { return _gameData; } }
    public SettingModel Settings { get { return _settings; } }

    private GameDataModel _gameData;
    private SettingModel _settings;

    private readonly string _gameDataKey = "GameData";
    private readonly string _settingKey = "Setting";

    private readonly GameData _defaultData;

    public DataManager(IObjectResolver container)
    {
        _defaultData = container.Resolve<GameData>();

        DeleteKey(_gameDataKey);
        DeleteKey(_settingKey);

        InitializeGameData();
        InitializeSettings();
    }

    private void InitializeGameData()
    {
        SetData(ref _gameData, _gameDataKey, () =>
        {
            _gameData = new GameDataModel();
            _gameData.SetDefaultData(_defaultData.DefaultGameData);
        });

        _gameData.ChangeEvent += () => Save(_gameDataKey, _gameData);
    }

    private void InitializeSettings()
    {
        SetData(ref _settings, _settingKey, () =>
        {
            _settings = new SettingModel();
            _settings.SetDefaultData(_defaultData.DefaultSetting);
        });

        _settings.ChangeEvent += () => Save(_settingKey, _settings);
    }

    public void Dispose()
    {
        Save(_gameDataKey, _gameData);
        Save(_settingKey, _settings);
    }
}

public interface IGameData
{
    public GameDataModel GameData { get; }
    public SettingModel Settings { get; }
}
}
