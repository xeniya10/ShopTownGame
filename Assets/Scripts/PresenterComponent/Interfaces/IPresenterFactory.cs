using ShopTown.ModelComponent;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public interface IPresenterFactory<out T>
{
    T Create(IModel model, IView view);
}
}
