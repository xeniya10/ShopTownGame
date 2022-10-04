using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;

namespace ShopTown.PresenterComponent
{
public class UpgradeRowPresenter
{
    private readonly UpgradeRowModel _upgradeRowModel;
    private readonly UpgradeRowView _upgradeRowView;

    private UpgradeRowPresenter(UpgradeRowView upgradeRowView, UpgradeRowModel upgradeRowModel)
    {
        _upgradeRowView = upgradeRowView;
        _upgradeRowModel = upgradeRowModel;
    }

    public UpgradeRowPresenter Create(Transform parent, UpgradeRowModel model)
    {
        var rowView = _upgradeRowView.Create(parent);
        rowView.Initialize(model.UpgradeLevel, model.Level, model.Name, model.Description, model.MoneyCost,
            model.GoldCost);

        var rowPresenter = new UpgradeRowPresenter(rowView, model);
        return rowPresenter;
    }
}
}
