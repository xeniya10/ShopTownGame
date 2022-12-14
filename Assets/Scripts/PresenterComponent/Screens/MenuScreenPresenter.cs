using ShopTown.ControllerComponent;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class MenuScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] private readonly IGameData _data;
    [Inject] public readonly IMenuScreenView _menu;

    public void Initialize()
    {
        _menu.Initialize(_data.GameData.Settings);
        SubscribeToButton(_menu.GetHideButton(), () => _menu.SetActive(false));

        SetSetting(_menu.GetMusicButton(), Setting.Music);
        SetSetting(_menu.GetSoundButton(), Setting.Sound);
        SetSetting(_menu.GetNotificationButton(), Setting.Notifications);
        SetSetting(_menu.GetRemoveAdsButton(), Setting.Ads);

        // SubscribeToButton(_menuScreenView.LikeButton, () => Application.OpenURL());
        SubscribeToButton(_menu.GetInstagramButton(), () => Application.OpenURL("https://www.instagram.com/"));
        SubscribeToButton(_menu.GetFacebookButton(), () => Application.OpenURL("https://www.facebook.com/"));
        SubscribeToButton(_menu.GetTelegramButton(), () => Application.OpenURL("https://telegram.org/"));
        SubscribeToButton(_menu.GetTwitterButton(), () => Application.OpenURL("https://twitter.com/"));
    }

    private void SetSetting(Button button, Setting setting)
    {
        SubscribeToButton(button, () =>
        {
            _data.GameData.Settings.ChangeState(setting, out var param);
            _menu.SetButtonText(setting, param);
        });
    }
}
}
