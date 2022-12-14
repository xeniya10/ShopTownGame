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
        DeleteKey(_gameDataKey);
        InitializeGameData();
    }

    private void InitializeGameData()
    {
        SetData(ref _gameData, _gameDataKey, () =>
        {
            _gameData = new GameDataModel();
            _gameData.SetDefaultData(_defaultData.DefaultGameData);
        });

        _gameData.ChangeEvent += () => Save(_gameDataKey, _gameData);
        _gameData.Settings.ChangeEvent += () => Save(_gameDataKey, _gameData);
    }

    public void Dispose()
    {
        Save(_gameDataKey, _gameData);
    }
}

public interface IGameData
{
    public GameDataModel GameData { get; }
}
}
