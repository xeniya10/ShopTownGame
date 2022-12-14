using ShopTown.ControllerComponent;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class WelcomeScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IGameBoardController _gameBoard;
    [Inject] private readonly IScreenView<MoneyModel> _view;

    public void Initialize()
    {
        _gameBoard.SetOfflineProfitEvent += Show;
        SubscribeToButton(_view.GetHideButton(), () => _view.SetActive(false));
    }

    private void Show()
    {
        if (_data.GameData.OfflineDollarBalance != null)
        {
            _view.Initialize(_data.GameData.OfflineDollarBalance);
            _view.SetActive(true);
        }
    }
}
}
