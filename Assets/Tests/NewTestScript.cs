using System.Collections.Generic;
using NUnit.Framework;
using ShopTown.ModelComponent;

public class NewTestScript
{
    private List<MoneyModel> _results = new List<MoneyModel>()
    {
        new MoneyModel(5, CurrencyType.Dollar),
        new MoneyModel(9.5, CurrencyType.Dollar),
        new MoneyModel(500, CurrencyType.Dollar),
        new MoneyModel(900.5, CurrencyType.Dollar),
        new MoneyModel(5000, CurrencyType.Dollar),
        new MoneyModel(9510, CurrencyType.Dollar),
        new MoneyModel(9590, CurrencyType.Dollar),
        new MoneyModel(5000000, CurrencyType.Dollar),
        new MoneyModel(9510000, CurrencyType.Dollar)
    };
    private List<string> _expectedResults = new List<string>()
    {
        "5",
        "9.5",
        "500",
        "900.5",
        "5K",
        "9.5K",
        "9.6K",
        "5M",
        "9.5M"
    };

    [Test] public void ToFormattedString_InputSeveralValues_ReturnsFormattedValues()
    {
        for (var i = 0; i < _results.Count; i++)
        {
            Assert.AreEqual(_expectedResults[i], _results[i].ToFormattedString());
        }
    }
}
