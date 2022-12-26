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

    [Inject] private readonly IShowable<IGameData> _welcomeScreen;
    [Inject] private readonly IShowable<GameCellModel> _newBusinessProfile;

    public void Initialize()
    {
        _gameBoardController.Initialize();
        _managerController.Initialize();
        _upgradeController.Initialize();

        _gameBoardController.CellActivationEvent += InitializeImprovements;
        _gameBoardController.CellUnlockEvent += UnlockImprovements;

        _managerController.ImprovementActivationEvent += _gameBoardController.InitializeManager;
        _upgradeController.ImprovementActivationEvent += _gameBoardController.InitializeUpgrade;

        _gameBoardController.SetOfflineProfitEvent += _welcomeScreen.Show;
        _gameBoardController.CellActivationEvent += _newBusinessProfile.Show;
    }

    private void UnlockImprovements(int level, bool isCellActivated)
    {
        _managerController.UnlockImprovement(level, isCellActivated);
        _upgradeController.UnlockImprovement(level, isCellActivated);
    }

    private void InitializeImprovements(GameCellModel cell)
    {
        _gameBoardController.InitializeManager(_managerController.FindImprovement(cell.Level).Model);
        _gameBoardController.InitializeUpgrade(_upgradeController.FindImprovement(cell.Level).Model);
    }
}
}
