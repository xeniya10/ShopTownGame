using System;
using System.Text;

public static class TimeSpanExtension
{
    public static string ToNumberFormatString(this TimeSpan timeSpan)
    {
        if (timeSpan.Days == 0 && timeSpan.Hours == 0 && timeSpan.Minutes == 0)
        {
            return timeSpan.ToString(timeSpan.Seconds < 10 ? @"s\.f" : @"ss\.f");
        }

        if (timeSpan.Days == 0 && timeSpan.Hours == 0)
        {
            return timeSpan.ToString(timeSpan.Minutes < 10 ? @"m\:ss\.f" : @"mm\:ss\.f");
        }

        if (timeSpan.Days == 0)
        {
            return timeSpan.ToString(timeSpan.Hours < 10 ? @"h\:mm\:ss" : @"hh\:mm\:ss");
        }

        return timeSpan.ToString(timeSpan.Days < 10 ? @"d\.hh\:mm" : @"dd\.hh\:mm");
    }

    public static string ToSymbolFormatString(this TimeSpan timeSpan)
    {
        var seconds = $"{timeSpan.Seconds} s";
        var minutes = $"{timeSpan.Minutes} m ";
        var hours = $"{timeSpan.Hours} h ";
        var days = $"{timeSpan.Days} d ";

        var symbolFormatTimeSpan = new StringBuilder();

        if (timeSpan.Days != 0)
        {
            symbolFormatTimeSpan.Append(days);
        }

        if (timeSpan.Hours != 0)
        {
            symbolFormatTimeSpan.Append(hours);
        }

        if (timeSpan.Minutes != 0)
        {
            symbolFormatTimeSpan.Append(minutes);
        }

        if (timeSpan.Seconds != 0)
        {
            symbolFormatTimeSpan.Append(seconds);
        }

        if (symbolFormatTimeSpan[^1] == ' ')
        {
            symbolFormatTimeSpan.Remove(symbolFormatTimeSpan.Length - 1, 1);
        }

        return symbolFormatTimeSpan.ToString();
    }
}
