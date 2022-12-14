public static class DoubleExtension
{
    public static string ToFormattedString(this double number)
    {
        return "$" + number.ToString("#,##0.00");
    }
}
