using System;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "GameCellData", menuName = "GameCellData")]
public class GameCellData : ScriptableObject
{
    public MoneyModel[] BaseProfit =
    {
        new MoneyModel(1, Currency.Dollar),
        new MoneyModel(5, Currency.Dollar),
        new MoneyModel(15, Currency.Dollar),
        new MoneyModel(30, Currency.Dollar),
        new MoneyModel(80, Currency.Dollar),
        new MoneyModel(150, Currency.Dollar),
        new MoneyModel(300, Currency.Dollar),
        new MoneyModel(700, Currency.Dollar),
        new MoneyModel(2000, Currency.Dollar),
        new MoneyModel(4000, Currency.Dollar),
        new MoneyModel(10000, Currency.Dollar),
        new MoneyModel(20000, Currency.Dollar),
        new MoneyModel(50000, Currency.Dollar),
        new MoneyModel(50000, Currency.Dollar),
        new MoneyModel(150000, Currency.Dollar),
        new MoneyModel(500000, Currency.Dollar),
        new MoneyModel(1000000, Currency.Dollar),
        new MoneyModel(2000000, Currency.Dollar),
        new MoneyModel(5000000, Currency.Dollar),
        new MoneyModel(10000000, Currency.Dollar),
        new MoneyModel(20000000, Currency.Dollar),
        new MoneyModel(40000000, Currency.Dollar),
        new MoneyModel(90000000, Currency.Dollar),
        new MoneyModel(150000000, Currency.Dollar),
        new MoneyModel(300000000, Currency.Dollar),
        new MoneyModel(500000000, Currency.Dollar),
        new MoneyModel(1000000000, Currency.Dollar),
        new MoneyModel(1500000000, Currency.Dollar)
    };

    public TimeSpan[] ProcessTime =
    {
        new TimeSpan(0, 0, 2),
        new TimeSpan(0, 0, 5),
        new TimeSpan(0, 0, 10),
        new TimeSpan(0, 0, 20),
        new TimeSpan(0, 0, 40),
        new TimeSpan(0, 1, 0),
        new TimeSpan(0, 2, 0),
        new TimeSpan(0, 3, 0),
        new TimeSpan(0, 5, 0),
        new TimeSpan(0, 8, 0),
        new TimeSpan(0, 10, 0),
        new TimeSpan(0, 15, 0),
        new TimeSpan(0, 20, 0),
        new TimeSpan(0, 40, 0),
        new TimeSpan(1, 0, 0),
        new TimeSpan(2, 0, 0),
        new TimeSpan(4, 0, 0),
        new TimeSpan(6, 0, 0),
        new TimeSpan(8, 0, 0),
        new TimeSpan(10, 0, 0),
        new TimeSpan(14, 0, 0),
        new TimeSpan(18, 0, 0),
        new TimeSpan(24, 0, 0),
        new TimeSpan(30, 0, 0),
        new TimeSpan(36, 0, 0),
        new TimeSpan(42, 0, 0),
        new TimeSpan(48, 0, 0)
    };

    public MoneyModel[] Cost =
    {
        new MoneyModel(2, Currency.Dollar),
        new MoneyModel(20, Currency.Dollar),
        new MoneyModel(50, Currency.Dollar),
        new MoneyModel(100, Currency.Dollar),
        new MoneyModel(500, Currency.Dollar),
        new MoneyModel(1500, Currency.Dollar),
        new MoneyModel(3000, Currency.Dollar),
        new MoneyModel(8000, Currency.Dollar),
        new MoneyModel(15000, Currency.Dollar),
        new MoneyModel(40000, Currency.Dollar),
        new MoneyModel(100000, Currency.Dollar),
        new MoneyModel(300000, Currency.Dollar),
        new MoneyModel(1000000, Currency.Dollar),
        new MoneyModel(3000000, Currency.Dollar),
        new MoneyModel(10000000, Currency.Dollar),
        new MoneyModel(20000000, Currency.Dollar),
        new MoneyModel(50000000, Currency.Dollar),
        new MoneyModel(100000000, Currency.Dollar),
        new MoneyModel(200000000, Currency.Dollar),
        new MoneyModel(500000000, Currency.Dollar),
        new MoneyModel(1000000000, Currency.Dollar)
    };
}
}
