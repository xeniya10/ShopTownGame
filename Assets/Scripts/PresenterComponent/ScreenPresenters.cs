using System;
using System.Collections.Generic;
using DG.Tweening;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class SplashScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] private readonly ISplashCellView _cell;
    [Inject] private readonly ISplashScreenView _splashScreen;

    private readonly List<ISplashCellView> _cells = new List<ISplashCellView>();

    private Sequence _appearSequence;
    private Sequence _disappearSequence;

    public void Initialize()
    {
        Show();
        SubscribeToButton(_splashScreen.GetStartButton(), Hide);
    }

    private void Show()
    {
        _appearSequence = DOTween.Sequence();
        CreateBoard();
        _splashScreen.AppearAnimation(_appearSequence);
        _appearSequence.Play();
    }

    private void Hide()
    {
        _disappearSequence = DOTween.Sequence();
        _splashScreen.DisappearAnimation(_disappearSequence);
        _cells.ForEach(cell => cell.DisappearAnimation(_disappearSequence));
        _disappearSequence.Play();
    }

    private void CreateBoard()
    {
        var boardModel = new SplashBoardModel(7, 4);
        for (var i = 0; i < boardModel.Rows; i++)
        {
            for (var j = 0; j < boardModel.Columns; j++)
            {
                var size = boardModel.CalculateCellSize();
                var start = boardModel.CalculateStartPosition(j, i);
                var target = boardModel.CalculateTargetPosition(j, i);

                var view = _cell.Create(_splashScreen.GetSplashField());
                var cellIndex = j + i * boardModel.Columns;
                var model = new SplashCellModel(cellIndex, size, start, target);
                view.Initialize(model);
                view.AppearAnimation(_appearSequence);
                _cells.Add(view);
            }
        }
    }
}

public class GameScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IGameScreenView _gameScreen;
    [Inject] public readonly IMenuScreenView _menuScreen;
    [Inject] public readonly IPurchaseScreenView _purchaseScreen;

    public void Initialize()
    {
        _gameScreen.Initialize(_data.GameData);
        _data.GameData.ChangeEvent += () => _gameScreen.Initialize(_data.GameData);
        SubscribeToButton(_gameScreen.GetDollarAddButton(), () => _purchaseScreen.SetActive(true));
        SubscribeToButton(_gameScreen.GetGoldAddButton(), () => _purchaseScreen.SetActive(true));
        SubscribeToButton(_gameScreen.GetMenuButton(), () => _menuScreen.SetActive(true));
    }
}

public class MenuScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] private readonly IGameData _data;
    [Inject] public readonly IMenuScreenView _menu;

    public void Initialize()
    {
        _menu.Initialize(_data.Settings);
        SubscribeToButton(_menu.GetHideButton(), () => _menu.SetActive(false));

        SetSetting(_menu.GetMusicButton(), Settings.Music, _data.Settings.MusicOn);
        SetSetting(_menu.GetSoundButton(), Settings.Sound, _data.Settings.SoundOn);
        SetSetting(_menu.GetNotificationButton(), Settings.Notifications, _data.Settings.NotificationsOn);
        SetSetting(_menu.GetRemoveAdsButton(), Settings.Ads, _data.Settings.AdsOn);

        // SubscribeToButton(_menuScreenView.LikeButton, () => Application.OpenURL());
        SubscribeToButton(_menu.GetInstagramButton(), () => Application.OpenURL("https://www.instagram.com/"));
        SubscribeToButton(_menu.GetFacebookButton(), () => Application.OpenURL("https://www.facebook.com/"));
        SubscribeToButton(_menu.GetTelegramButton(), () => Application.OpenURL("https://telegram.org/"));
        SubscribeToButton(_menu.GetTwitterButton(), () => Application.OpenURL("https://twitter.com/"));
    }

    private void SetSetting(Button button, Settings setting, bool param)
    {
        SubscribeToButton(button, () =>
        {
            _data.Settings.ChangeState(setting);
            _menu.SetButtonText(setting, param);
        });
    }
}

public class PurchaseScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] public readonly IPurchaseScreenView _purchaseScreen;
    [Inject] private readonly IPackCellView _packCell;
    [Inject] private readonly PacksData _packsData;

    public void Initialize()
    {
        SubscribeToButton(_purchaseScreen.GetHideButton(), () => _purchaseScreen.SetActive(false));
        CreatePacks(_packsData.DollarPacks, _purchaseScreen.GetDollarArea());
        CreatePacks(_packsData.GoldPacks, _purchaseScreen.GetGoldArea());
    }

    private void CreatePacks(List<PackModel> packs, Transform parent)
    {
        foreach (var model in packs)
        {
            var dollarPack = _packCell.Create(parent);
            dollarPack.Initialize(model);
        }
    }
}

public class NewBusinessScreenPresenter : ButtonSubscription, IShowable<GameCellModel>, IInitializable
{
    [Inject] private readonly INewBusinessScreenView _view;

    public void Initialize()
    {
        SubscribeToButton(_view.GetHideButton(), () => _view.SetActive(false));
    }

    public void Show(GameCellModel model)
    {
        _view.Initialize(model);
        _view.SetActive(true);
    }
}

public class WelcomeScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IWelcomeScreenView _view;

    public void Initialize()
    {
        _view.Initialize(_data.GameData);
        SubscribeToButton(_view.GetHideButton(), () => _view.SetActive(false));
    }
}

public abstract class ButtonSubscription
{
    protected void SubscribeToButton(Button button, Action callBack)
    {
        button.onClick.AddListener(() => callBack?.Invoke());
    }
}

public interface IShowable<T>
{
    void Show(T model);
}
}
