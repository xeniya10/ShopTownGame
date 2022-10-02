using UnityEngine;

public class UpgradeRowPresenter
{
    private readonly UpgradeRowView _upgradeRowView;
    private readonly UpgradeRowModel _upgradeRowModel;

    public UpgradeRowPresenter(UpgradeRowView upgradeRowView,
    UpgradeRowModel upgradeRowModel)
    {
        _upgradeRowView = upgradeRowView;
        _upgradeRowModel = upgradeRowModel;
    }

    public UpgradeRowPresenter Create(Transform parent, UpgradeRowModel _upgradeRowModel)
    {
        var rowView = _upgradeRowView.Create(parent, _upgradeRowModel.UpgradeLevel, _upgradeRowModel.Level, _upgradeRowModel.Name, _upgradeRowModel.Description);
        rowView.SetMoneyPrice(_upgradeRowModel.MoneyCost);
        if (_upgradeRowModel.MoneyCost == 0)
        {
            rowView.SetGoldPrice(_upgradeRowModel.GoldCost);
        }

        var rowPresenter = new UpgradeRowPresenter(rowView, _upgradeRowModel);
        return rowPresenter;
    }
}
