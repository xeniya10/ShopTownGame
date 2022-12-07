using ShopTown.ModelComponent;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public interface IMenuScreenView : IInitializable<SettingModel>, IActivatableScreen, ISettingButton, ISocialNetButton,
    IHideButton
{
    void SetButtonText(Setting parameter, bool state);
}

public interface ISettingButton
{
    Button GetMusicButton();

    Button GetSoundButton();

    Button GetNotificationButton();

    Button GetRemoveAdsButton();
}

public interface ISocialNetButton
{
    Button GetLikeButton();

    Button GetInstagramButton();

    Button GetFacebookButton();

    Button GetTelegramButton();

    Button GetTwitterButton();
}
}
