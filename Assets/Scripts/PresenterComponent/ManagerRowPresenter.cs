using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;

namespace ShopTown.PresenterComponent
{
public class ManagerRowPresenter
{
    private readonly ManagerRowModel _managerRowModel;
    private readonly ManagerRowView _managerRowView;

    private ManagerRowPresenter(ManagerRowView managerRowView, ManagerRowModel managerRowModel)
    {
        _managerRowView = managerRowView;
        _managerRowModel = managerRowModel;
    }

    public ManagerRowPresenter Create(Transform parent, ManagerRowModel model)
    {
        var view = _managerRowView.Create(parent, model.Level, model.Name, model.Description);
        view.SetMoneyPrice(model.MoneyCost);
        if (model.MoneyCost == 0)
        {
            view.SetGoldPrice(model.GoldCost);
        }

        var rowPresenter = new ManagerRowPresenter(view, model);
        return rowPresenter;
    }
}
}
