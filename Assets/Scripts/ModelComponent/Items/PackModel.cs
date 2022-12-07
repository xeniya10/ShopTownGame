using System;

namespace ShopTown.ModelComponent
{
[Serializable]
public class PackModel
{
    public MoneyModel Profit;
    public double Price;
    public int Size;

    public PackModel(MoneyModel profit, double price, int size)
    {
        Profit = profit;
        Price = price;
        Size = size;
    }
}
}
