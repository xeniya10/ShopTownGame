using ShopTown.ControllerComponent;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class WelcomeScreenPresenter : IInitializable, IShowable<IGameData>
{
    [Inject] private readonly IPlayable _audio;
    [Inject] private readonly IButtonSubscriber _subscriber;
    [Inject] private readonly IScreenView<MoneyModel> _view;

    public void Initialize()
    {
        _subscriber.AddListenerToButton(_view.GetHideButton(), () => _view.SetActive(false));
    }

    public void Show(IGameData model)
    {
        if (model.GameData.OfflineDollarBalance != null)
        {
            _audio.PlayNewCoinSound();
            _view.Initialize(model.GameData.OfflineDollarBalance);
            _view.SetActive(true);
        }
    }
}
}
