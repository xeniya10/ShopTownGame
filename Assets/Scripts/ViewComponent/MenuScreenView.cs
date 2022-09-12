using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class MenuScreenView : MonoBehaviour
{
    public Button HideButton;

    [Header("Setting Components")]
    public Button MusicSwitchButton;
    public Button SoundSwitchButton;
    public Button NotificationSwitchButton;
    public Button RemoveAdsButton;

    [Header("SocialNet Components")]
    public Button LikeButton;
    public Button InstagramButton;
    public Button FacebookButton;
    public Button TelegramButton;
    public Button TwitterButton;

    [Header("Button Text")]

    [SerializeField]
    private TextMeshProUGUI MusicButtonText;

    [SerializeField]
    private TextMeshProUGUI SoundButtonText;

    [SerializeField]
    private TextMeshProUGUI NotificationButtonText;

    [Header("Animation Durations")]

    [SerializeField]
    private float _moveTime;
    private Vector2 _showPosition;
    private Vector2 _hidePosition;

    private string _music = "Music: ";
    private string _sound = "Sound: ";
    private string _notification = "Notification: ";
    private string _on = "On";
    private string _off = "Off";

    public void SetMusicState(bool isMusicOn)
    {
        if (isMusicOn)
        {
            MusicButtonText.text = _music + _on;
        }

        else
        {
            MusicButtonText.text = _music + _off;
        }
    }

    public void SetSoundState(bool isSoundOn)
    {
        if (isSoundOn)
        {
            SoundButtonText.text = _sound + _on;
        }

        else
        {
            SoundButtonText.text = _sound + _off;
        }
    }

    public void SetNotificationState(bool isNotificationOn)
    {
        if (isNotificationOn)
        {
            NotificationButtonText.text = _notification + _on;
        }

        else
        {
            NotificationButtonText.text = _notification + _off;
        }
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    private void AppearAnimation()
    {
        _showPosition = transform.localPosition;

        var startX = _showPosition.x;
        var startY = _showPosition.y + Screen.height * 1.5f;
        _hidePosition = new Vector2(startX, startY);

        SetPosition(_hidePosition);
        var moveAnimation = transform.DOLocalMove(_showPosition, _moveTime);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        AppearAnimation();
    }

    private void DisappearAnimation(Action callBack)
    {
        var moveAnimation = transform.DOLocalMove(_hidePosition, _moveTime)
        .OnComplete(() => callBack?.Invoke());
    }

    public void Hide()
    {
        DisappearAnimation(() => gameObject.SetActive(false));
        SetPosition(_showPosition);
    }
}
