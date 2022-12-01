using ShopTown.PresenterComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.ControllerComponent
{
public class GameplayController : IInitializable
{
    [Inject] private readonly GameBoardController _gameBoardController;
    [Inject] private readonly ManagerController _managerController;
    [Inject] private readonly UpgradeController _upgradeController;

    public void Initialize()
    {
        _gameBoardController.Initialize();
        _managerController.Initialize();
        _upgradeController.Initialize();

        _gameBoardController.CellActivateEvent += InitializeImprovements;
        _gameBoardController.CellUnlockEvent += UnlockImprovements;

        _managerController.ImprovementActivateEvent += _gameBoardController.InitializeManager;
        _upgradeController.ImprovementActivateEvent += _gameBoardController.InitializeUpgrade;
    }

    private void UnlockImprovements(int level, bool isActivate)
    {
        _managerController.Unlock(level, isActivate);
        _upgradeController.Unlock(level, isActivate);
    }

    private void InitializeImprovements(GameCellPresenter cell)
    {
        cell.InitializeManager(_managerController.FindImprovement(cell.Model.Level).Model);
        cell.InitializeUpgrade(_upgradeController.FindImprovement(cell.Model.Level).Model);
    }
}
}
