using System.Collections.Generic;
using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class PackCellView : MonoBehaviour, ICellView<PackModel>
{
    [SerializeField] private Button _buyButton;

    [Header("Images")]
    [SerializeField] private List<Image> _packSizeImages;
    [SerializeField] private Image _currencyImage;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _profitText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Header("Currency Sprites")]
    [SerializeField] private CurrencyContainer _currency;

    public ICellView<PackModel> Instantiate(Transform parent)
    {
        return Instantiate(this, parent);
    }

    public void Initialize(PackModel model)
    {
        SetProfit(model.Profit);
        SetPrice(model.Price);
        SetPackSize(model.Size);
    }

    public Button GetBuyButton()
    {
        return _buyButton;
    }

    private void SetProfit(MoneyModel profit)
    {
        _profitText.text = profit.ToFormattedString();
        if (profit.Value == Currency.Dollar)
        {
            SetPackCurrency(_currency.DollarIcon);
            return;
        }

        SetPackCurrency(_currency.GoldIcon);
    }

    private void SetPrice(double price)
    {
        _priceText.text = price.ToFormattedString();
    }

    private void SetPackSize(int size)
    {
        for (var i = 0; i < size; i++)
        {
            _packSizeImages[i].gameObject.SetActive(true);
        }
    }

    private void SetPackCurrency(Sprite currencyIcon)
    {
        _currencyImage.sprite = currencyIcon;

        foreach (var image in _packSizeImages)
        {
            image.sprite = currencyIcon;
        }
    }
}
}
