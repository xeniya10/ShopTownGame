using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PurchaseScreenView : MonoBehaviour
{
    public Button OkButton;

    [Header("Animation Durations")]

    [SerializeField]
    private float _moveTime;
    private Vector2 _showPosition;
    private Vector2 _hidePosition;

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
