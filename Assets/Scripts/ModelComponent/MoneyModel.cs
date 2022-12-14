using System;

namespace ShopTown.ModelComponent
{
public enum Currency { Dollar, Gold }

[Serializable]
public class MoneyModel
{
    public double Number;
    public Currency Value;

    public MoneyModel(double number, Currency value = Currency.Dollar)
    {
        Number = number;
        Value = value;
    }
}
}
