using DG.Tweening;
using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class WelcomeScreenView : MonoBehaviour
{
    [Header("Components")]
    public Button OkButton;
    [SerializeField] private TextMeshProUGUI _moneyText;

    [Header("Animation Durations")]
    [SerializeField] private float _moveTime;
    private Vector2 _startPosition;

    private void SetMoneyNumber(MoneyModel number)
    {
        _moneyText.text = number.ToFormattedString();
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void Initialize(MoneyModel moneyAmount, MoneyModel goldAmount)
    {
        SetMoneyNumber(moneyAmount);
        SetMoneyNumber(goldAmount);
    }

    public void Show()
    {
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        transform.MoveFromScreenBorder(0f, -1.5f, _moveTime);
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
}
