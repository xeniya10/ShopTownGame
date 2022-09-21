using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackCellView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _priceText;

    public string GetPrice()
    {
        return _priceText.text;
    }

    public string GetMoney()
    {
        return _moneyText.text;
    }

    public void ClickPurchaseButton(Action callBack)
    {
        _purchaseButton.onClick.AddListener(() => callBack?.Invoke());
    }
}
