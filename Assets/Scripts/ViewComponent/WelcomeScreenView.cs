using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class WelcomeScreenView : MonoBehaviour
{
    [Header("Components")]
    public Button OkButton;
    public TextMeshProUGUI AmountMoneyText;
    public TextMeshProUGUI AmountGoldText;

    [Header("Animation Durations")]

    [SerializeField]
    private float _moveTime;
    private Vector2 _showPosition;
    private Vector2 _hidePosition;

    private void SetMoneyNumber(float moneyAmount)
    {
        AmountMoneyText.text = moneyAmount.ToString();
    }

    private void SetGoldNumber(float goldAmount)
    {
        AmountGoldText.text = goldAmount.ToString();
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    private void AppearAnimation()
    {
        _showPosition = transform.localPosition;

        var startX = _showPosition.x;
        var startY = _showPosition.y - Screen.height * 1.5f;
        _hidePosition = new Vector2(startX, startY);

        SetPosition(_hidePosition);
        var moveAnimation = transform.DOLocalMove(_showPosition, _moveTime);
    }

    public void Show(float moneyAmount, float goldAmount)
    {
        SetMoneyNumber(moneyAmount);
        SetGoldNumber(goldAmount);
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