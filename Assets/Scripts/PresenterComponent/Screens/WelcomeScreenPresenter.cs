using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.PresenterComponent
{
public class WelcomeScreenPresenter : ButtonSubscription, IInitializable<MoneyModel>
{
    [Inject] private readonly IWelcomeScreenView _view;

    public void Initialize(MoneyModel model)
    {
        _view.Initialize(model);
        _view.SetActive(true);
        SubscribeToButton(_view.GetHideButton(), () => _view.SetActive(false));
    }
}
}
