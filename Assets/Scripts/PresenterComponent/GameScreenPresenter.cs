using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ShopTown.PresenterComponent
{
public class GameScreenPresenter
{
    private DataController _data;
    [Inject] private readonly GameScreenView _gameScreenView;
    [Inject] private readonly MenuScreenView _menuScreenView;
    [Inject] private readonly NewBusinessScreenView _newBusinessScreenView;
    [Inject] private readonly PurchaseScreenView _purchaseScreenView;
    [Inject] private readonly PackCellView _packCellView;
    [Inject] private readonly WelcomeScreenView _welcomeScreenView;
    [Inject] private readonly PacksData _packsData;

    public void Initialize(ref DataController data)
    {
        _data = data;
        InitializeTopBar();
        InitializeMenuScreen();
        InitializePurchaseScreen();
        InitializeWelcomeScreen();
        InitializeNewBusinessScreen();
    }

    private void InitializeTopBar()
    {
        SetMoneyBalance();
        _data.GameData.ChangeEvent += SetMoneyBalance;
        SubscribeToButton(_gameScreenView.DollarAddButton, _purchaseScreenView.Show);
        SubscribeToButton(_gameScreenView.GoldAddButton, _purchaseScreenView.Show);
        SubscribeToButton(_gameScreenView.MenuButton, () => _menuScreenView.Show(_data.Settings));
    }

    private void InitializeMenuScreen()
    {
        SubscribeToButton(_menuScreenView.HideButton, _menuScreenView.Hide);

        SetSetting(_menuScreenView.MusicButton, Settings.Music, _data.Settings.MusicOn);
        SetSetting(_menuScreenView.SoundButton, Settings.Sound, _data.Settings.SoundOn);
        SetSetting(_menuScreenView.NotificationButton, Settings.Notifications, _data.Settings.NotificationsOn);
        SetSetting(_menuScreenView.RemoveAdsButton, Settings.Ads, _data.Settings.AdsOn);

        // SubscribeToButton(_menuScreenView.LikeButton, () => Application.OpenURL());
        SubscribeToButton(_menuScreenView.InstagramButton, () => Application.OpenURL("https://www.instagram.com/"));
        SubscribeToButton(_menuScreenView.FacebookButton, () => Application.OpenURL("https://www.facebook.com/"));
        SubscribeToButton(_menuScreenView.TelegramButton, () => Application.OpenURL("https://telegram.org/"));
        SubscribeToButton(_menuScreenView.TwitterButton, () => Application.OpenURL("https://twitter.com/"));
    }

    private void SetSetting(Button button, Settings setting, bool param)
    {
        SubscribeToButton(button, () =>
        {
            _data.Settings.ChangeState(setting);
            _menuScreenView.ChangeButtonText(setting, param);
        });
    }

    private void SubscribeToButton(Button button, Action callBack)
    {
        button.onClick.AddListener(() => callBack?.Invoke());
    }

    private void InitializePurchaseScreen()
    {
        SubscribeToButton(_purchaseScreenView.HideButton, _purchaseScreenView.Hide);

        for (var i = 0; i < _packsData.DollarPacks.Count; i++)
        {
            var dollarPacks = _packsData.DollarPacks;
            var dollarPack = _packCellView.Create(_purchaseScreenView.DollarPacks);
            dollarPack.Initialize(dollarPacks[i].Profit, dollarPacks[i].Price, dollarPacks[i].Size);

            var goldPacks = _packsData.GoldPacks;
            var goldPack = _packCellView.Create(_purchaseScreenView.GoldPacks);
            goldPack.Initialize(goldPacks[i].Profit, goldPacks[i].Price, goldPacks[i].Size);
        }
    }

    private void InitializeWelcomeScreen()
    {
        _welcomeScreenView.Initialize(_data.GameData.CurrentDollarBalance, _data.GameData.CurrentGoldBalance);
        SubscribeToButton(_welcomeScreenView.OkButton, _welcomeScreenView.Hide);
    }

    private void InitializeNewBusinessScreen()
    {
        SubscribeToButton(_newBusinessScreenView.OkButton, _newBusinessScreenView.Hide);
    }

    private void SetMoneyBalance()
    {
        _gameScreenView.SetMoneyNumber(_data.GameData.CurrentDollarBalance);
        _gameScreenView.SetMoneyNumber(_data.GameData.CurrentGoldBalance);
    }

    public void ShowNewBusinessScreen(int level)
    {
        _newBusinessScreenView.Initialize(level);
        _newBusinessScreenView.Show();
    }
}
}
