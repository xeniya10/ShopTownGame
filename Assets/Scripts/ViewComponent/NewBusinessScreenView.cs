using System;
using DG.Tweening;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class NewBusinessScreenView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image _businessImage;
    [SerializeField] private Button _okButton;
    [SerializeField] private TextMeshProUGUI _businessNameText;
    [SerializeField] private ParticleSystem _confettiParticleSystem;
    [SerializeField] private BusinessCollection _businessCollection;

    [Header("Animation Durations")]
    [SerializeField] private float _moveTime;

    private Vector2 _startPosition;

    private void SetSprite(int level)
    {
        _businessImage.sprite = _businessCollection.Sprites[level - 1];
    }

    private void SetNameText(string businessName)
    {
        _businessNameText.text = businessName;
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void ClickOkButton(Action callBack)
    {
        _okButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void Show(int level, string businessName)
    {
        var sequence = DOTween.Sequence();
        SetSprite(level);
        SetNameText(businessName);
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        AnimationUtility.MoveFromScreenBorder(transform, 0f, -1.5f, _moveTime, sequence);
        sequence.OnComplete(() => _confettiParticleSystem.Play());
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
}
