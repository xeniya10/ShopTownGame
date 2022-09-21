using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class WelcomeScreenView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button _okButton;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _goldText;

    [Header("Animation Durations")]
    [SerializeField] private float _moveTime;
    private Vector2 _startPosition;

    private void SetMoneyNumber(float moneyAmount)
    {
        _moneyText.text = moneyAmount.ToString();
    }

    private void SetGoldNumber(float goldAmount)
    {
        _goldText.text = goldAmount.ToString();
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void ClickOkButton(Action callBack)
    {
        _okButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void Show(float moneyAmount, float goldAmount)
    {
        SetMoneyNumber(moneyAmount);
        SetGoldNumber(goldAmount);
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        AnimationUtility.MoveFromScreenBorder(transform, 0f, -1.5f, _moveTime, null);
    }

    public void Hide()
    {
        var sequence = DOTween.Sequence();
        AnimationUtility.MoveToScreenBorder(transform, 0f, -1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            SetPosition(_startPosition);
        });
    }
}