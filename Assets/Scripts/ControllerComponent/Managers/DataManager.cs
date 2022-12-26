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

    private readonly string _key = "GameData";

    private readonly GameData _defaultData;

    public DataManager(IObjectResolver container)
    {
        _defaultData = container.Resolve<GameData>();
        InitializeGameData();
    }

    private void InitializeGameData()
    {
        _gameData = Load<GameDataModel>(_key);

        if (_gameData == null)
        {
            CreateDefaultData();
        }

        _gameData.ChangeEvent += () => Save(_key, _gameData);
        _gameData.Settings.ChangeEvent += () => Save(_key, _gameData);
    }

    private void CreateDefaultData()
    {
        _gameData = new GameDataModel();
        _gameData.SetDefaultData(_defaultData.GetDefaultModel());
    }

    public void Dispose()
    {
        Save(_key, _gameData);
    }
}
}
