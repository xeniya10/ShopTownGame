using System;
using NUnit.Framework;
using ShopTown.ModelComponent;

public class UtilityTester
{
    // Testing time TimeSpanExtension

    [Test]
    public void SecondFormatTest()
    {
        var twoSecondsNumberResult = new TimeSpan(0, 0, 2).ToNumberFormatString();
        var twoSecondsNumberExpectedResult = "2.0";

        var tenSecondsNumberResult = new TimeSpan(0, 0, 10).ToNumberFormatString();
        var tenSecondsNumberExpectedResult = "10.0";

        Assert.AreEqual(twoSecondsNumberExpectedResult, twoSecondsNumberResult);
        Assert.AreEqual(tenSecondsNumberExpectedResult, tenSecondsNumberResult);

        var twoSecondsNameResult = new TimeSpan(0, 0, 2).ToNameFormatString();
        var twoSecondsNameExpectedResult = "2 s";

        var tenSecondsNameResult = new TimeSpan(0, 0, 10).ToNameFormatString();
        var tenSecondsNameExpectedResult = "10 s";

        Assert.AreEqual(twoSecondsNameExpectedResult, twoSecondsNameResult);
        Assert.AreEqual(tenSecondsNameExpectedResult, tenSecondsNameResult);
    }

    [Test]
    public void MinuteFormatTest()
    {
        var twoMinutesNumberResult = new TimeSpan(0, 2, 2).ToNumberFormatString();
        var twoMinutesNumberExpectedResult = "2:02.0";

        var tenMinutesNumberResult = new TimeSpan(0, 10, 10).ToNumberFormatString();
        var tenMinutesNumberExpectedResult = "10:10.0";

        Assert.AreEqual(twoMinutesNumberExpectedResult, twoMinutesNumberResult);
        Assert.AreEqual(tenMinutesNumberExpectedResult, tenMinutesNumberResult);

        var twoMinutesNameResult = new TimeSpan(0, 2, 0).ToNameFormatString();
        var twoMinutesNameExpectedResult = "2 m";

        var tenMinutesNameResult = new TimeSpan(0, 10, 0).ToNameFormatString();
        var tenMinutesNameExpectedResult = "10 m";

        Assert.AreEqual(twoMinutesNameExpectedResult, twoMinutesNameResult);
        Assert.AreEqual(tenMinutesNameExpectedResult, tenMinutesNameResult);
    }

    [Test]
    public void HourFormatTest()
    {
        var twoHoursNumberResult = new TimeSpan(2, 2, 2).ToNumberFormatString();
        var twoHoursNumberExpectedResult = "2:02";

        var tenHoursNumberResult = new TimeSpan(10, 10, 10).ToNumberFormatString();
        var tenHoursNumberExpectedResult = "10:10";

        Assert.AreEqual(twoHoursNumberExpectedResult, twoHoursNumberResult);
        Assert.AreEqual(tenHoursNumberExpectedResult, tenHoursNumberResult);

        var twoHoursNameResult = new TimeSpan(2, 0, 0).ToNameFormatString();
        var twoHoursNameExpectedResult = "2 h";

        var oneDayNameResult = new TimeSpan(24, 0, 0).ToNameFormatString();
        var oneDayNameExpectedResult = "1 d";

        var oneWithHalfDayNameResult = new TimeSpan(36, 0, 0).ToNameFormatString();
        var oneWithHalfDayNameExpectedResult = "1 d 12 h";

        Assert.AreEqual(twoHoursNameExpectedResult, twoHoursNameResult);
        Assert.AreEqual(oneDayNameExpectedResult, oneDayNameResult);
        Assert.AreEqual(oneWithHalfDayNameExpectedResult, oneWithHalfDayNameResult);
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
