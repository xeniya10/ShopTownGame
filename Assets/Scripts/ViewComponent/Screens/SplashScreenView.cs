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
    public Button StartButton;
    [SerializeField] private Image _splashScreenImage;
    [SerializeField] private TextMeshProUGUI _startButtonText;
    [SerializeField] private TextMeshProUGUI _gameNameText;

    [Header("Animation Durations")]
    [SerializeField] private float _scaleTime;
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _moveTime;

    [NonSerialized] public Sequence AppearSequence;
    [NonSerialized] public Sequence DisappearSequence;

    public void InitializeSequences()
    {
        AppearSequence = DOTween.Sequence();
        DisappearSequence = DOTween.Sequence();
    }

    public void AppearCell(SplashCellView cell)
    {
        cell.transform.Move(cell.TargetPosition, _moveTime * 0.5f, AppearSequence);
    }

    public void AppearTextFields()
    {
        _gameNameText.transform.MoveFromScreenBorder(-1.5f, 0f, _moveTime, AppearSequence);
        StartButton.transform.MoveFromScreenBorder(-1.5f, 0f, _moveTime, AppearSequence);
    }

    public void DisappearTextFields()
    {
        _startButtonText.Fade(0, _fadeTime, DisappearSequence);
        _gameNameText.Fade(0, _fadeTime, DisappearSequence);
        _splashScreenImage.Fade(0, _fadeTime, DisappearSequence, () => gameObject.SetActive(false));
    }

    public void DisappearCells(SplashCellView cell)
    {
        var scale = new Vector2(0, 0);
        cell.transform.Scale(scale, _scaleTime, DisappearSequence);
    }
}
}
