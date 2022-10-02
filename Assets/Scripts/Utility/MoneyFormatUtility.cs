using System.Collections.Generic;

public class MoneyModel
{
    public double Number { get; private set; }
    public string FormattedNumber { get; private set; }
    public string Scale { get; private set; }

    public MoneyModel(double number, string formattednumber, string scale)
    {
        this.Number = number;
        this.FormattedNumber = formattednumber;
        this.Scale = scale;
    }
}

public static class MoneyFormatUtility
{
    public static Dictionary<int, string> Scales = new Dictionary<int, string>
    {
        {3, "K"},
        {6, "M"},
        {9, "B"},
        {12, "T"},
        {15, "Q"},
        {18, "Qn"},
        {21, "S"},
        {24, "Sp"},
        {27, "O"},
        {30, "N"},
        {33, "D"},
        {36, "UD"},
        {39, "DD"},
        {42, "TD"},
        {45, "QD"},
        {48, "QnD"},
        {51, "SD"},
        {54, "SpD"},
        {57, "OD"},
        {60, "ND"},
        {63, "VT"},
        {66, "UT"},
        {69, "DT"},
        {72, "TT"},
        {75, "QT"},
        {78, "QnT"},
        {81, "ST"},
        {84, "SpT"},
        {87, "OT"},
        {90, "NT"},
        {93, "TT"},
        {96, "UTT"},
        {99, "DTT"},
        {102, "TTT"},
        {105, "QTT"},
        {108, "QnTT"},
        {111, "STT"},
        {114, "SpTT"},
        {117, "OTT"},
        {120, "NTT"},
        {123, "QT"},
        {126, "UQT"},
        {129, "DQT"},
        {132, "TQT"},
        {135, "QQT"},
        {138, "QnQT"},
        {141, "SQT"},
        {144, "SpQT"},
        {147, "OQnT"},
        {150, "NQnT"},
        {153, "QQnT"},
        {156, "UQQnT"},
        {159, "DQQnT"},
        {162, "TQQnT"},
        {165, "QQQnT"},
        {168, "QnQQnT"},
        {171, "SQQnT"},
        {174, "SpQQnT"},
        {177, "OQQnT"},
        {180, "NQQnT"},
        {183, "ST"},
        {186, "UST"},
        {189, "DST"},
        {192, "TST"},
        {195, "QST"},
        {198, "QnST"},
        {201, "SST"},
        {204, "SpST"},
        {207, "OST"},
        {210, "NST"},
        {213, "SpT"},
        {216, "USpT"},
        {219, "DSpT"},
        {222, "TSpT"},
        {225, "QSpT"},
        {228, "QtSpT"},
        {231, "SSpT"},
        {234, "SpSpT"},
        {237, "OSpT"},
        {240, "NSpT"},
        {243, "OT"},
        {246, "UOT"},
        {249, "DOT"},
        {252, "TOT"},
        {255, "QOT"},
        {258, "QnOT"},
        {261, "SOT"},
        {264, "SpOT"},
        {267, "OOT"},
        {270, "NOT"},
        {273, "NT"},
        {276, "UNT"},
        {279, "DNT"},
        {282, "TNT"},
        {285, "QNT"},
        {288, "QnNT"},
        {291, "SNT"},
        {294, "SpNT"},
        {297, "ONT"},
        {300, "NNT"},
        {303, "C"},
        {306, "UC"}
    };

    private static int lowestScale = 3;

    public static MoneyModel GetNumberParameters(string number)
    {
        return GetNumberDetails(double.Parse(number));
    }

    public static MoneyModel GetNumberDetails(double number)
    {
        var textNumber = number.ToString();
        var scale = DigitCount(textNumber) - 1;
        string formatedTextNumber;
        string textScale;

        if (scale < lowestScale)
        {
            formatedTextNumber = number.ToString("#,##0.0");

            if (formatedTextNumber.Substring(formatedTextNumber.IndexOf('.'), 2).Equals(".0"))
            {
                return new MoneyModel(number, number.ToString("#,##0"), string.Empty);
            }

            return new MoneyModel(number, formatedTextNumber, string.Empty);
        }

        var scaleModulo = scale % lowestScale;
        var key = scale - scaleModulo;
        Scales.TryGetValue(key, out textScale);

        formatedTextNumber = textNumber.Substring(0, scaleModulo + 1);
        var fractionText = "." + textNumber.Substring(scaleModulo + 1, 1);

        if (!fractionText.Equals(".0"))
        {
            formatedTextNumber += fractionText;
        }

        return new MoneyModel(number, formatedTextNumber, textScale);
    }

    private static int DigitCount(string textNumber)
    {
        int plusIndex = textNumber.IndexOf("+");
        if (plusIndex > 0)
        {
            string s = textNumber.Substring(plusIndex, textNumber.Length - plusIndex);
            return int.Parse(s) + 1;
        }

        int dotIndex = textNumber.IndexOf(".");
        if (dotIndex > 0)
        {
            textNumber = textNumber.Substring(0, dotIndex);
        }

        return textNumber.Length;
    }

    public static string Default(double unformatted)
    {
        var money = GetNumberDetails(unformatted);
        return money.FormattedNumber + money.Scale;
    }

    public static string MoneyDefault(double unformatted)
    {
        var money = GetNumberDetails(unformatted);
        return "$ " + money.FormattedNumber + money.Scale;
    }

    public static string GoldDefault(double unformatted)
    {
        var money = GetNumberDetails(unformatted);
        return "G " + money.FormattedNumber + money.Scale;
    }
}