using System;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class UpgradeRowView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private Image _lockImage;

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

    private void SetMoneyPrice(double price)
    {
        _priceText.text = MoneyFormatUtility.MoneyDefault(price);
    }

    private void SetGoldPrice(double price)
    {
        _priceText.text = MoneyFormatUtility.GoldDefault(price);
    }

    private void SetName(string upgradeName)
    {
        _nameText.text = upgradeName;
    }

    private void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    public void ClickBuyButton(Action callBack)
    {
        _buyButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public UpgradeRowView Create(Transform parent)
    {
        var row = Instantiate(this, parent);
        return row;
    }

    public void Initialize(int upgradeLevel, int level, string upgradeName,
        string description, double moneyCost, double goldCost)
    {
        SetSprite(upgradeLevel, level);
        SetName(upgradeName);
        SetDescription(description);
        if (moneyCost == 0)
        {
            SetGoldPrice(goldCost);
            return;
        }

        SetMoneyPrice(moneyCost);
    }

    public void Lock()
    {
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, null, () =>
        {
            _lockImage.gameObject.SetActive(true);
            AnimationUtility.Fade(_lockImage, 1, _fadeTime, null, null);
        });
    }

    public void Unlock()
    {
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, null, () => _lockImage.gameObject.SetActive(false));
    }
}
}
