using ShopTown.ModelComponent;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public class GameCellFactory : IPresenterFactory<IGameCell>
{
    public IGameCell Create(IModel model, IView view)
    {
        return new GameCellPresenter(model, view);
    }
}
}
