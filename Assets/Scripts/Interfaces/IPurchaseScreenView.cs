using UnityEngine;

namespace ShopTown.ViewComponent
{
public interface IPurchaseScreenView : IHideButton, IPackArea, IChangeActivation, IActivatableScreen
{}

public interface IPackArea
{
    Transform GetDollarArea();

    Transform GetGoldArea();
}
}
