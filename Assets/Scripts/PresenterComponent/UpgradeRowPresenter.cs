using System;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;

namespace ShopTown.PresenterComponent
{
public class UpgradeRowPresenter
{
    public UpgradeRowModel UpgradeRowModel;
    private readonly UpgradeRowView _upgradeRowView;

    private UpgradeRowPresenter(UpgradeRowView upgradeRowView, UpgradeRowModel upgradeRowModel)
    {
        _upgradeRowView = upgradeRowView;
        UpgradeRowModel = upgradeRowModel;
    }

    public UpgradeRowPresenter Create(Transform parent, UpgradeRowModel model)
    {
        var rowView = _upgradeRowView.Create(parent);
        rowView.Initialize(model);
        SetState(model.State);

        var rowPresenter = new UpgradeRowPresenter(rowView, model);
        return rowPresenter;
    }

    public void SetState(UpgradeState state)
    {
        UpgradeRowModel.State = state;

        switch (state)
        {
            case UpgradeState.Hide:
                _upgradeRowView.Hide();
                break;

            case UpgradeState.Lock:
                _upgradeRowView.Lock();
                break;

            case UpgradeState.Unlock:
                _upgradeRowView.Unlock();
                break;
        }
    }

    public void LevelUp()
    {
        if (UpgradeRowModel.UpgradeLevel == 3)
        {
            SetState(UpgradeState.Lock);
            return;
        }

        UpgradeRowModel.UpgradeLevel += 1;
        _upgradeRowView.Initialize(UpgradeRowModel);
    }

    public void SubscribeToBuyButton(Action<UpgradeRowPresenter> callBack)
    {
        _upgradeRowView.BuyButton.onClick.AddListener(() => callBack?.Invoke(this));
    }

    public void PlaySalute()
    {
        // _managerRowView.Salute.Play();
    }
}
}
