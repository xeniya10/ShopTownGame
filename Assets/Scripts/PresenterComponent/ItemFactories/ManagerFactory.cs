using ShopTown.ModelComponent;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public class ManagerFactory : IPresenterFactory<IManager>
{
    public IManager Create(IModel model, IView view)
    {
        return new ManagerPresenter(model, view);
    }
}
}
