using ShopTown.Data;

namespace ShopTown.ModelComponent
{
public class UpgradeRowModel
{
    // Description
    public int Level;
    public int UpgradeLevel = 1;
    private string BusinessName { get { return _business.LevelNames[Level - 1]; } }
    public string Description { get { return $"Increase {BusinessName} profit x{UpgradeLevel + 1}"; } }
    public string Name { get { return GetName(); } }

    // Cost
    private MoneyModel BaseCost { get { return _rowData.BaseCost[Level - 1]; } }
    public MoneyModel Cost { get { return new MoneyModel(BaseCost.Number * UpgradeLevel, BaseCost.Value); } }

    // State
    public bool Unlocked;

    // Data Containers
    private readonly BusinessData _business;
    private readonly UpgradeRowData _rowData;

    public UpgradeRowModel(BusinessData business, UpgradeRowData rowData)
    {
        _business = business;
        _rowData = rowData;
    }

    private string GetName()
    {
        if (UpgradeLevel == 2)
        {
            return _rowData.SecondLevelNames[Level - 1];
        }

        if (UpgradeLevel == 3)
        {
            return _rowData.ThirdLevelNames[Level - 1];
        }

        return _rowData.FirstLevelNames[Level - 1];
    }
}
}
