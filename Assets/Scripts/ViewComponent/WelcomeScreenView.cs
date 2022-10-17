using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using ShopTown.ModelComponent;

public class WelcomeScreenView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button _okButton;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _goldText;

    [Header("Animation Durations")]
    [SerializeField] private float _moveTime;

    private Vector2 _startPosition;

    private void SetMoneyNumber(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            _moneyText.text = number.ToFormattedString();
            return;
        }

        _goldText.text = number.ToFormattedString();
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void ClickOkButton(Action callBack)
    {
        _okButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void Show(MoneyModel moneyAmount, MoneyModel goldAmount)
    {
        SetMoneyNumber(moneyAmount);
        SetMoneyNumber(goldAmount);
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        transform.MoveFromScreenBorder(0f, -1.5f, _moveTime, null);
    }

    public void Hide()
    {
        var sequence = DOTween.Sequence();
        transform.MoveToScreenBorder(0f, -1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            SetPosition(_startPosition);
        });
    }
}
