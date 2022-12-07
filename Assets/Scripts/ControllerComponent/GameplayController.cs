using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.ControllerComponent
{
public class GameplayController : IInitializable
{
    [Inject] private readonly IGameBoardController _gameBoardController;
    [Inject] private readonly IImprovementController<ManagerPresenter> _managerController;
    [Inject] private readonly IImprovementController<UpgradePresenter> _upgradeController;

    public void Initialize()
    {
        _gameBoardController.Initialize();
        _managerController.Initialize();
        _upgradeController.Initialize();

        _gameBoardController.ActivateEvent += InitializeImprovements;
        _gameBoardController.UnlockEvent += UnlockImprovements;

        _managerController.ActivateEvent += _gameBoardController.InitializeManager;
        _upgradeController.ActivateEvent += _gameBoardController.InitializeUpgrade;
    }

    private void UnlockImprovements(int level, bool isActivate)
    {
        _managerController.Unlock(level, isActivate);
        _upgradeController.Unlock(level, isActivate);
    }

    private void InitializeImprovements(GameCellModel cell)
    {
        _gameBoardController.InitializeManager(_managerController.FindImprovement(cell.Level).Model);
        _gameBoardController.InitializeUpgrade(_upgradeController.FindImprovement(cell.Level).Model);
    }
}
}
