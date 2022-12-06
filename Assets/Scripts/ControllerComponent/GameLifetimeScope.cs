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
    [SerializeField] private ImprovementContainer _improvementSprites;

    [Header("Audio")]
    [SerializeField] private AudioSourceView _audioSource;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterView(builder);
        builder.Register<DataManager>(Lifetime.Scoped).AsImplementedInterfaces();
        RegisterPresenter(builder);
        RegisterController(builder);
    }

    private void RegisterController(IContainerBuilder builder)
    {
        builder.Register<GameBoardController>(Lifetime.Scoped);
        builder.Register<ManagerController>(Lifetime.Scoped);
        builder.Register<UpgradeController>(Lifetime.Scoped);
        builder.RegisterEntryPoint<GameplayController>(Lifetime.Scoped);
    }

    private void RegisterPresenter(IContainerBuilder builder)
    {
        builder.Register<NewBusinessScreenPresenter>(Lifetime.Scoped).As<IShowable<GameCellModel>>();
        builder.Register<WelcomeScreenPresenter>(Lifetime.Scoped).As<IInitializable<MoneyModel>>();
        builder.Register<AudioSourcePresenter>(Lifetime.Scoped).As<IPlayable>().AsSelf();

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<SplashScreenPresenter>();
            entryPoints.Add<GameScreenPresenter>();
            entryPoints.Add<MenuScreenPresenter>();
            entryPoints.Add<PurchaseScreenPresenter>();
            entryPoints.Add<NewBusinessScreenPresenter>();
            // entryPoints.Add<WelcomeScreenPresenter>();
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

        builder.RegisterInstance(_improvementSprites);

        builder.RegisterInstance(_defaultData);
        builder.RegisterInstance(_businessData);
        builder.RegisterInstance(_gameCellData);
        builder.RegisterInstance(_improvementData);
        builder.RegisterInstance(_packsData);

        builder.RegisterInstance(_audioSource).As<IAudioSource>().AsSelf();
    }
}
}
