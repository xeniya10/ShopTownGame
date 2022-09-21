using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [Header("Screens")]
    [SerializeField] private StartScreenView _startScreen;
    [SerializeField] private GameScreenView _gameScreen;
    [SerializeField] private MenuScreenView _menuScreen;
    [SerializeField] private PurchaseScreenView _purchaseScreen;
    [SerializeField] private NewBusinessScreenView _newBusinessScreen;
    [SerializeField] private WelcomeScreenView _welcomeScreen;

    [Header("Prefabs")]
    [SerializeField] private GameCellView _gameCellPrefab;
    [SerializeField] private StartImageCellView _startImagePrefab;
    [SerializeField] private ManagerRowView _managerRowPrefab;
    [SerializeField] private UpgradeRowView _upgradeRowPrefab;
    [SerializeField] private PackCellView _packCellPrefab;

    // [Header("ScriptableObjects")]
    // [SerializeField] private GameSettings _gameSettings;

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
        builder.Register<GameController>(Lifetime.Scoped);
        builder.Register<DataController>(Lifetime.Scoped);

        builder.RegisterEntryPoint<GameplayController>(Lifetime.Scoped);
    }

    private void RegisterModel(IContainerBuilder builder)
    {
        builder.Register<GameDataModel>(Lifetime.Scoped);
        builder.Register<GameSettingModel>(Lifetime.Scoped);

        builder.Register<StartScreenModel>(Lifetime.Scoped);
        builder.Register<GameBoardModel>(Lifetime.Scoped);

        builder.Register<GameCellModel>(Lifetime.Scoped);
        builder.Register<ManagerRowModel>(Lifetime.Scoped);
        builder.Register<UpgradeRowModel>(Lifetime.Scoped);
    }

    private void RegisterPresenter(IContainerBuilder builder)
    {
        builder.Register<StartScreenPresenter>(Lifetime.Scoped);
        // builder.RegisterEntryPoint<StartScreenPresenter>(Lifetime.Scoped);
    }

    private void RegisterView(IContainerBuilder builder)
    {
        builder.RegisterInstance(_startScreen);
        builder.RegisterInstance(_gameScreen);
        builder.RegisterInstance(_menuScreen);
        builder.RegisterInstance(_purchaseScreen);
        builder.RegisterInstance(_newBusinessScreen);
        builder.RegisterInstance(_welcomeScreen);

        builder.RegisterInstance(_gameCellPrefab);
        builder.RegisterInstance(_startImagePrefab);
        builder.RegisterInstance(_managerRowPrefab);
        builder.RegisterInstance(_upgradeRowPrefab);

        // builder.RegisterInstance(_gameSettings);
    }
}
