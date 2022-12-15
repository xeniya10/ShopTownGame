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

    [Test] public void Initialize__CallsGameBoardControllerInitialization()
    {
        _gameplayController.Initialize();
        _gameBoardController.Received(1).Initialize();
    }

    [Test] public void Initialize__CallsManagerControllerInitialization()
    {
        _gameplayController.Initialize();
        _managerController.Received(1).Initialize();
    }

    [Test] public void Initialize__CallsUpgradeControllerInitialization()
    {
        _gameplayController.Initialize();
        _upgradeController.Received(1).Initialize();
    }

    [Test] public void Initialize_ByActivateEvent_InvokesFindImprovement()
    {
        _gameplayController.Initialize();
        _managerController.FindImprovement(Arg.Any<int>()).Returns(new ManagerPresenter(null, null));
        _upgradeController.FindImprovement(Arg.Any<int>()).Returns(new UpgradePresenter(null, null));

        _gameBoardController.ActivateEvent += Raise.Event<Action<GameCellModel>>(new GameCellModel());

        _managerController.Received(1).FindImprovement(Arg.Any<int>());
        _upgradeController.Received(1).FindImprovement(Arg.Any<int>());
    }

    [Test] public void Initialize_ByUnlockEvent_InvokesManagerUnlock()
    {
        _gameplayController.Initialize();
        _gameBoardController.UnlockEvent += Raise.Event<Action<int, bool>>(0, false);
        _managerController.Received(1).Unlock(0, false);
    }

    [Test] public void Initialize_ByUnlockEvent_InvokesUpgradeUnlock()
    {
        _gameplayController.Initialize();
        _gameBoardController.UnlockEvent += Raise.Event<Action<int, bool>>(0, false);
        _upgradeController.Received(1).Unlock(0, false);
    }

    [Test] public void Initialize_ByActivateEvent_InvokesInitializeManager()
    {
        _gameplayController.Initialize();
        _managerController.ActivateEvent += Raise.Event<Action<ImprovementModel>>(new ImprovementModel());
        _gameBoardController.Received(1).InitializeManager(Arg.Any<ImprovementModel>());
    }

    [Test] public void Initialize_ByActivateEvent_InvokesInitializeUpgrade()
    {
        _gameplayController.Initialize();
        _upgradeController.ActivateEvent += Raise.Event<Action<ImprovementModel>>(new ImprovementModel());
        _gameBoardController.Received(1).InitializeUpgrade(Arg.Any<ImprovementModel>());
    }
}
