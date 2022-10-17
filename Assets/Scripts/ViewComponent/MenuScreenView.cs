using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

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

    public void SetMusicState(bool isMusicOn)
    {
        if (isMusicOn)
        {
            _musicButtonText.text = _music + _on;
        }

        else
        {
            _musicButtonText.text = _music + _off;
        }
    }

    public void SetSoundState(bool isSoundOn)
    {
        if (isSoundOn)
        {
            _soundButtonText.text = _sound + _on;
        }

        else
        {
            _soundButtonText.text = _sound + _off;
        }
    }

    public void SetNotificationState(bool isNotificationOn)
    {
        if (isNotificationOn)
        {
            _notificationButtonText.text = _notification + _on;
        }

        else
        {
            _notificationButtonText.text = _notification + _off;
        }
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void ClickHideButton(Action callBack)
    {
        _hideButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickMusicButton(Action callBack)
    {
        _musicSwitchButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickSoundButton(Action callBack)
    {
        _soundSwitchButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickNotificationButton(Action callBack)
    {
        _notificationSwitchButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickRemoveAdsButton(Action callBack)
    {
        _removeAdsButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickLikeButton(Action callBack)
    {
        _likeButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickInstagramButton(Action callBack)
    {
        _instagramButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickFacebookButton(Action callBack)
    {
        _facebookButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickTelegramButton(Action callBack)
    {
        _telegramButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickTwitterButton(Action callBack)
    {
        _twitterButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void Show()
    {
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        transform.MoveFromScreenBorder(0f, 1.5f, _moveTime, null);
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
