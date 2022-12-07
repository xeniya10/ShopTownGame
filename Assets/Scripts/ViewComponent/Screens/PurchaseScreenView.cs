using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class PurchaseScreenView : MonoBehaviour, IPurchaseScreenView
{
    [SerializeField] private Button _hideButton;

    [Header("Transforms")]
    [SerializeField] private Transform _dollarPacks;
    [SerializeField] private Transform _goldPacks;

    [Header("Animation Duration")]
    [SerializeField] private float _moveTime;

    private Vector2 _startPosition;

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    private void Show()
    {
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        transform.MoveFromScreenBorder(0f, 1.5f, _moveTime);
    }

    private void Hide()
    {
        var sequence = DOTween.Sequence();
        transform.MoveToScreenBorder(0f, 1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            SetPosition(_startPosition);
        });
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

    public Transform GetDollarArea()
    {
        return _dollarPacks;
    }

    public Transform GetGoldArea()
    {
        return _goldPacks;
    }
}
}
