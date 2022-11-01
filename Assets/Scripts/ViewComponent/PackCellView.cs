using System;
using System.Collections.Generic;
using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class PackCellView : MonoBehaviour
{
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private List<GameObject> _packSizeObjects;

    [Header("Currency Sprites")]
    [SerializeField] private Sprite _dollarIcon;
    [SerializeField] private Sprite _goldIcon;

    [Header("Images")]
    [SerializeField] private List<Image> _packSizeImages;
    [SerializeField] private Image _currencyImage;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _profitText;
    [SerializeField] private TextMeshProUGUI _priceText;

    private void SetProfit(MoneyModel profit)
    {
        _profitText.text = profit.ToFormattedString();
        if (profit.Value == Currency.Dollar)
        {
            SetPackCurrency(_dollarIcon);
            return;
        }

        SetPackCurrency(_goldIcon);
    }

    private void SetPrice(double price)
    {
        _priceText.text = price.ToFormattedString();
    }

    private void SetPackSize(int size)
    {
        for (var i = 0; i < size; i++)
        {
            _packSizeObjects[i].SetActive(true);
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

    public PackCellView Create(Transform parent)
    {
        return Instantiate(this, parent);
    }

    public void Initialize(MoneyModel profit, double price, int size)
    {
        SetProfit(profit);
        SetPrice(price);
        SetPackSize(size);
    }

    public void SubscribeToPurchaseButton(Action callBack)
    {
        _purchaseButton.onClick.AddListener(() => callBack?.Invoke());
    }
}
}
