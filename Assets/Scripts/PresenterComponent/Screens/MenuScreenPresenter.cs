using ShopTown.ControllerComponent;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class MenuScreenPresenter : IInitializable
{
    [Inject] private readonly IButtonSubscriber _subscriber;
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IMenuScreenView _menu;

    public void Initialize()
    {
        _menu.Initialize(_data.GameData.Settings);
        _subscriber.AddListenerToButton(_menu.GetHideButton(), () => _menu.SetActive(false));

        SetSetting(_menu.GetMusicButton(), Setting.Music);
        SetSetting(_menu.GetSoundButton(), Setting.Sound);
        SetSetting(_menu.GetNotificationButton(), Setting.Notifications);
        SetSetting(_menu.GetRemoveAdsButton(), Setting.Ads);

        // SubscribeToButton(_menuScreenView.LikeButton, () => Application.OpenURL());
        _subscriber.AddListenerToButton(_menu.GetInstagramButton(),
            () => Application.OpenURL("https://www.instagram.com/"));

        _subscriber.AddListenerToButton(_menu.GetFacebookButton(),
            () => Application.OpenURL("https://www.facebook.com/"));

        _subscriber.AddListenerToButton(_menu.GetTelegramButton(), () => Application.OpenURL("https://telegram.org/"));
        _subscriber.AddListenerToButton(_menu.GetTwitterButton(), () => Application.OpenURL("https://twitter.com/"));
    }

    private void SetSetting(Button button, Setting setting)
    {
        _subscriber.AddListenerToButton(button, () =>
        {
            _data.GameData.Settings.ChangeState(setting, out var param);
            _menu.SetButtonText(setting, param);
        });
    }
}
}
