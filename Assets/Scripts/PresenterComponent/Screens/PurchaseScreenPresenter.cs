using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class PurchaseScreenPresenter : IInitializable
{
    [Inject] private readonly IButtonSubscriber _subscriber;
    [Inject] private readonly IPurchaseScreenView _purchaseScreen;
    [Inject] private readonly ICellView<PackModel> _packCell;
    [Inject] private readonly IPackData _packsData;

    public void Initialize()
    {
        _subscriber.AddListenerToButton(_purchaseScreen.GetHideButton(), () => _purchaseScreen.SetActive(false));
        CreateDollarPacks();
        CreateGoldPacks();
    }

    private void CreateDollarPacks()
    {
        for (var i = 0; i < _packsData.GetDollarPackCount(); i++)
        {
            var dollarPack = _packCell.Instantiate(_purchaseScreen.GetDollarArea());
            dollarPack.Initialize(_packsData.GetDollarPack(i + 1));
        }
    }

    private void CreateGoldPacks()
    {
        for (var i = 0; i < _packsData.GetGoldPackCount(); i++)
        {
            var dollarPack = _packCell.Instantiate(_purchaseScreen.GetGoldArea());
            dollarPack.Initialize(_packsData.GetGoldPack(i + 1));
        }
    }
}
}
