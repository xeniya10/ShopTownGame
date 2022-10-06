using System;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class ManagerRowView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button _hireButton;
    [SerializeField] private Image _managerImage;
    [SerializeField] private Image _lockImage;

    [Space]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Header("Collections")]
    [SerializeField] private ManagerCollection _managerCollection;

    [Header("Animation Duration")]
    [SerializeField] private float _fadeTime;

    private void SetSprite(int level)
    {
        _managerImage.sprite = _managerCollection.AvatarSprites[level - 1];
    }

    private void SetCost(MoneyModel cost)
    {
        if (cost.Value == Currency.Gold)
        {
            _priceText.text = MoneyFormatUtility.GoldDefault(cost.Number);
            return;
        }

        _priceText.text = MoneyFormatUtility.MoneyDefault(cost.Number);
    }

    public void SetMoneyPrice(double price)
    {}

    public void SetGoldPrice(double price)
    {}

    private void SetName(string managerName)
    {
        _nameText.text = managerName;
    }

    private void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    public void ClickHireButton(Action callBack)
    {
        _hireButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public ManagerRowView Create(Transform parent)
    {
        var row = Instantiate(this, parent);
        return row;
    }

    public void Initialize(ManagerRowModel model)
    {
        SetSprite(model.Level);
        SetName(model.Name);
        SetDescription(model.Description);
        SetCost(model.Cost);
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
