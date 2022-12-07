using System;

[Serializable]
public struct TimeModel
{
    public int Days;
    public int Hours;
    public int Minutes;
    public int Seconds;

    public TimeModel(int days, int hours, int minutes, int seconds)
    {
        Days = days;
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }

    public TimeSpan ToTimeSpan()
    {
        return new TimeSpan(Days, Hours, Minutes, Seconds);
    }
}
