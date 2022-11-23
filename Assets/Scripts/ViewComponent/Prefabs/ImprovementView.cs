using System;
using DG.Tweening;
using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public interface IImprovementView
{
    IImprovementView Create(Transform parent);

    void Initialize(ImprovementModel model);

    void SetImprovementSprite(Sprite sprite);

    void SubscribeToBuyButton(Action callBack);

    void Hide();

    void Lock();

    void Unlock();

    void Activate();
}

public class ImprovementView : MonoBehaviour, IImprovementView
{
    public Button BuyButton;
    [SerializeField] protected ParticleSystem _salute;

    [Header("Currency Sprites")]
    [SerializeField] protected Sprite _dollarIcon;
    [SerializeField] protected Sprite _goldIcon;

    [Header("Images")]
    [SerializeField] protected Image _improvementImage;
    [SerializeField] protected Image _lockImage;
    [SerializeField] protected Image _currencyImage;

    [Header("Text Fields")]
    [SerializeField] protected TextMeshProUGUI _nameText;
    [SerializeField] protected TextMeshProUGUI _descriptionText;
    [SerializeField] protected TextMeshProUGUI _priceText;

    [Header("Animation Duration")]
    [SerializeField] protected float _fadeTime;

    public void SetImprovementSprite(Sprite sprite)
    {
        _improvementImage.sprite = sprite;
    }

    private void SetCost(MoneyModel cost)
    {
        _priceText.text = cost.ToFormattedString();
        if (cost.Value == Currency.Dollar)
        {
            _currencyImage.sprite = _dollarIcon;
            return;
        }

        _currencyImage.sprite = _goldIcon;
    }

    private void SetName(string managerName)
    {
        _nameText.text = managerName;
    }

    private void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    public IImprovementView Create(Transform parent)
    {
        return Instantiate(this, parent);
    }

    public void Initialize(ImprovementModel model)
    {
        SetName(model.Name);
        SetDescription(model.Description);
        SetCost(model.Cost);
    }

    public void SubscribeToBuyButton(Action callBack)
    {
        BuyButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Lock()
    {
        gameObject.SetActive(true);

        _lockImage.DOFade(0, _fadeTime)
            .OnComplete(() =>
            {
                _lockImage.gameObject.SetActive(true);
                _lockImage.DOFade(0.35f, _fadeTime);
            });
    }

    public void Unlock()
    {
        gameObject.SetActive(true);
        _lockImage.DOFade(0, _fadeTime).OnComplete(() => _lockImage.gameObject.SetActive(false));
    }

    public void Activate()
    {
        _salute.Play();
    }
}
}
