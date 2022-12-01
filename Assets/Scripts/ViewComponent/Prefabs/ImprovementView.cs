using DG.Tweening;
using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class ImprovementView : MonoBehaviour, IImprovementView
{
    [SerializeField] protected Button _buyButton;
    [SerializeField] protected ParticleSystem _salute;

    [Header("Images")]
    [SerializeField] protected Image _improvementImage;
    [SerializeField] protected Image _lockImage;
    [SerializeField] protected Image _currencyImage;

    [Header("Text Fields")]
    [SerializeField] protected TextMeshProUGUI _nameText;
    [SerializeField] protected TextMeshProUGUI _descriptionText;
    [SerializeField] protected TextMeshProUGUI _priceText;

    [Header("Currency Sprites")]
    [SerializeField] private CurrencyContainer _currency;

    [Header("Animation Duration")]
    [SerializeField] protected float _fadeTime;

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

    public void StartAnimation(ImprovementState state)
    {
        switch (state)
        {
            case ImprovementState.Hide:
                HideAnimation();
                break;

            case ImprovementState.Lock:
                LockAnimation();
                break;

            case ImprovementState.Unlock:
                UnlockAnimation();
                break;
        }
    }

    public void ActivateAnimation()
    {
        _salute.Play();
    }

    public void SetImprovementSprite(Sprite sprite)
    {
        _improvementImage.sprite = sprite;
    }

    public Button GetBuyButton()
    {
        return _buyButton;
    }

    private void SetCost(MoneyModel cost)
    {
        _priceText.text = cost.ToFormattedString();
        if (cost.Value == Currency.Dollar)
        {
            _currencyImage.sprite = _currency.DollarIcon;
            return;
        }

        _currencyImage.sprite = _currency.GoldIcon;
    }

    private void SetName(string managerName)
    {
        _nameText.text = managerName;
    }

    private void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    private void HideAnimation()
    {
        gameObject.SetActive(false);
    }

    private void LockAnimation()
    {
        gameObject.SetActive(true);

        _lockImage.DOFade(0, _fadeTime)
            .OnComplete(() =>
            {
                _lockImage.gameObject.SetActive(true);
                _lockImage.DOFade(0.35f, _fadeTime);
            });
    }

    private void UnlockAnimation()
    {
        gameObject.SetActive(true);
        _lockImage.DOFade(0, _fadeTime).OnComplete(() => _lockImage.gameObject.SetActive(false));
    }
}

public interface IImprovementView : ICreatable<IImprovementView>, IInitializable<ImprovementModel>, IBuyButton
{
    void SetImprovementSprite(Sprite sprite);

    void StartAnimation(ImprovementState state);

    void ActivateAnimation();
}
}
