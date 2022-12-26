using System;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "CurrencySprites", menuName = "CurrencySprites")]
public class CurrencyContainer : ScriptableObject, ICurrencyIcon
{
    [SerializeField] private Sprite _dollarIcon;
    [SerializeField] private Sprite _goldIcon;

    public Sprite GetIcon(CurrencyType currencyType)
    {
        if (_dollarIcon == null || _goldIcon == null)
        {
            throw new NullReferenceException($"{GetType().Name}.{nameof(GetIcon)}: Currency Icon is null");
        }

        if (currencyType == CurrencyType.Dollar)
        {
            return _dollarIcon;
        }

        return _goldIcon;
    }
}
}
