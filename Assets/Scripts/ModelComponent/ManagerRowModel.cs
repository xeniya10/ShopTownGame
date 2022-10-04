using ShopTown.Data;

namespace ShopTown.ModelComponent
{
public class ManagerRowModel
{
    // Description
    public int Level;
    public string Name { get { return _rowData.ManagerNames[Level - 1]; } }
    private string BusinessName { get { return _business.LevelNames[Level - 1]; } }
    public string Description { get { return $"Hire manager to run your {BusinessName}"; } }

    // Cost
    public double MoneyCost { get { return _rowData.MoneyBaseCost[Level - 1]; } }
    public double GoldCost { get { return _rowData.GoldBaseCost[Level - 1]; } }

    // State
    public bool Unlocked;

    // Data Containers
    private readonly BusinessData _business;
    private readonly ManagerRowData _rowData;

    public ManagerRowModel(BusinessData business, ManagerRowData rowData)
    {
        _business = business;
        _rowData = rowData;
    }
}
}
