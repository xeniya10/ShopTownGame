using ShopTown.Data;

namespace ShopTown.ModelComponent
{
public enum UpgradeState { Hide, Lock, Unlock }

public class UpgradeRowModel
{
    // Description
    public int Level;
    public int UpgradeLevel;
    public string Description;
    public string Name;

    // Cost
    public MoneyModel Cost;

    // State
    public UpgradeState State;
    public bool AreAllLevelsActivated;

    // Data Containers
    private readonly BusinessData _business;
    private readonly UpgradeRowData _rowData;

    public UpgradeRowModel(BusinessData business, UpgradeRowData rowData)
    {
        _business = business;
        _rowData = rowData;
    }

    public void Initialize(int level, int upgradeLevel, bool areAllLevelsActivated,
        UpgradeState state)
    {
        Level = level;
        UpgradeLevel = upgradeLevel;
        AreAllLevelsActivated = areAllLevelsActivated;
        State = state;
        SetDescription();
        SetName();
        SetCost();
    }

    private void SetDescription()
    {
        var businessName = _business.LevelNames[Level - 1];
        Description = $"Increase {businessName} profit x{UpgradeLevel + 1}";
    }

    private void SetName()
    {
        if (UpgradeLevel == 2)
        {
            Name = _rowData.SecondLevelNames[Level - 1];
            return;
        }

        if (UpgradeLevel == 3)
        {
            Name = _rowData.ThirdLevelNames[Level - 1];
            return;
        }

        Name = _rowData.FirstLevelNames[Level - 1];
    }

    private void SetCost()
    {
        var baseCost = _rowData.BaseCost[Level - 1];
        Cost = new MoneyModel(baseCost.Number * UpgradeLevel, baseCost.Value);
    }
}
}
