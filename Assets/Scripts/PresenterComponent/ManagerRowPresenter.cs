using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;

namespace ShopTown.PresenterComponent
{
public class ManagerRowPresenter
{
    private readonly ManagerRowModel _managerRowModel;
    private readonly ManagerRowView _managerRowView;

    public MoneyModel Cost { get { return _managerRowModel.Cost; } }

    private ManagerRowPresenter(ManagerRowView managerRowView, ManagerRowModel managerRowModel)
    {
        _managerRowView = managerRowView;
        _managerRowModel = managerRowModel;
    }

    public ManagerRowPresenter Create(Transform parent, ManagerRowModel model)
    {
        var view = _managerRowView.Create(parent);
        view.Initialize(model);

        var rowPresenter = new ManagerRowPresenter(view, model);
        return rowPresenter;
    }

    public void Lock()
    {}

    public void Unlock()
    {}
}
}
