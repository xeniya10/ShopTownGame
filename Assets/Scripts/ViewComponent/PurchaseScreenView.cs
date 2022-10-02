using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PurchaseScreenView : MonoBehaviour
{
    [SerializeField] private Button _hideButton;
    [SerializeField] private float _moveTime;
    private Vector2 _startPosition;

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void ClickOkButton(Action callBack)
    {
        _hideButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void Show()
    {
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        AnimationUtility.MoveFromScreenBorder(transform, 0f, 1.5f, _moveTime, null);
    }

    public void Hide()
    {
        var sequence = DOTween.Sequence();
        AnimationUtility.MoveToScreenBorder(transform, 0f, 1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            SetPosition(_startPosition);
        });
    }
}