using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class NewBusinessScreenPresenter : ButtonSubscription, IShowable<GameCellModel>, IInitializable
{
    [Inject] private readonly INewBusinessScreenView _view;

    public void Initialize()
    {
        SubscribeToButton(_view.GetHideButton(), () => _view.SetActive(false));
    }

    public void Show(GameCellModel model)
    {
        _view.Initialize(model);
        _view.SetActive(true);
    }
}
}
