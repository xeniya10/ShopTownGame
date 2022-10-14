using System;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class UpgradeRowView : MonoBehaviour
{
    [Header("Components")]
    public Button BuyButton;
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

    private void SetCost(MoneyModel cost)
    {
        _priceText.text = MoneyFormatUtility.Default(cost);
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
        BuyButton.onClick.AddListener(() => callBack?.Invoke());
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

    public void Lock()
    {
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, null, () =>
        {
            _lockImage.gameObject.SetActive(true);
            AnimationUtility.Fade(_lockImage, 0.35f, _fadeTime, null, null);
        });
    }

    public void Unlock()
    {
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, null, () => _lockImage.gameObject.SetActive(false));
    }
}
}
