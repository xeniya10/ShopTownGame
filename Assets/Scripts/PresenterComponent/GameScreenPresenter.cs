using System.Collections.Generic;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;
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
    private readonly PackCellView _packCellView;
    private readonly WelcomeScreenView _welcomeScreenView;
    private readonly PacksData _packsData;

    private List<PackCellView> _dollarPacks = new List<PackCellView>();
    private List<PackCellView> _goldPacks = new List<PackCellView>();

    public GameScreenPresenter(GameScreenView gameScreenView, MenuScreenView menuScreenView, PurchaseScreenView purchaseScreenView,
        PackCellView packCellView, WelcomeScreenView welcomeScreenView, NewBusinessScreenView newBusinessScreenView,
        PacksData packsData, DataController dataController)
    {
        _gameScreenView = gameScreenView;
        _menuScreenView = menuScreenView;
        _purchaseScreenView = purchaseScreenView;
        _packCellView = packCellView;
        _welcomeScreenView = welcomeScreenView;
        _newBusinessScreenView = newBusinessScreenView;
        _packsData = packsData;
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
        _gameScreenView.SubscribeToAddButton(_purchaseScreenView.Show);
        _gameScreenView.SubscribeToMenuButton(() => _menuScreenView.Show(data.Settings));
    }

    private void InitializeMenuScreen()
    {
        var setting = _dataController.GameData.Settings;
        _menuScreenView.SubscribeToHideButton(_menuScreenView.Hide);
        _menuScreenView.SubscribeToMusicButton(() =>
        {
            setting.ChangeState(Settings.Music);
            _menuScreenView.ChangeButtonText(Settings.Music, setting.MusicOn);
        });

        _menuScreenView.SubscribeToSoundButton(() =>
        {
            setting.ChangeState(Settings.Sound);
            _menuScreenView.ChangeButtonText(Settings.Sound, setting.SoundOn);
        });

        _menuScreenView.SubscribeToNotificationButton(() =>
        {
            setting.ChangeState(Settings.Notifications);
            _menuScreenView.ChangeButtonText(Settings.Notifications, setting.NotificationsOn);
        });

        _menuScreenView.SubscribeToRemoveAdsButton(() =>
        {
            setting.ChangeState(Settings.Ads);
            _menuScreenView.ChangeButtonText(Settings.Ads, setting.AdsOn);
        });

        // _menuScreenView.SubscribeToLikeButton();
        _menuScreenView.SubscribeToInstagramButton(() => Application.OpenURL("https://www.instagram.com/"));
        _menuScreenView.SubscribeToFacebookButton(() => Application.OpenURL("https://www.facebook.com/"));
        _menuScreenView.SubscribeToTelegramButton(() => Application.OpenURL("https://telegram.org/"));
        _menuScreenView.SubscribeToTwitterButton(() => Application.OpenURL("https://twitter.com/"));
    }

    private void InitializePurchaseScreen()
    {
        _purchaseScreenView.SubscribeToOkButton(_purchaseScreenView.Hide);
        for (var i = 0; i < _packsData.DollarPacks.Count; i++)
        {
            var dollarPacks = _packsData.DollarPacks;
            var dollarPack = _packCellView.Create(_purchaseScreenView.DollarPacks);
            dollarPack.Initialize(dollarPacks[i].Profit, dollarPacks[i].Price, dollarPacks[i].Size);
            _dollarPacks.Add(dollarPack);

            var goldPacks = _packsData.GoldPacks;
            var goldPack = _packCellView.Create(_purchaseScreenView.GoldPacks);
            goldPack.Initialize(goldPacks[i].Profit, goldPacks[i].Price, goldPacks[i].Size);
            _goldPacks.Add(goldPack);
        }
    }

    private void InitializeWelcomeScreen()
    {
        var data = _dataController.GameData;
        _welcomeScreenView.Initialize(data.CurrentMoneyBalance, data.CurrentGoldBalance);
        _welcomeScreenView.SubscribeToOkButton(_welcomeScreenView.Hide);
    }

    private void InitializeNewBusinessScreen()
    {
        _newBusinessScreenView.SubscribeToOkButton(_newBusinessScreenView.Hide);
    }

    private void SetMoneyBalance()
    {
        var data = _dataController.GameData;
        _gameScreenView.SetMoneyNumber(data.CurrentMoneyBalance);
        _gameScreenView.SetMoneyNumber(data.CurrentGoldBalance);
    }

    public void ShowNewBusinessScreen(int level)
    {
        _newBusinessScreenView.Initialize(level);
        _newBusinessScreenView.Show();
    }
}
}
