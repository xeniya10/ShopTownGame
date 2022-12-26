using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShopTown.ControllerComponent
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
    [SerializeField] private GameData _defaultData;
    [SerializeField] private BusinessData _businessData;
    [SerializeField] private BoardData _boardData;
    [SerializeField] private ImprovementData _improvementData;
    [SerializeField] private PacksData _packsData;

    [Header("Sprite Containers")]
    [SerializeField] private ImprovementContainer _improvementSprites;

    [Header("Audio")]
    [SerializeField] private AudioSourceView _audioSource;

    [Header("Ads")]
    [SerializeField] private AdsView _adsView;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterView(builder);
        RegisterPresenter(builder);
        RegisterController(builder);
    }

    private void RegisterController(IContainerBuilder builder)
    {
        builder.Register<StorageManager>(Lifetime.Scoped).As<IStorageManager>();
        builder.Register<DataManager>(Lifetime.Scoped).AsImplementedInterfaces();

        builder.Register<GameBoardController>(Lifetime.Scoped).AsImplementedInterfaces();
        builder.Register<ManagerController>(Lifetime.Scoped).As<IImprovementController<ManagerPresenter>>();
        builder.Register<UpgradeController>(Lifetime.Scoped).As<IImprovementController<UpgradePresenter>>();

        builder.RegisterEntryPoint<GameplayController>(Lifetime.Scoped);
    }

    private void RegisterPresenter(IContainerBuilder builder)
    {
        builder.Register<AudioPresenter>(Lifetime.Scoped).As<IPlayable>();
        builder.Register<ButtonSubscriber>(Lifetime.Scoped).As<IButtonSubscriber>();

        builder.Register<WelcomeScreenPresenter>(Lifetime.Scoped).As<IShowable<IGameData>>();
        builder.Register<NewBusinessScreenPresenter>(Lifetime.Scoped).As<IShowable<GameCellModel>>();

        builder.Register<GameCellFactory>(Lifetime.Scoped).As<IPresenterFactory<IGameCell>>();
        builder.Register<ManagerFactory>(Lifetime.Scoped).As<IPresenterFactory<IManager>>();
        builder.Register<UpgradeFactory>(Lifetime.Scoped).As<IPresenterFactory<IUpgrade>>();

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<AudioPresenter>();
            entryPoints.Add<SplashScreenPresenter>();
            entryPoints.Add<GameScreenPresenter>();
            entryPoints.Add<MenuScreenPresenter>();
            entryPoints.Add<PurchaseScreenPresenter>();
            entryPoints.Add<NewBusinessScreenPresenter>();
            entryPoints.Add<WelcomeScreenPresenter>();
        });
    }

    private void RegisterView(IContainerBuilder builder)
    {
        builder.RegisterInstance(_splashScreen).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_gameScreen).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_menuScreen).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_purchaseScreen).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_newBusinessScreen).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_welcomeScreen).AsImplementedInterfaces().AsSelf();

        builder.RegisterInstance(_improvementPrefab).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_gameCellPrefab).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_splashPrefab).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_packCellPrefab).AsImplementedInterfaces().AsSelf();

        builder.RegisterInstance(_improvementSprites).AsImplementedInterfaces().AsSelf();

        builder.RegisterInstance(_defaultData).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_businessData).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_boardData).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_improvementData).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(_packsData).AsImplementedInterfaces().AsSelf();

        builder.RegisterInstance(_audioSource).AsImplementedInterfaces().AsSelf();

        builder.RegisterInstance(_adsView).AsImplementedInterfaces().AsSelf();
    }
}
}
