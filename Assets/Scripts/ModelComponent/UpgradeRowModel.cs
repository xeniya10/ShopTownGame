using System;

[Serializable]
public class UpgradeRowModel
{
    public int Level;
    public string BusinessName { get { return _business.LevelNames[Level - 1]; } }
    public int UpgradeLevel = 1;
    public string Name
    {
        get
        {
            if (UpgradeLevel == 1)
            {
                return _rowData.FirstNames[Level - 1];
            }

            if (UpgradeLevel == 2)
            {
                return _rowData.SecondNames[Level - 1];
            }

            return _rowData.ThirdNames[Level - 1];
        }
    }
    public string Description { get { return $"{BusinessName} profit x{UpgradeLevel + 1}"; } }
    public double BaseMoneyCost { get { return _rowData.MoneyBaseCost[Level - 1]; } }
    public double MoneyCost { get { return BaseMoneyCost * UpgradeLevel; } }
    public double BaseGoldCost { get { return _rowData.GoldBaseCost[Level - 1]; } }
    public double GoldCost { get { return BaseGoldCost * UpgradeLevel; } }
    public bool Unlocked = false;

    private readonly BusinessData _business;
    private readonly UpgradeRowData _rowData;

    public UpgradeRowModel(BusinessData business, UpgradeRowData rowData)
    {
        _business = business;
        _rowData = rowData;
    }
}