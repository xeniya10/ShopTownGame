using DG.Tweening;
using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class MenuScreenView : MonoBehaviour, IMenuScreenView
{
    [SerializeField] private Button _hideButton;

    [Header("Setting Components")]
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _notificationButton;
    [SerializeField] private Button _removeAdsButton;

    [Header("SocialNet Components")]
    [SerializeField] private Button _likeButton;
    [SerializeField] private Button _instagramButton;
    [SerializeField] private Button _facebookButton;
    [SerializeField] private Button _telegramButton;
    [SerializeField] private Button _twitterButton;

    [Header("Button Text")]
    [SerializeField] private TextMeshProUGUI _musicButtonText;
    [SerializeField] private TextMeshProUGUI _soundButtonText;
    [SerializeField] private TextMeshProUGUI _notificationButtonText;

    [Header("Animation Durations")]
    [SerializeField] private float _moveTime;

    [Header("Button Names")]
    public string Music = "Music: ";
    public string Sound = "Sound: ";
    public string Notification = "Notification: ";
    public string OnState = "On";
    public string OffState = "Off";

    private Vector2 _startPosition;

    public void Initialize(SettingModel settings)
    {
        SetButtonText(_musicButtonText, Music, settings.MusicOn);
        SetButtonText(_soundButtonText, Sound, settings.SoundOn);
        SetButtonText(_notificationButtonText, Notification, settings.NotificationsOn);
    }

    public void SetActive(bool isActivated)
    {
        if (isActivated)
        {
            Show();
            return;
        }

        Hide();
    }

    public void SetButtonText(Setting parameter, bool state)
    {
        switch (parameter)
        {
            case Setting.Music:
                SetButtonText(_musicButtonText, Music, state);
                break;

            case Setting.Sound:
                SetButtonText(_soundButtonText, Sound, state);
                break;

            case Setting.Notifications:
                SetButtonText(_notificationButtonText, Notification, state);
                break;

            case Setting.Ads:
                // ChangeAdsState();
                break;
        }
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    private void SetButtonText(TextMeshProUGUI buttonText, string name, bool isOn)
    {
        if (isOn)
        {
            buttonText.text = name + OnState;
            return;
        }

        buttonText.text = name + OffState;
    }

    private void Show()
    {
        _startPosition = transform.localPosition;
        // Initialize(settings);
        gameObject.SetActive(true);
        transform.MoveFromScreenBorder(0f, 1.5f, _moveTime);
    }

    private void Hide()
    {
        var sequence = DOTween.Sequence();
        transform.MoveToScreenBorder(0f, 1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            SetPosition(_startPosition);
        });
    }

    public Button GetMusicButton()
    {
        return _musicButton;
    }

    public Button GetSoundButton()
    {
        return _soundButton;
    }

    public Button GetNotificationButton()
    {
        return _notificationButton;
    }

    public Button GetRemoveAdsButton()
    {
        return _removeAdsButton;
    }

    public Button GetLikeButton()
    {
        return _likeButton;
    }

    public Button GetInstagramButton()
    {
        return _instagramButton;
    }

    public Button GetFacebookButton()
    {
        return _facebookButton;
    }

    public Button GetTelegramButton()
    {
        return _telegramButton;
    }

    public Button GetTwitterButton()
    {
        return _twitterButton;
    }

    public Button GetHideButton()
    {
        return _hideButton;
    }
}
}
