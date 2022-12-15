using ShopTown.ModelComponent;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public class UpgradeFactory : IPresenterFactory<IUpgrade>
{
    public IUpgrade Create(IModel model, IView view)
    {
        return new UpgradePresenter(model, view);
    }
}
}
