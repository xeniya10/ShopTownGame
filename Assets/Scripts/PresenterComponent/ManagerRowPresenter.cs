using System;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;

namespace ShopTown.PresenterComponent
{
public class ManagerRowPresenter
{
    public ManagerRowModel ManagerRowModel;
    private readonly ManagerRowView _managerRowView;

    private ManagerRowPresenter(ManagerRowView managerRowView, ManagerRowModel managerRowModel)
    {
        _managerRowView = managerRowView;
        ManagerRowModel = managerRowModel;
    }

    public ManagerRowPresenter Create(Transform parent, ManagerRowModel model)
    {
        var view = _managerRowView.Create(parent);
        view.Initialize(model);
        SetState(model.State);

        var rowPresenter = new ManagerRowPresenter(view, model);
        return rowPresenter;
    }

    public void SetState(ManagerState state)
    {
        ManagerRowModel.SetState(state);

        switch (state)
        {
            case ManagerState.Hide:
                _managerRowView.Hide();
                break;

            case ManagerState.Lock:
                _managerRowView.Lock();
                break;

            case ManagerState.Unlock:
                _managerRowView.Unlock();
                break;
        }
    }

    public void Activate()
    {
        _managerRowView.Salute.Play();
        SetState(ManagerState.Lock);
        ManagerRowModel.IsActivated = true;
    }

    public void SubscribeToHireButton(Action<ManagerRowPresenter> callBack)
    {
        _managerRowView.HireButton.onClick.AddListener(() => callBack?.Invoke(this));
    }
}
}
