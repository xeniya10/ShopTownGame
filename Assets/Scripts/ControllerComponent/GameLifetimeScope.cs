using ShopTown.ControllerComponent;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ControllerComponent
{
public class GameLifetimeScope : LifetimeScope
{
    [Header("Screens")]
    [SerializeField] private SplashScreenView _splashScreen;
    [SerializeField] private GameScreenView _gameScreen;
    [SerializeField] private MenuScreenView _menuScreen;
    [SerializeField] private PurchaseScreenView _purchaseScreen;
    [SerializeField] private NewBusinessScreenView _newBusinessScreen;
    [SerializeField] private WelcomeScreenView _welcomeScreen;

    [Header("Prefabs")]
    [SerializeField] private GameCellView _gameCellPrefab;
    [SerializeField] private SplashCellView _splashPrefab;
    [SerializeField] private ImprovementView _improvementPrefab;
    [SerializeField] private PackCellView _packCellPrefab;

    [Header("Data Containers")]
    [SerializeField] private DefaultDataConfiguration _defaultData;
    [SerializeField] private BusinessData _businessData;
    [SerializeField] private GameCellData _gameCellData;
    [SerializeField] private ImprovementData _improvementData;
    [SerializeField] private PacksData _packsData;

    [Header("Sprite Containers")]
    [SerializeField] private ImprovementCollection _improvementSprites;

    [Header("Audio")]
    [SerializeField] private AudioSourceView _audioSource;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterModel(builder);
        RegisterPresenter(builder);
        RegisterView(builder);
        RegisterController(builder);
    }

    private void RegisterController(IContainerBuilder builder)
    {
        builder.Register<DataController>(Lifetime.Scoped);
        builder.Register<GameBoardController>(Lifetime.Scoped);
        builder.Register<ImprovementController>(Lifetime.Scoped);
        builder.Register<ManagerController>(Lifetime.Scoped);
        builder.Register<UpgradeController>(Lifetime.Scoped);
        builder.RegisterEntryPoint<GameplayController>(Lifetime.Scoped);
    }

    private void RegisterModel(IContainerBuilder builder)
    {
        builder.Register<StorageManager>(Lifetime.Scoped);

        builder.Register<GameDataModel>(Lifetime.Scoped);
        builder.Register<GameSettingModel>(Lifetime.Scoped);

        builder.Register<SplashBoardModel>(Lifetime.Scoped);
        builder.Register<GameBoardModel>(Lifetime.Scoped);

        builder.Register<ImprovementModel>(Lifetime.Scoped);
        builder.Register<GameCellModel>(Lifetime.Scoped);
        // builder.Register<ManagerModel>(Lifetime.Scoped);
        // builder.Register<UpgradeModel>(Lifetime.Scoped);
    }

    private void RegisterPresenter(IContainerBuilder builder)
    {
        builder.Register<SplashScreenPresenter>(Lifetime.Scoped);
        builder.Register<GameScreenPresenter>(Lifetime.Scoped);

        builder.Register<GameCellPresenter>(Lifetime.Scoped);
        builder.Register<ImprovementPresenter>(Lifetime.Scoped);
        builder.Register<ManagerPresenter>(Lifetime.Scoped);
        builder.Register<UpgradePresenter>(Lifetime.Scoped);
    }

    private void RegisterView(IContainerBuilder builder)
    {
        builder.RegisterInstance(_splashScreen);
        builder.RegisterInstance(_gameScreen);
        builder.RegisterInstance(_menuScreen);
        builder.RegisterInstance(_purchaseScreen);
        builder.RegisterInstance(_newBusinessScreen);
        builder.RegisterInstance(_welcomeScreen);

        builder.RegisterInstance(_improvementPrefab).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_gameCellPrefab);
        builder.RegisterInstance(_splashPrefab);
        builder.RegisterInstance(_packCellPrefab);

        builder.RegisterInstance(_improvementSprites);

        builder.RegisterInstance(_defaultData);
        builder.RegisterInstance(_businessData);
        builder.RegisterInstance(_gameCellData);
        builder.RegisterInstance(_improvementData);
        builder.RegisterInstance(_packsData);

        builder.RegisterInstance(_audioSource);
    }
}
}
