using System;
using DG.Tweening;
using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class MenuScreenView : MonoBehaviour
{
    [SerializeField] private Button _hideButton;

    [Header("Setting Components")]
    [SerializeField] private Button _musicSwitchButton;
    [SerializeField] private Button _soundSwitchButton;
    [SerializeField] private Button _notificationSwitchButton;
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

    public void SubscribeToHideButton(Action callBack)
    {
        _hideButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SubscribeToMusicButton(Action callBack)
    {
        _musicSwitchButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SubscribeToSoundButton(Action callBack)
    {
        _soundSwitchButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SubscribeToNotificationButton(Action callBack)
    {
        _notificationSwitchButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SubscribeToRemoveAdsButton(Action callBack)
    {
        _removeAdsButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SubscribeToLikeButton(Action callBack)
    {
        _likeButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SubscribeToInstagramButton(Action callBack)
    {
        _instagramButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SubscribeToFacebookButton(Action callBack)
    {
        _facebookButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SubscribeToTelegramButton(Action callBack)
    {
        _telegramButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SubscribeToTwitterButton(Action callBack)
    {
        _twitterButton.onClick.AddListener(() => callBack?.Invoke());
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

    private void Initialize(GameSettingModel settings)
    {
        SetButtonText(_musicButtonText, _music, settings.MusicOn);
        SetButtonText(_soundButtonText, _sound, settings.SoundOn);
        SetButtonText(_notificationButtonText, _notification, settings.NotificationsOn);
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
