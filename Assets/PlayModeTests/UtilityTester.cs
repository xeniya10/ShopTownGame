using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UtilityTester
{
    // Testing time formater in AnimationUtility

    [Test]
    public void SecondFormatTest()
    {
        var twoSecondsResult = AnimationUtility.TimeToString(new TimeSpan(0, 0, 2));
        var twoSecondsExpectedResult = "2.0";

        var tenSecondsResult = AnimationUtility.TimeToString(new TimeSpan(0, 0, 10));
        var tenSecondsExpectedResult = "10.0";

        Assert.AreEqual(twoSecondsExpectedResult, twoSecondsResult);
        Assert.AreEqual(tenSecondsExpectedResult, tenSecondsResult);
    }

    [Test]
    public void MinuteFormatTest()
    {
        var twoMinutesResult = AnimationUtility.TimeToString(new TimeSpan(0, 2, 2));
        var twoMinutesExpectedResult = "2:02";

        var tenMinutesResult = AnimationUtility.TimeToString(new TimeSpan(0, 10, 10));
        var tenMinutesExpectedResult = "10:10";

        Assert.AreEqual(twoMinutesExpectedResult, twoMinutesResult);
        Assert.AreEqual(tenMinutesExpectedResult, tenMinutesResult);
    }

    [Test]
    public void HourFormatTest()
    {
        var twoHoursResult = AnimationUtility.TimeToString(new TimeSpan(2, 2, 2));
        var twoHoursExpectedResult = "2:02:02";

        var tenHoursResult = AnimationUtility.TimeToString(new TimeSpan(10, 10, 10));
        var tenHoursExpectedResult = "10:10:10";

        Assert.AreEqual(twoHoursExpectedResult, twoHoursResult);
        Assert.AreEqual(tenHoursExpectedResult, tenHoursResult);
    }

    // Testing money formater

    [Test]
    public void UnitFormatTest()
    {
        var fiveUnitsResult = MoneyFormatUtility.Default(5);
        var fiveUnitsExpectedResult = "5";

        var nineAndHalfResult = MoneyFormatUtility.Default(9.5);
        var nineAndHalfExpectedResult = "9.5";

        Assert.AreEqual(fiveUnitsExpectedResult, fiveUnitsResult);
        Assert.AreEqual(nineAndHalfExpectedResult, nineAndHalfResult);
    }

    [Test]
    public void HundredsFormatTest()
    {
        var fiveHundredsResult = MoneyFormatUtility.Default(500);
        var fiveHundredsExpectedResult = "500";

        var nineHundredsAndHalfResult = MoneyFormatUtility.Default(900.5);
        var nineHundredsAndHalfExpectedResult = "900.5";

        Assert.AreEqual(fiveHundredsExpectedResult, fiveHundredsResult);
        Assert.AreEqual(nineHundredsAndHalfExpectedResult, nineHundredsAndHalfResult);
    }

    [Test]
    public void MillionsFormatTest()
    {
        var fiveMillionsResult = MoneyFormatUtility.Default(5000000);
        var fiveMillionsExpectedResult = "5M";

        var nineMillionsAndHalfResult = MoneyFormatUtility.Default(9510000);
        var nineMillionsAndHalfExpectedResult = "9.5M";

        Assert.AreEqual(fiveMillionsExpectedResult, fiveMillionsResult);
        Assert.AreEqual(nineMillionsAndHalfExpectedResult, nineMillionsAndHalfResult);
    }
}
