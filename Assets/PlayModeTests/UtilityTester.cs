using System;
using NUnit.Framework;
using ShopTown.ModelComponent;

public class UtilityTester
{
    // Testing time TimeSpanExtension

    [Test]
    public void SecondFormatTest()
    {
        var twoSecondsResult = new TimeSpan(0, 0, 2).ToFormattedString();
        var twoSecondsExpectedResult = "2.0";

        var tenSecondsResult = new TimeSpan(0, 0, 10).ToFormattedString();
        var tenSecondsExpectedResult = "10.0";

        Assert.AreEqual(twoSecondsExpectedResult, twoSecondsResult);
        Assert.AreEqual(tenSecondsExpectedResult, tenSecondsResult);
    }

    [Test]
    public void MinuteFormatTest()
    {
        var twoMinutesResult = new TimeSpan(0, 2, 2).ToFormattedString();
        var twoMinutesExpectedResult = "2:02";

        var tenMinutesResult = new TimeSpan(0, 10, 10).ToFormattedString();
        var tenMinutesExpectedResult = "10:10";

        Assert.AreEqual(twoMinutesExpectedResult, twoMinutesResult);
        Assert.AreEqual(tenMinutesExpectedResult, tenMinutesResult);
    }

    [Test]
    public void HourFormatTest()
    {
        var twoHoursResult = new TimeSpan(2, 2, 2).ToFormattedString();
        var twoHoursExpectedResult = "2:02:02";

        var tenHoursResult = new TimeSpan(10, 10, 10).ToFormattedString();
        var tenHoursExpectedResult = "10:10:10";

        Assert.AreEqual(twoHoursExpectedResult, twoHoursResult);
        Assert.AreEqual(tenHoursExpectedResult, tenHoursResult);
    }

    // Testing money formater

    [Test]
    public void UnitFormatTest()
    {
        var fiveUnitsResult = new MoneyModel(5, Currency.Dollar).ToFormattedString();
        var fiveUnitsExpectedResult = "5";

        var nineAndHalfResult = new MoneyModel(9.5, Currency.Dollar).ToFormattedString();
        var nineAndHalfExpectedResult = "9.5";

        Assert.AreEqual(fiveUnitsExpectedResult, fiveUnitsResult);
        Assert.AreEqual(nineAndHalfExpectedResult, nineAndHalfResult);
    }

    [Test]
    public void HundredsFormatTest()
    {
        var fiveHundredsResult = new MoneyModel(500, Currency.Dollar).ToFormattedString();
        var fiveHundredsExpectedResult = "500";

        var nineHundredsAndHalfResult = new MoneyModel(900.5, Currency.Dollar).ToFormattedString();
        var nineHundredsAndHalfExpectedResult = "900.5";

        Assert.AreEqual(fiveHundredsExpectedResult, fiveHundredsResult);
        Assert.AreEqual(nineHundredsAndHalfExpectedResult, nineHundredsAndHalfResult);
    }

    [Test]
    public void MillionsFormatTest()
    {
        var fiveMillionsResult = new MoneyModel(5000000, Currency.Dollar).ToFormattedString();
        var fiveMillionsExpectedResult = "5M";

        var nineMillionsAndHalfResult = new MoneyModel(9510000, Currency.Dollar).ToFormattedString();
        var nineMillionsAndHalfExpectedResult = "9.5M";

        Assert.AreEqual(fiveMillionsExpectedResult, fiveMillionsResult);
        Assert.AreEqual(nineMillionsAndHalfExpectedResult, nineMillionsAndHalfResult);
    }
}
