using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private UpgradeSpriteCollection _upgradeSprites;

    [Header("Animation Duration")]
    [SerializeField] private float _fadeTime;

    public void SetFirstUpgradeSprite(int level)
    {
        _upgradeImage.sprite = _upgradeSprites.FirstLevelSprites[level - 1];
    }

    public void SetSecondUpgradeSprite(int level)
    {
        _upgradeImage.sprite = _upgradeSprites.SecondLevelSprites[level - 1];
    }

    public void SetThirdUpgradeSprite(int level)
    {
        _upgradeImage.sprite = _upgradeSprites.ThirdLevelSprites[level - 1];
    }

    public void SetMoneyPrice(double price)
    {
        _priceText.text = MoneyFormatUtility.MoneyDefault(price);
    }

    public void SetGoldPrice(double price)
    {
        _priceText.text = MoneyFormatUtility.GoldDefault(price);
    }

    public void SetName(string name)
    {
        _nameText.text = name;
    }

    public void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    public void ClickBuyButton(Action callBack)
    {
        _buyButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public UpgradeRowView Create(Transform parent, int upgradeLevel, int level, string name, string description)
    {
        var row = Instantiate(this, parent);

        switch (upgradeLevel)
        {
            case 1:
                row.SetFirstUpgradeSprite(level);
                break;

            case 2:
                row.SetSecondUpgradeSprite(level);
                break;

            case 3:
                row.SetThirdUpgradeSprite(level);
                break;
        }

        row.SetName(name);
        row.SetDescription(description);

        return row;
    }

    public void Lock()
    {
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, null,
        () =>
        {
            _lockImage.gameObject.SetActive(true);
            AnimationUtility.Fade(_lockImage, 1, _fadeTime, null, null);
        });
    }

    public void Unlock()
    {
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, null,
        () => _lockImage.gameObject.SetActive(false));
    }
}