using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;
using System;

public class StartScreenView : MonoBehaviour
{
    [Header("Components")]
    public Transform CellField;
    [SerializeField] private Image _startScreenImage;
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

    public void AppearAnimation(List<StartImageCellView> list)
    {
        _appearSequence = DOTween.Sequence();

        AppearCells(list);
        AnimationUtility.MoveFromScreenBorder(_gameNameText.transform, -1.5f, 0f, _moveTime, _appearSequence);
        AnimationUtility.MoveFromScreenBorder(_startButton.transform, -1.5f, 0f, _moveTime, _appearSequence);
    }

    private void AppearCells(List<StartImageCellView> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            AnimationUtility.Move(list[i].transform, list[i].TargetPosition, _moveTime * 0.5f, _appearSequence);
        }
    }

    public void DisappearAnimation(List<StartImageCellView> list)
    {
        _disappearSequence = DOTween.Sequence();

        AnimationUtility.Fade(_startButtonText, 0, _fadeTime, _disappearSequence, null);
        AnimationUtility.Fade(_gameNameText, 0, _fadeTime, _disappearSequence, null);
        DisappearCells(list);
        AnimationUtility.Fade(_startScreenImage, 0, _fadeTime, _disappearSequence,
        () => gameObject.SetActive(false));

        _disappearSequence.Play();
    }

    private void DisappearCells(List<StartImageCellView> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var scale = new Vector2(0, 0);
            AnimationUtility.Scale(list[i].transform, scale, _scaleTime, _disappearSequence, null);
        }
    }
}