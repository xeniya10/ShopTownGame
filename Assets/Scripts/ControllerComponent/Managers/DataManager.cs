using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using VContainer;

namespace ShopTown.ControllerComponent
{
public class DataManager : StorageManager, IGameData, IDisposable
{
    public GameDataModel GameData { get { return _gameData; } }
    private GameDataModel _gameData;

    private readonly string _gameDataKey = "GameData";

    private readonly GameData _defaultData;

    public DataManager(IObjectResolver container)
    {
        _defaultData = container.Resolve<GameData>();
        InitializeGameData();
    }

    private void InitializeGameData()
    {
        Load(ref _gameData, _gameDataKey);

        if (_gameData == null)
        {
            CreateDefaultData();
        }

        _gameData.ChangeEvent += () => Save(_gameDataKey, _gameData);
        _gameData.Settings.ChangeEvent += () => Save(_gameDataKey, _gameData);
    }

    private void CreateDefaultData()
    {
        _gameData = new GameDataModel();
        _gameData.SetDefaultData(_defaultData.GetDefaultModel());
    }

    public void Dispose()
    {
        Save(_gameDataKey, _gameData);
    }
}
}
