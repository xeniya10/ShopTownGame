using System;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.ControllerComponent
{
public class GameplayController : IInitializable, IDisposable
{
    [Inject] private DataController _data;
    [Inject] private readonly GameScreenPresenter _gameScreenPresenter;
    [Inject] private readonly GameBoardController _gameBoardController;
    [Inject] private readonly ManagerController _managerController;
    [Inject] private readonly UpgradeController _upgradeController;

    public void Initialize()
    {
        _data.Initialize();
        _gameScreenPresenter.Initialize(ref _data);

        _gameBoardController.Initialize(ref _data.GameData);
        _managerController.Initialize(ref _data.GameData);
        _upgradeController.Initialize(ref _data.GameData);

        _gameBoardController.CellActivateEvent += InitializeImprovements;
        _gameBoardController.CellUnlockEvent += CheckImprovements;

        _managerController.ImprovementActivateEvent += _gameBoardController.InitializeManager;
        _upgradeController.ImprovementActivateEvent += _gameBoardController.InitializeUpgrade;
    }

    private void CheckImprovements(int level, bool isActivate)
    {
        _managerController.Unlock(level, isActivate);
        _upgradeController.Unlock(level, isActivate);
    }

    private void InitializeImprovements(GameCellPresenter cell)
    {
        cell.InitializeManager(_managerController.FindImprovement(cell.Model.Level).Model);
        cell.InitializeUpgrade(_upgradeController.FindImprovement(cell.Model.Level).Model);
        ShowNewLevelProfiler(cell.Model.Level);
    }

    private void ShowNewLevelProfiler(int level)
    {
        if (level > _data.GameData.MaxOpenedLevel)
        {
            _gameScreenPresenter.ShowNewBusinessScreen(level);
            _data.GameData.SetMaxOpenedLevel(level);
        }
    }

    public void Dispose()
    {
        _data.SaveGameData();
        _data.SaveSettings();

        _gameBoardController.SaveData();
        _managerController.SaveData();
        _upgradeController.SaveData();
    }
}
}
