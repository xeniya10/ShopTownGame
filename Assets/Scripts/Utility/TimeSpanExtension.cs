using System;

public static class TimeSpanExtension
{
    public static string ToFormattedString(this TimeSpan timeSpan)
    {
        if (timeSpan.Hours == 0 && timeSpan.Minutes == 0)
        {
            return timeSpan.ToString(timeSpan.Seconds < 10 ? @"s\.f" : @"ss\.f");
        }

        if (timeSpan.Hours == 0)
        {
            return timeSpan.ToString(timeSpan.Minutes < 10 ? @"m\:ss" : @"mm\:ss");
        }

        return timeSpan.ToString(timeSpan.Hours < 10 ? @"h\:mm\:ss" : @"hh\:mm\:ss");
    }
}
