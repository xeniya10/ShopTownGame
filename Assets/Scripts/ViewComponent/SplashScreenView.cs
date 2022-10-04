using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class SplashScreenView : MonoBehaviour
{
    [Header("Components")]
    public Transform CellField;
    [SerializeField] private Image _splashScreenImage;
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _startButtonText;
    [SerializeField] private TextMeshProUGUI _gameNameText;

    [Header("Animation Durations")]
    [SerializeField] private float _scaleTime;
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _moveTime;

    private Sequence _appearSequence;
    private Sequence _disappearSequence;

    public void ClickStartButton(Action callBack)
    {
        _startButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void InitializeSequences()
    {
        _appearSequence = DOTween.Sequence();
        _disappearSequence = DOTween.Sequence();
    }

    public void PlayAppearSequence()
    {
        _appearSequence.Play();
    }

    public void PlayDisappearSequence()
    {
        _disappearSequence.Play();
    }

    public void AppearCell(SplashCellView cell)
    {
        AnimationUtility.Move(cell.transform, cell.TargetPosition, _moveTime * 0.5f, _appearSequence);
    }

    public void AppearTextFields()
    {
        AnimationUtility.MoveFromScreenBorder(_gameNameText.transform, -1.5f, 0f, _moveTime, _appearSequence);
        AnimationUtility.MoveFromScreenBorder(_startButton.transform, -1.5f, 0f, _moveTime, _appearSequence);
    }

    public void DisappearTextFields()
    {
        AnimationUtility.Fade(_startButtonText, 0, _fadeTime, _disappearSequence, null);
        AnimationUtility.Fade(_gameNameText, 0, _fadeTime, _disappearSequence, null);
        AnimationUtility.Fade(_splashScreenImage, 0, _fadeTime, _disappearSequence, () => gameObject.SetActive(false));
    }

    public void DisappearCells(SplashCellView cell)
    {
        var scale = new Vector2(0, 0);
        AnimationUtility.Scale(cell.transform, scale, _scaleTime, _disappearSequence, null);
    }
}
}
