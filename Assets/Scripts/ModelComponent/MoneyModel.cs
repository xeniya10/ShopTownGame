using System;
using System.Collections.Generic;

namespace ShopTown.ModelComponent
{
public enum Currency { Dollar, Gold }

[Serializable]
public class MoneyModel
{
    public double Number;
    public Currency Value;

    public MoneyModel(double number, Currency value)
    {
        Number = number;
        Value = value;
    }

    public string ToFormattedString()
    {
        var number = MoneyFormat.GetNumberDetails(Number);
        return number.FormattedNumber + number.Scale;
    }
}

public class MoneyFormat
{
    public double Number { get; private set; }
    public string FormattedNumber { get; private set; }
    public string Scale { get; private set; }

    private static Dictionary<int, string> _scales = new Dictionary<int, string>()
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

    private static readonly int _lowestScale = 3;

    private MoneyFormat(double number, string formattedNumber, string scale)
    {
        Number = number;
        FormattedNumber = formattedNumber;
        Scale = scale;
    }

    // public static MoneyFormat GetNumberParameters(string number)
    // {
    //     return GetNumberDetails(double.Parse(number));
    // }

    public static MoneyFormat GetNumberDetails(double number)
    {
        var textNumber = number.ToString();
        var scale = DigitCount(textNumber) - 1;
        string formattedTextNumber;

        if (scale < _lowestScale)
        {
            formattedTextNumber = number.ToString("#,##0.0");

            if (formattedTextNumber.Substring(formattedTextNumber.IndexOf('.'), 2)
                .Equals(".0"))
            {
                return new MoneyFormat(number, number.ToString("#,##0"), string.Empty);
            }

            return new MoneyFormat(number, formattedTextNumber, string.Empty);
        }

        var scaleModulo = scale % _lowestScale;
        var key = scale - scaleModulo;
        _scales.TryGetValue(key, out var textScale);

        formattedTextNumber = textNumber.Substring(0, scaleModulo + 1);
        var fractionText = "." + textNumber.Substring(scaleModulo + 1, 1);

        if (!fractionText.Equals(".0"))
        {
            formattedTextNumber += fractionText;
        }

        return new MoneyFormat(number, formattedTextNumber, textScale);
    }

    private static int DigitCount(string textNumber)
    {
        var plusIndex = textNumber.IndexOf("+");
        if (plusIndex > 0)
        {
            var s = textNumber.Substring(plusIndex, textNumber.Length - plusIndex);
            return int.Parse(s) + 1;
        }

        var dotIndex = textNumber.IndexOf(".");
        if (dotIndex > 0)
        {
            textNumber = textNumber.Substring(0, dotIndex);
        }

        return textNumber.Length;
    }
}
}
