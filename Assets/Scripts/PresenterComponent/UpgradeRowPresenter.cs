using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;

namespace ShopTown.PresenterComponent
{
public static class StringExtension
{
    public static string PlusHello(this string str)
    {
        return str + "Hello";
    }
}

public class UpgradeRowPresenter
{
    private readonly UpgradeRowModel _upgradeRowModel;
    private readonly UpgradeRowView _upgradeRowView;

    public MoneyModel Cost { get { return _upgradeRowModel.Cost; } }

    private UpgradeRowPresenter(UpgradeRowView upgradeRowView, UpgradeRowModel upgradeRowModel)
    {
        var str = "aaa";
        str.PlusHello();
        _upgradeRowView = upgradeRowView;
        _upgradeRowModel = upgradeRowModel;
    }

    public UpgradeRowPresenter Create(Transform parent, UpgradeRowModel model)
    {
        var rowView = _upgradeRowView.Create(parent);
        rowView.Initialize(model);

        var rowPresenter = new UpgradeRowPresenter(rowView, model);
        return rowPresenter;
    }

    public void Lock()
    {}

    public void Unlock()
    {}
}
}
