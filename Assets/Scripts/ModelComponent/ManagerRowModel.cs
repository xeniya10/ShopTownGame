using ShopTown.Data;

namespace ShopTown.ModelComponent
{
public enum ManagerState { Hide, Lock, Unlock }

public class ManagerRowModel
{
    // Description
    public int Level;
    public string Name;
    public string Description;

    // Cost
    public MoneyModel Cost;

    // State
    public ManagerState State;
    public bool IsActivated;

    // Data Containers
    private readonly BusinessData _business;
    private readonly ManagerRowData _rowData;

    // [Inject]
    public ManagerRowModel(BusinessData business, ManagerRowData rowData)
    {
        _business = business;
        _rowData = rowData;
    }

    public void Initialize(int level, bool isActivated, ManagerState state)
    {
        Level = level;
        IsActivated = isActivated;
        SetState(state);
        SetDescription();
        SetName();
        SetCost();
    }

    public void SetState(ManagerState state)
    {
        State = state;
    }

    private void SetDescription()
    {
        var businessName = _business.LevelNames[Level - 1];
        Description = $"Hire manager to run your {businessName}";
    }

    private void SetName()
    {
        Name = _rowData.ManagerNames[Level - 1];
    }

    private void SetCost()
    {
        Cost = _rowData.BaseCost[Level - 1];
    }
}
}
