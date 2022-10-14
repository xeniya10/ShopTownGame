using System;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;

namespace ShopTown.PresenterComponent
{
public class ManagerRowPresenter
{
    private readonly ManagerRowModel _managerRowModel;
    private readonly ManagerRowView _managerRowView;

    public int Level { get { return _managerRowModel.Level; } }
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

    public void SetActive(bool isActivated)
    {
        _managerRowModel.Unlocked = isActivated;
        if (isActivated == true)
        {
            _managerRowView.Unlock();
            return;
        }

        _managerRowView.Lock();
    }

    public void SubscribeToHireButton(Action<ManagerRowPresenter> callBack)
    {
        _managerRowView.HireButton.onClick.AddListener(() => callBack?.Invoke(this));
    }
}
}
