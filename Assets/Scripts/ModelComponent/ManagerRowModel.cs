using System;

[Serializable]
public class ManagerRowModel
{
    public int Level;
    public string BusinessName { get { return _business.LevelNames[Level - 1]; } }
    public string Name { get { return _rowData.ManagerNames[Level - 1]; } }
    public string Description { get { return $"Hire manager to run your {BusinessName}"; } }
    public double MoneyCost { get { return _rowData.MoneyBaseCost[Level - 1]; } }
    public double GoldCost { get { return _rowData.GoldBaseCost[Level - 1]; } }
    public bool Unlocked = false;

    private readonly BusinessData _business;
    private readonly ManagerRowData _rowData;

    public ManagerRowModel(BusinessData business, ManagerRowData rowData)
    {
        _business = business;
        _rowData = rowData;
    }
}
