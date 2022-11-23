using DG.Tweening;
using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class MenuScreenView : MonoBehaviour
{
    public Button HideButton;

    [Header("Setting Components")]
    public Button MusicButton;
    public Button SoundButton;
    public Button NotificationButton;
    public Button RemoveAdsButton;

    [Header("SocialNet Components")]
    public Button LikeButton;
    public Button InstagramButton;
    public Button FacebookButton;
    public Button TelegramButton;
    public Button TwitterButton;

    [Header("Button Text")]
    [SerializeField] private TextMeshProUGUI _musicButtonText;
    [SerializeField] private TextMeshProUGUI _soundButtonText;
    [SerializeField] private TextMeshProUGUI _notificationButtonText;

    [Header("Animation Durations")]
    [SerializeField] private float _moveTime;
    private Vector2 _startPosition;

    private string _music = "Music: ";
    private string _sound = "Sound: ";
    private string _notification = "Notification: ";
    private string _on = "On";
    private string _off = "Off";

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    private void SetButtonText(TextMeshProUGUI buttonText, string name, bool isOn)
    {
        if (isOn)
        {
            buttonText.text = name + _on;
            return;
        }

        buttonText.text = name + _off;
    }

    private void Initialize(GameSettingModel settings)
    {
        SetButtonText(_musicButtonText, _music, settings.MusicOn);
        SetButtonText(_soundButtonText, _sound, settings.SoundOn);
        SetButtonText(_notificationButtonText, _notification, settings.NotificationsOn);
    }

    public void ChangeButtonText(Settings parameter, bool state)
    {
        switch (parameter)
        {
            case Settings.Music:
                SetButtonText(_musicButtonText, _music, state);
                break;

            case Settings.Sound:
                SetButtonText(_soundButtonText, _sound, state);
                break;

            case Settings.Notifications:
                SetButtonText(_notificationButtonText, _notification, state);
                break;

            case Settings.Ads:
                // ChangeAdsState();
                break;
        }
    }

    public void Show(GameSettingModel settings)
    {
        _startPosition = transform.localPosition;
        Initialize(settings);
        gameObject.SetActive(true);
        transform.MoveFromScreenBorder(0f, 1.5f, _moveTime);
    }

    public void Hide()
    {
        var sequence = DOTween.Sequence();
        transform.MoveToScreenBorder(0f, 1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            SetPosition(_startPosition);
        });
    }
}
}
