using System;
using NUnit.Framework;
using ShopTown.ControllerComponent;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using VContainer;

public class GameplayControllerTester
{
    private IObjectResolver _container;

    public class TestGameBoardController : IGameBoardController
    {
        public bool IsInitialized;
        public bool IsManagerInitialized;
        public bool IsUpgradeInitialized;

        public event Action<GameCellModel> ActivateEvent;
        public event Action<int, bool> UnlockEvent;

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void InitializeManager(ImprovementModel improvement)
        {
            IsManagerInitialized = true;
        }

        public void InitializeUpgrade(ImprovementModel improvement)
        {
            IsUpgradeInitialized = true;
        }

        public void InvokeActivateEvent(GameCellModel model = null)
        {
            ActivateEvent?.Invoke(model);
        }

        public void InvokeUnlockEvent()
        {
            UnlockEvent?.Invoke(0, false);
        }
    }

    public class TestManagerController : IImprovementController<ManagerPresenter>
    {
        public bool IsInitialized;
        public bool IsFound;
        public bool IsUnlocked;
        public event Action<ImprovementModel> ActivateEvent;

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void Unlock(int level, bool isCellActivated)
        {
            IsUnlocked = true;
        }

        public ImprovementPresenter FindImprovement(int level)
        {
            IsFound = true;
            return new ManagerPresenter(null, null);
        }

        public void InvokeActivateEvent()
        {
            ActivateEvent?.Invoke(null);
        }
    }

    public class TestUpgradeController : IImprovementController<UpgradePresenter>
    {
        public bool IsInitialized;
        public bool IsFound;
        public bool IsUnlocked;
        public event Action<ImprovementModel> ActivateEvent;

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void Unlock(int level, bool isCellActivated)
        {
            IsUnlocked = true;
        }

        public ImprovementPresenter FindImprovement(int level)
        {
            IsFound = true;
            return new UpgradePresenter(null, null);
        }

        public void InvokeActivateEvent()
        {
            ActivateEvent?.Invoke(null);
        }
    }

    [SetUp]
    public void CreateContainerBuilder()
    {
        var builder = new ContainerBuilder();
        builder.Register<TestGameBoardController>(Lifetime.Scoped).As<IGameBoardController>().AsSelf();
        builder.Register<TestManagerController>(Lifetime.Scoped)
            .As<IImprovementController<ManagerPresenter>>()
            .AsSelf();

        builder.Register<TestUpgradeController>(Lifetime.Scoped)
            .As<IImprovementController<UpgradePresenter>>()
            .AsSelf();

        builder.Register<GameplayController>(Lifetime.Scoped);

        _container = builder.Build();
    }

    [Test]
    public void GameplayControllerInitialization()
    {
        var gameplayController = _container.Resolve<GameplayController>();
        gameplayController.Initialize();

        var gameBoardController = _container.Resolve<TestGameBoardController>();
        var managerController = _container.Resolve<TestManagerController>();
        var upgradeController = _container.Resolve<TestUpgradeController>();

        Assert.AreEqual(true, gameBoardController.IsInitialized);
        Assert.AreEqual(true, managerController.IsInitialized);
        Assert.AreEqual(true, upgradeController.IsInitialized);
    }

    [Test]
    public void GameplayControllerCellActivationCase()
    {
        var gameplayController = _container.Resolve<GameplayController>();
        gameplayController.Initialize();

        var gameBoardController = _container.Resolve<TestGameBoardController>();
        var managerController = _container.Resolve<TestManagerController>();
        var upgradeController = _container.Resolve<TestUpgradeController>();
        gameBoardController.InvokeActivateEvent(new GameCellModel() {});

        Assert.AreEqual(true, managerController.IsFound);
        Assert.AreEqual(true, upgradeController.IsFound);
    }

    [Test]
    public void GameplayControllerCellUnlockCase()
    {
        var gameplayController = _container.Resolve<GameplayController>();
        gameplayController.Initialize();

        var gameBoardController = _container.Resolve<TestGameBoardController>();
        gameBoardController.InvokeUnlockEvent();

        var managerController = _container.Resolve<TestManagerController>();
        var upgradeController = _container.Resolve<TestUpgradeController>();

        Assert.AreEqual(true, managerController.IsUnlocked);
        Assert.AreEqual(true, upgradeController.IsUnlocked);
    }

    [Test]
    public void GameplayControllerManagerActivationCase()
    {
        var gameplayController = _container.Resolve<GameplayController>();
        gameplayController.Initialize();

        var gameBoardController = _container.Resolve<TestGameBoardController>();
        var managerController = _container.Resolve<TestManagerController>();
        managerController.InvokeActivateEvent();

        Assert.AreEqual(true, gameBoardController.IsManagerInitialized);
    }

    [Test]
    public void GameplayControllerUpgradeActivationCase()
    {
        var gameplayController = _container.Resolve<GameplayController>();
        gameplayController.Initialize();

        var gameBoardController = _container.Resolve<TestGameBoardController>();
        var upgradeController = _container.Resolve<TestUpgradeController>();
        upgradeController.InvokeActivateEvent();

        Assert.AreEqual(true, gameBoardController.IsUpgradeInitialized);
    }
}
