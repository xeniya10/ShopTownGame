using System;
using NSubstitute;
using NUnit.Framework;
using ShopTown.ControllerComponent;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;

public class GameplayControllerTester
{
    private IGameBoardController _gameBoardController;
    private IImprovementController<ManagerPresenter> _managerController;
    private IImprovementController<UpgradePresenter> _upgradeController;
    private GameplayController _gameplayController;

    [SetUp] public void CreateTestObjects()
    {
        _gameBoardController = Substitute.For<IGameBoardController>();
        _managerController = Substitute.For<IImprovementController<ManagerPresenter>>();
        _upgradeController = Substitute.For<IImprovementController<UpgradePresenter>>();
        _gameplayController = new GameplayController(_gameBoardController, _managerController, _upgradeController);
    }

    [Test] public void SuccessfulInitializationCallTest()
    {
        _gameplayController.Initialize();

        _gameBoardController.Received(1).Initialize();
        _managerController.Received(1).Initialize();
        _upgradeController.Received(1).Initialize();
    }

    [Test] public void SuccessfulCellActivationCallTest()
    {
        _gameplayController.Initialize();
        _managerController.FindImprovement(Arg.Any<int>()).Returns(new ManagerPresenter(null, null));
        _upgradeController.FindImprovement(Arg.Any<int>()).Returns(new UpgradePresenter(null, null));

        _gameBoardController.ActivateEvent += Raise.Event<Action<GameCellModel>>(new GameCellModel());

        _managerController.Received(1).FindImprovement(Arg.Any<int>());
        _upgradeController.Received(1).FindImprovement(Arg.Any<int>());
    }

    [Test] public void SuccessfulCellUnlockCallTest()
    {
        _gameplayController.Initialize();

        _gameBoardController.UnlockEvent += Raise.Event<Action<int, bool>>(0, false);

        _managerController.Received(1).Unlock(0, false);
        _upgradeController.Received(1).Unlock(0, false);
    }

    [Test] public void SuccessfulImprovementActivationCallTest()
    {
        _gameplayController.Initialize();

        _managerController.ActivateEvent += Raise.Event<Action<ImprovementModel>>(new ImprovementModel());
        _upgradeController.ActivateEvent += Raise.Event<Action<ImprovementModel>>(new ImprovementModel());

        _gameBoardController.Received(1).InitializeManager(Arg.Any<ImprovementModel>());
        _gameBoardController.Received(1).InitializeUpgrade(Arg.Any<ImprovementModel>());
    }
}
