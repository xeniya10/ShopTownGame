using System;

namespace ShopTown.ModelComponent
{
public enum CurrencyType { Dollar, Gold }

[Serializable]
public class MoneyModel
{
    public double Value;
    public CurrencyType Currency;

    public MoneyModel(double value, CurrencyType currency = CurrencyType.Dollar)
    {
        Value = value;
        Currency = currency;
    }
}
}
