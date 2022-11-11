using System;
using System.Collections.Generic;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.ModelComponent
{
[Serializable]
public class PackModel
{
    public MoneyModel Profit;
    public double Price;
    public int Size;

    public PackModel(MoneyModel profit, double price, int size)
    {
        Profit = profit;
        Price = price;
        Size = size;
    }
}
}

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "PacksData", menuName = "PacksData")]
public class PacksData : ScriptableObject
{
    public List<PackModel> DollarPacks = new List<PackModel>()
    {
        new PackModel(new MoneyModel(10000, Currency.Dollar), 2.99, 1),
        new PackModel(new MoneyModel(100000, Currency.Dollar), 14.99, 2),
        new PackModel(new MoneyModel(500000, Currency.Dollar), 49.99, 3)
    };

    public List<PackModel> GoldPacks = new List<PackModel>()
    {
        new PackModel(new MoneyModel(5, Currency.Gold), 4.99, 1),
        new PackModel(new MoneyModel(50, Currency.Gold), 29.99, 2),
        new PackModel(new MoneyModel(100, Currency.Gold), 49.99, 3)
    };
}
}
