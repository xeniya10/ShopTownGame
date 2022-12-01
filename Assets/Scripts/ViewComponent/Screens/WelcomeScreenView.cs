using DG.Tweening;
using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class WelcomeScreenView : MonoBehaviour, IWelcomeScreenView
{
    [Header("Components")]
    [SerializeField] private Button _hideButton;
    [SerializeField] private TextMeshProUGUI _moneyText;

    [Header("Animation Durations")]
    [SerializeField] private float _moveTime;

    private Vector2 _startPosition;

    public void Initialize(GameDataModel data)
    {
        SetMoneyNumber(data.CurrentDollarBalance);
        SetMoneyNumber(data.CurrentGoldBalance);
    }

    public void SetActive(bool isActivated)
    {
        if (isActivated)
        {
            Show();
            return;
        }

        Hide();
    }

    public Button GetHideButton()
    {
        return _hideButton;
    }

    private void SetMoneyNumber(MoneyModel number)
    {
        _moneyText.text = number.ToFormattedString();
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    private void Show()
    {
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        transform.MoveFromScreenBorder(0f, -1.5f, _moveTime);
    }

    private void Hide()
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

public interface IWelcomeScreenView : IInitializable<GameDataModel>, IActivatableScreen, IHideButton
{}

public interface IHideButton
{
    Button GetHideButton();
}

public interface IActivatableScreen
{
    void SetActive(bool isActivated);
}
}
