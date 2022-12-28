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
        // sequence.OnComplete(() => gameObject.SetActive(false));
        FadeAnimation(_splashScreenImage, sequence, 0.3f);
        FadeAnimation(_startButtonText, sequence);
        FadeAnimation(_gameNameText, sequence);
    }

    public void DisappearAnimationImage(Sequence sequence)
    {
        FadeAnimation(_splashScreenImage, sequence, 0f, () => gameObject.SetActive(false));
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

    private void FadeAnimation(Graphic uiElement, Sequence sequence, float alpha = 0f, Action callBack = null)
    {
        uiElement.Fade(alpha, _fadeTime, sequence, () => callBack?.Invoke());
    }
}
}
