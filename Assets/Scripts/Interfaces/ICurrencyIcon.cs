using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
public interface ICurrencyIcon
{
    Sprite GetIcon(Currency currency);
}
}
