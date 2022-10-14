using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class GameScreenPresenter : IInitializable
{
    private readonly DataController _dataController;
    private readonly GameScreenView _gameScreenView;
    private readonly MenuScreenView _menuScreenView;
    private readonly NewBusinessScreenView _newBusinessScreenView;
    private readonly PurchaseScreenView _purchaseScreenView;
    private readonly WelcomeScreenView _welcomeScreenView;

    public GameScreenPresenter(GameScreenView gameScreenView, MenuScreenView menuScreenView, PurchaseScreenView purchaseScreenView,
        WelcomeScreenView welcomeScreenView, NewBusinessScreenView newBusinessScreenView, DataController dataController)
    {
        _gameScreenView = gameScreenView;
        _menuScreenView = menuScreenView;
        _purchaseScreenView = purchaseScreenView;
        _welcomeScreenView = welcomeScreenView;
        _newBusinessScreenView = newBusinessScreenView;
        _dataController = dataController;
    }

    public void Initialize()
    {
        InitializeTopBar();
        InitializeMenuScreen();
        InitializePurchaseScreen();
        InitializeWelcomeScreen();
        InitializeNewBusinessScreen();
    }

    private void InitializeTopBar()
    {
        var data = _dataController.GameData;

        SetMoneyBalance();
        data.BalanceChangeEvent += SetMoneyBalance;

        _gameScreenView.SetGoldNumber(data.CurrentGoldBalance);

        _gameScreenView.ClickAddButton(_purchaseScreenView.Show);
        _gameScreenView.ClickMenuButton(_menuScreenView.Show);
    }

    private void InitializeMenuScreen()
    {
        var data = _dataController.GameData;

        _menuScreenView.ClickHideButton(_menuScreenView.Hide);

        _menuScreenView.ClickMusicButton(() => _menuScreenView.SetMusicState(data.Settings.Music));
        _menuScreenView.ClickSoundButton(() => _menuScreenView.SetSoundState(data.Settings.Sound));
        _menuScreenView.ClickNotificationButton(() => _menuScreenView.SetNotificationState(data.Settings.Notifications));
        // _menuScreenView.ClickRemoveAdsButton(() => _menuScreenView. (data.Settings.Ads));

        // _menuScreenView.ClickLikeButton();
        // _menuScreenView.ClickInstagramButton();
        // _menuScreenView.ClickFacebookButton();
        // _menuScreenView.ClickTelegramButton();
        // _menuScreenView.ClickTwitterButton();
    }

    private void InitializePurchaseScreen()
    {
        _purchaseScreenView.ClickOkButton(_purchaseScreenView.Hide);
    }

    private void InitializeWelcomeScreen()
    {
        var data = _dataController.GameData;

        // _welcomeScreenView.SetMoneyNumber();
        // _welcomeScreenView.SetGoldNumber();
        _welcomeScreenView.ClickOkButton(_welcomeScreenView.Hide);
    }

    private void InitializeNewBusinessScreen()
    {
        _newBusinessScreenView.ClickOkButton(_newBusinessScreenView.Hide);
    }

    private void SetMoneyBalance()
    {
        var data = _dataController.GameData;
        _gameScreenView.SetMoneyNumber(data.CurrentMoneyBalance);
    }

    public void ShowNewBusinessScreen(int level)
    {
        _newBusinessScreenView.Initialize(level);
        _newBusinessScreenView.Show();
    }
}
}
