using System.Collections.Generic;
using NUnit.Framework;
using ShopTown.ModelComponent;

public class MoneyModelFormatTester
{
    private List<MoneyModel> _results = new List<MoneyModel>()
    {
        new MoneyModel(5, Currency.Dollar),
        new MoneyModel(9.5, Currency.Dollar),
        new MoneyModel(500, Currency.Dollar),
        new MoneyModel(900.5, Currency.Dollar),
        new MoneyModel(5000000, Currency.Dollar),
        new MoneyModel(9510000, Currency.Dollar)
    };
    private List<string> _expectedResults = new List<string>()
    {
        "5",
        "9.5",
        "500",
        "900.5",
        "5M",
        "9.5M"
    };

    [Test] public void MoneyFormatTest()
    {
        for (var i = 0; i < _results.Count; i++)
        {
            Assert.AreEqual(_expectedResults[i], _results[i].ToFormattedString());
        }
    }
}
