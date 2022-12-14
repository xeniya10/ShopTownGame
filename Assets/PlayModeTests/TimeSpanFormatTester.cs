using System;
using System.Collections.Generic;
using NUnit.Framework;

public class TimeSpanFormatTester
{
    private List<TimeSpan> _results = new List<TimeSpan>()
    {
        new TimeSpan(0, 0, 2),
        new TimeSpan(0, 0, 10),
        new TimeSpan(0, 2, 0),
        new TimeSpan(0, 2, 2),
        new TimeSpan(0, 10, 0),
        new TimeSpan(0, 10, 10),
        new TimeSpan(2, 0, 0),
        new TimeSpan(2, 2, 0),
        new TimeSpan(2, 2, 2),
        new TimeSpan(10, 0, 0),
        new TimeSpan(10, 10, 0),
        new TimeSpan(10, 10, 10),
        new TimeSpan(2, 0, 0, 0),
        new TimeSpan(2, 2, 0, 0),
        new TimeSpan(2, 2, 2, 0),
        new TimeSpan(2, 2, 2, 2),
        new TimeSpan(10, 0, 0, 0),
        new TimeSpan(10, 10, 0, 0),
        new TimeSpan(10, 10, 10, 0),
        new TimeSpan(10, 10, 10, 10)
    };

    private List<string> _numberFormatExpectedResults = new List<string>()
    {
        "2.0",
        "10.0",
        "2:00.0",
        "2:02.0",
        "10:00.0",
        "10:10.0",
        "2:00:00",
        "2:02:00",
        "2:02:02",
        "10:00:00",
        "10:10:00",
        "10:10:10",
        "2.00:00",
        "2.02:00",
        "2.02:02",
        "2.02:02",
        "10.00:00",
        "10.10:00",
        "10.10:10",
        "10.10:10"
    };

    [Test] public void TimeNumberFormatTest()
    {
        for (var i = 0; i < _results.Count; i++)
        {
            Assert.AreEqual(_numberFormatExpectedResults[i], _results[i].ToNumberFormatString());
        }
    }

    private List<string> _symbolFormatExpectedResults = new List<string>()
    {
        "2 s",
        "10 s",
        "2 m",
        "2 m 2 s",
        "10 m",
        "10 m 10 s",
        "2 h",
        "2 h 2 m",
        "2 h 2 m 2 s",
        "10 h",
        "10 h 10 m",
        "10 h 10 m 10 s",
        "2 d",
        "2 d 2 h",
        "2 d 2 h 2 m",
        "2 d 2 h 2 m 2 s",
        "10 d",
        "10 d 10 h",
        "10 d 10 h 10 m",
        "10 d 10 h 10 m 10 s"
    };

    [Test] public void TimeSymbolFormatTest()
    {
        for (var i = 0; i < _results.Count; i++)
        {
            Assert.AreEqual(_symbolFormatExpectedResults[i], _results[i].ToSymbolFormatString());
        }
    }
}
