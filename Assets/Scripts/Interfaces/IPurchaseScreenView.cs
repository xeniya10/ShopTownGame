using UnityEngine;

namespace ShopTown.ViewComponent
{
public interface IPurchaseScreenView : IHideButton, IPackArea, IActivatableScreen
{}

public interface IPackArea
{
    Transform GetDollarArea();

    Transform GetGoldArea();
}
}
