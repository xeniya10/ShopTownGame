using System;
using DG.Tweening;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class UpgradeRowView : MonoBehaviour
{
    public Button BuyButton;
    public ParticleSystem Salute;

    [Header("Currency Sprites")]
    [SerializeField] private Sprite _dollarIcon;
    [SerializeField] private Sprite _goldIcon;

    [Header("Images")]
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _currencyImage;

    [Space]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Space]
    [SerializeField] private UpgradeCollection _upgradeSprites;

    [Header("Animation Duration")]
    [SerializeField] private float _fadeTime;

    private void SetSprite(int upgradeLevel, int level)
    {
        if (upgradeLevel == 2)
        {
            _upgradeImage.sprite = _upgradeSprites.SecondLevelSprites[level - 1];
            return;
        }

        if (upgradeLevel == 3)
        {
            _upgradeImage.sprite = _upgradeSprites.ThirdLevelSprites[level - 1];
            return;
        }

        _upgradeImage.sprite = _upgradeSprites.FirstLevelSprites[level - 1];
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

    private void SetName(string upgradeName)
    {
        _nameText.text = upgradeName;
    }

    private void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    public UpgradeRowView Create(Transform parent)
    {
        var row = Instantiate(this, parent);
        return row;
    }

    public void Initialize(UpgradeRowModel model)
    {
        SetSprite(model.UpgradeLevel, model.Level);
        SetName(model.Name);
        SetDescription(model.Description);
        SetCost(model.Cost);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Lock()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        _lockImage.DOFade(0, _fadeTime)
            .OnComplete(() =>
            {
                _lockImage.gameObject.SetActive(true);
                _lockImage.Fade(0.35f, _fadeTime, null, null);
            });
    }

    public void Unlock()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        _lockImage.DOFade(0, _fadeTime).OnComplete(() => _lockImage.gameObject.SetActive(false));
    }
}
}
