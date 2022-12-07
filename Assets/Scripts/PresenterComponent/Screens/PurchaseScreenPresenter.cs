using System.Collections.Generic;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class PurchaseScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] public readonly IPurchaseScreenView _purchaseScreen;
    [Inject] private readonly IPackCellView _packCell;
    [Inject] private readonly PacksData _packsData;

    public void Initialize()
    {
        SubscribeToButton(_purchaseScreen.GetHideButton(), () => _purchaseScreen.SetActive(false));
        CreatePacks(_packsData.DollarPacks, _purchaseScreen.GetDollarArea());
        CreatePacks(_packsData.GoldPacks, _purchaseScreen.GetGoldArea());
    }

    private void CreatePacks(List<PackModel> packs, Transform parent)
    {
        foreach (var model in packs)
        {
            var dollarPack = _packCell.Instantiate(parent);
            dollarPack.Initialize(model);
        }
    }
}
}
