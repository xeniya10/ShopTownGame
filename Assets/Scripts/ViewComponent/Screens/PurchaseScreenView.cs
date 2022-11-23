using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class PurchaseScreenView : MonoBehaviour
{
    public Button HideButton;

    [Header("Transforms")]
    public Transform DollarPacks;
    public Transform GoldPacks;

    [Header("Animation Duration")]
    [SerializeField] private float _moveTime;

    private Vector2 _startPosition;

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void Show()
    {
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        transform.MoveFromScreenBorder(0f, 1.5f, _moveTime);
    }

    public void Hide()
    {
        var sequence = DOTween.Sequence();
        transform.MoveToScreenBorder(0f, 1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            SetPosition(_startPosition);
        });
    }
}
}
