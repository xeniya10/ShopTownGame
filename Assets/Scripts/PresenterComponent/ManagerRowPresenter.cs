using UnityEngine;

public class ManagerRowPresenter
{
    private readonly ManagerRowView _managerRowView;
    private readonly ManagerRowModel _managerRowModel;

    public ManagerRowPresenter(ManagerRowView managerRowView,
    ManagerRowModel managerRowModel)
    {
        _managerRowView = managerRowView;
        _managerRowModel = managerRowModel;
    }

    public ManagerRowPresenter Create(Transform parent, ManagerRowModel _managerRowModel)
    {
        var rowView = _managerRowView.Create(parent, _managerRowModel.Level, _managerRowModel.Name, _managerRowModel.Description);
        rowView.SetMoneyPrice(_managerRowModel.MoneyCost);
        if (_managerRowModel.MoneyCost == 0)
        {
            rowView.SetGoldPrice(_managerRowModel.GoldCost);
        }

        var rowPresenter = new ManagerRowPresenter(rowView, _managerRowModel);
        return rowPresenter;
    }
}
