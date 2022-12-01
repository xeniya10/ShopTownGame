using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class SplashScreenView : MonoBehaviour, ISplashScreenView
{
    [Header("Components")]
    [SerializeField] private Transform _splashField;
    [SerializeField] private Button _startButton;
    [SerializeField] private Image _splashScreenImage;
    [SerializeField] private TextMeshProUGUI _startButtonText;
    [SerializeField] private TextMeshProUGUI _gameNameText;

    [Header("Animation Durations")]
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _moveTime;

    public void AppearAnimation(Sequence sequence)
    {
        MoveAnimation(_gameNameText.transform, sequence);
        MoveAnimation(_startButton.transform, sequence);
    }

    public void DisappearAnimation(Sequence sequence)
    {
        FadeAnimation(_startButtonText, sequence);
        FadeAnimation(_gameNameText, sequence);
        FadeAnimation(_splashScreenImage, sequence, () => gameObject.SetActive(false));
    }

    public Transform GetSplashField()
    {
        return _splashField;
    }

    public Button GetStartButton()
    {
        return _startButton;
    }

    private void MoveAnimation(Transform uiElement, Sequence sequence)
    {
        uiElement.MoveFromScreenBorder(-1.5f, 0f, _moveTime, sequence);
    }

    private void FadeAnimation(Graphic uiElement, Sequence sequence, Action callBack = null)
    {
        uiElement.Fade(0, _fadeTime, sequence, () => callBack?.Invoke());
    }
}

public interface ISplashScreenView : ISplashField, IStartButton, ISplashAnimation
{}

public interface IStartButton
{
    Button GetStartButton();
}

public interface ISplashField
{
    Transform GetSplashField();
}
}
