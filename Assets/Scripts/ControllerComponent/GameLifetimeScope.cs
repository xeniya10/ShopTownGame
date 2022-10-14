using ShopTown.ControllerComponent;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace ControllerComponent
{
public class GameLifetimeScope : LifetimeScope
{
    [FormerlySerializedAs("_startScreen")]
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
    [SerializeField] private ManagerRowView _managerRowPrefab;
    [SerializeField] private UpgradeRowView _upgradeRowPrefab;
    [SerializeField] private PackCellView _packCellPrefab;

    [Header("DataContainers")]
    [SerializeField] private BusinessData _businessData;
    [SerializeField] private GameCellData _gameCellData;
    [SerializeField] private ManagerRowData _managerRowData;
    [SerializeField] private UpgradeRowData _upgradeRowData;

    [Header("Audio")]
    [SerializeField] private AudioSourceView _audioSource;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterController(builder);
        RegisterModel(builder);
        RegisterPresenter(builder);
        RegisterView(builder);
    }

    private void RegisterController(IContainerBuilder builder)
    {
        builder.Register<DataController>(Lifetime.Scoped);
        builder.RegisterEntryPoint<GameplayController>(Lifetime.Scoped);
    }

    private void RegisterModel(IContainerBuilder builder)
    {
        builder.Register<GameDataModel>(Lifetime.Scoped);
        builder.Register<GameSettingModel>(Lifetime.Scoped);

        builder.Register<SplashScreenModel>(Lifetime.Scoped);
        builder.Register<GameBoardModel>(Lifetime.Scoped);

        builder.Register<GameCellModel>(Lifetime.Scoped);
        builder.Register<ManagerRowModel>(Lifetime.Scoped);
        builder.Register<UpgradeRowModel>(Lifetime.Scoped);
    }

    private void RegisterPresenter(IContainerBuilder builder)
    {
        builder.Register<SplashScreenPresenter>(Lifetime.Scoped);
        builder.Register<GameScreenPresenter>(Lifetime.Scoped);

        builder.Register<GameCellPresenter>(Lifetime.Scoped);
        builder.Register<ManagerRowPresenter>(Lifetime.Scoped);
        builder.Register<UpgradeRowPresenter>(Lifetime.Scoped);
    }

    private void RegisterView(IContainerBuilder builder)
    {
        builder.RegisterInstance(_splashScreen);
        builder.RegisterInstance(_gameScreen);
        builder.RegisterInstance(_menuScreen);
        builder.RegisterInstance(_purchaseScreen);
        builder.RegisterInstance(_newBusinessScreen);
        builder.RegisterInstance(_welcomeScreen);

        builder.RegisterInstance(_gameCellPrefab);
        builder.RegisterInstance(_splashPrefab);
        builder.RegisterInstance(_managerRowPrefab);
        builder.RegisterInstance(_upgradeRowPrefab);

        builder.RegisterInstance(_businessData);
        builder.RegisterInstance(_gameCellData);
        builder.RegisterInstance(_managerRowData);
        builder.RegisterInstance(_upgradeRowData);

        builder.RegisterInstance(_audioSource);
    }
}
}
