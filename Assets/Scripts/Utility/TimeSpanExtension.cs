using System;

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
            return timeSpan.ToString(timeSpan.Hours < 10 ? @"h\:mm" : @"hh\:mm");
        }

        return timeSpan.ToString(timeSpan.Days < 10 ? @"d\.hh\:mm" : @"dd\.hh\:mm");
    }

    public static string ToNameFormatString(this TimeSpan timeSpan)
    {
        if (timeSpan.Days == 0 && timeSpan.Hours == 0 && timeSpan.Minutes == 0)
        {
            return $"{timeSpan.Seconds} s";
        }

        if (timeSpan.Days == 0 && timeSpan.Hours == 0)
        {
            return $"{timeSpan.Minutes} m";
        }

        if (timeSpan.Days == 0)
        {
            return $"{timeSpan.Hours} h";
        }

        if (timeSpan.Days != 0 && timeSpan.Hours != 0)
        {
            return $"{timeSpan.Days} d {timeSpan.Hours} h";
        }

        return $"{timeSpan.Days} d";
    }
}
