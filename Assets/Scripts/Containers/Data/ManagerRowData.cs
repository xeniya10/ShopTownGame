using System.Collections.Generic;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "ManagerData", menuName = "ManagerData")]
public class ManagerRowData : ScriptableObject
{
    public List<string> ManagerNames = new List<string>()
    {
        "Flora",
        "Jackie",
        "Ruby",
        "Zoe",
        "Candice",
        "Ann",
        "Henry",
        "Maggie",
        "Kevin",
        "Mike",
        "Oliver",
        "Page",
        "Emily",
        "Sally",
        "Nicole",
        "Chloe",
        "Brian",
        "Carl",
        "Melissa",
        "Jesse",
        "Kevin",
        "Penny",
        "Mark",
        "Steve",
        "Barbara",
        "Diana",
        "Irene"
    };

    public List<MoneyModel> BaseCost = new List<MoneyModel>()
    {
        new MoneyModel(1000, Currency.Dollar),
        new MoneyModel(5000, Currency.Dollar),
        new MoneyModel(10000, Currency.Dollar),
        new MoneyModel(30000, Currency.Dollar),
        new MoneyModel(80000, Currency.Dollar),
        new MoneyModel(150000, Currency.Dollar),
        new MoneyModel(300000, Currency.Dollar),
        new MoneyModel(600000, Currency.Dollar),
        new MoneyModel(900000, Currency.Dollar),
        new MoneyModel(1500000, Currency.Dollar),
        new MoneyModel(5000000, Currency.Dollar),
        new MoneyModel(10000000, Currency.Dollar),
        new MoneyModel(15000000, Currency.Dollar),
        new MoneyModel(20000000, Currency.Dollar),
        new MoneyModel(3, Currency.Gold),
        new MoneyModel(5, Currency.Gold),
        new MoneyModel(10, Currency.Gold),
        new MoneyModel(20, Currency.Gold),
        new MoneyModel(40, Currency.Gold),
        new MoneyModel(80, Currency.Gold),
        new MoneyModel(120, Currency.Gold),
        new MoneyModel(200, Currency.Gold),
        new MoneyModel(250, Currency.Gold),
        new MoneyModel(300, Currency.Gold),
        new MoneyModel(400, Currency.Gold),
        new MoneyModel(600, Currency.Gold),
        new MoneyModel(1000, Currency.Gold)
    };
}
}
